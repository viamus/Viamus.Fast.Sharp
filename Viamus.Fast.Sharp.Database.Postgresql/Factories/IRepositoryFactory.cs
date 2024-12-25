using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;

namespace Viamus.Fast.Sharp.Database.Postgresql.Factories;

public interface IRepositoryFactory: IAsyncDisposable
{
    IRepository<TEntity> Get<TEntity>(DbContext context) where TEntity : Entity;
}