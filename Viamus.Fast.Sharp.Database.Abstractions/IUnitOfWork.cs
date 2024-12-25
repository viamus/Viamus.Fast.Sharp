namespace Viamus.Fast.Sharp.Database.Abstractions;

public interface IUnitOfWork: IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    
    Task CommitAsync(CancellationToken cancellationToken = default);
}