using Microsoft.EntityFrameworkCore;
using Viamus.Fast.Sharp.Dispatcher.Service.Database.Entities;
using Viamus.Fast.Sharp.Dispatcher.Service.Database.Mapping;

namespace Viamus.Fast.Sharp.Dispatcher.Service.Database;

public class DispatcherContext(DbContextOptions<DispatcherContext> options) : DbContext(options)
{
    public DbSet<Tenancy> Tenancies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TenancyMapping());
    }
}