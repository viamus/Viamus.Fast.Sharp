using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Database.EntityFramework;
using Viamus.Fast.Sharp.Database.EntityFramework.Factories;
using Viamus.Fast.Sharp.Dispatcher.Service.Database;

namespace Viamus.Fast.Sharp.Dispatcher.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<DispatcherContext>(opt =>
            opt.UseNpgsql
            (
                configuration.GetConnectionString("DefaultConnection"),
                o => o
                    .EnableRetryOnFailure(3, TimeSpan.FromMilliseconds(100), null)
            ));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>(builder =>
        {
            var dbContext = builder.GetService<DispatcherContext>();
            return new UnitOfWork(dbContext!, new RepositoryFactory());
        });

        return services;
    }
}