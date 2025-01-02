using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Database.EntityFramework;
using Viamus.Fast.Sharp.Database.EntityFramework.Factories;
using Viamus.Fast.Sharp.Dispatcher.Public.Infrastructure.Database;

namespace Viamus.Fast.Sharp.Dispatcher.Public;

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
    
    public static IServiceCollection AddHybridCaching(this IServiceCollection services, IConfiguration configuration)
    {
#pragma warning disable EXTEXP0018
        services.AddHybridCache();
#pragma warning restore EXTEXP0018
        return services;
    }
    
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        

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