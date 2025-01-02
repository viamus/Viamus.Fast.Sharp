using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Dispatcher.Public.Domain.Entities;
using Viamus.Fast.Sharp.Dispatcher.Public.Infrastructure.Database.Mapping;

namespace Viamus.Fast.Sharp.Dispatcher.Public.Infrastructure.Database;

public class DispatcherContext(DbContextOptions<DispatcherContext> options) : DbContext(options)
{
    public DbSet<Hosting> Tenancies { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HostingMapping());
    }
}