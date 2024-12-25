using System;
using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;

namespace Viamus.Fast.Sharp.Database.EntityFramework.Factories;

public interface IRepositoryFactory: IAsyncDisposable
{
    IRepository<TEntity> Get<TEntity>(DbContext context) where TEntity : Entity;
}