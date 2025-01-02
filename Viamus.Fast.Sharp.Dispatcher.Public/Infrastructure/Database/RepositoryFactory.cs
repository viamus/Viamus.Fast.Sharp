using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Database.EntityFramework;
using Viamus.Fast.Sharp.Database.EntityFramework.Factories;

namespace Viamus.Fast.Sharp.Dispatcher.Public.Infrastructure.Database;

public class RepositoryFactory: IRepositoryFactory
{
    private readonly ConcurrentDictionary<Type, object> _repositoryCache = new();
    
    public IRepository<TEntity> Get<TEntity>(DbContext context) where TEntity : Entity
    {
        if (_repositoryCache.TryGetValue(typeof(TEntity), out var repositoryCache))
        {
            return (repositoryCache as IRepository<TEntity>)!;
        }

        var repository = new Repository<TEntity>(context);

        _repositoryCache.TryAdd(typeof(TEntity), repository);

        return repository;
    }
    
    public async ValueTask DisposeAsync()
    {
        foreach (var repositoryCache in _repositoryCache.Values)
        {
            await (repositoryCache as IAsyncDisposable)!.DisposeAsync().ConfigureAwait(false);
        }
        
        GC.SuppressFinalize(this);
    }
}