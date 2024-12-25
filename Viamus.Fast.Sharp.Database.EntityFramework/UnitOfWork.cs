using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Database.EntityFramework.Factories;

namespace Viamus.Fast.Sharp.Database.EntityFramework;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable
{
    private readonly DbContext _context;
    private readonly IRepositoryFactory _repositoryFactory;

    [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
    public UnitOfWork(DbContext context, IRepositoryFactory repositoryFactory)
    {
        _context = context;
        _repositoryFactory = repositoryFactory;
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
    {
        return _repositoryFactory.Get<TEntity>(_context);
    }

    public Task CommitAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(_context.SaveChangesAsync(cancellationToken));

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync().ConfigureAwait(false);
        await _repositoryFactory.DisposeAsync().ConfigureAwait(false);
    }
}