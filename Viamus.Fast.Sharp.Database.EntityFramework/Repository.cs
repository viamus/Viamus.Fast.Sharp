using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;

namespace Viamus.Fast.Sharp.Database.EntityFramework;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : Entity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly DbContext _context;
    
    [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        Task.FromResult(_context.Set<TEntity>().Add(entity));

    public async Task UpsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (await _dbSet.FirstOrDefaultAsync(e => e.Id == entity.Id, cancellationToken: cancellationToken) is null)
        {
            await AddAsync(entity, cancellationToken);
        }
        else
        {
            entity.Update();
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Delete();
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<IList<TEntity>> GetAsync
    (
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[]? includes
    )
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes is not null)
        {
            query = includes.Aggregate
            (
                query,
                (current, include) => current.Include(include)
            );
        }

        if (orderBy is not null)
        {
            return await orderBy(query).ToListAsync(cancellationToken);
        }

        return await query.ToListAsync(cancellationToken);
    }
    
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}