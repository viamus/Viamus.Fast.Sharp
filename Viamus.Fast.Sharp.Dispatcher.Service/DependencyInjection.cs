using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Database.EntityFramework;
using Viamus.Fast.Sharp.Database.EntityFramework.Factories;
using Viamus.Fast.Sharp.Dispatcher.Service.Database;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Shared;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Interfaces;

namespace Viamus.Fast.Sharp.Dispatcher.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration.GetConnectionString("DefaultConnection"));
        
        services.AddDbContextPool<DispatcherContext>(opt =>
            opt.UseNpgsql
            (
                configuration.GetConnectionString("DefaultConnection")!,
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
    
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<ITenancyHandler, TenancyHandler>();

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration.GetConnectionString("DefaultConnection"));
        
        services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);

        return services;
    }
}