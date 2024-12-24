using System.Linq.Expressions;

namespace Viamus.Fast.Sharp.Database.Abstractions;

/// <summary>
/// Interface <c>Entity IRepository</c> Generic IRepository pattern construction
/// </summary>
public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    /// <summary>
    /// This method adds a new entity to the tracking context
    /// </summary>
    /// <param name="entity">Entity type object</param>
    /// <param name="cancellationToken">Cancellation Token, default is none</param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// This method do an upsert in an entity to the tracking context
    /// </summary>
    /// <param name="entity">Entity type object</param>
    /// <param name="cancellationToken">Cancellation Token, default is none</param>
    Task UpsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// This method do a logic exclusion in an entity from the database in the tracking context
    /// </summary>
    /// <param name="entity">Entity type object</param>
    /// <param name="cancellationToken">Cancellation Token, default is none</param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// This method returns an Entity by its id
    /// </summary>
    /// <param name="id">EntityId</param>
    /// <param name="cancellationToken">Cancellation Token, default is none</param>
    /// <returns>Entity</returns>
    Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// This method return an Entity IList based on the expression filter passed by parameter
    /// </summary>
    /// <param name="filter">Expression Filter for query</param>
    /// <param name="orderBy">Function for ordering </param>
    /// <param name="cancellationToken">Cancellation Token, default is none</param>
    /// <param name="includes">Expression of includes</param>
    /// <returns>Entity IList</returns>
    Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes
    );

    /// <summary>
    /// Save all tracked entities to the database
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token, default is none</param>
    Task SaveAsync(CancellationToken cancellationToken = default);
}