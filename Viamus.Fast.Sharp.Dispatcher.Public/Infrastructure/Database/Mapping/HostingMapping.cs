using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viamus.Fast.Sharp.Dispatcher.Public.Domain.Entities;

namespace Viamus.Fast.Sharp.Dispatcher.Public.Infrastructure.Database.Mapping;

public class HostingMapping : IEntityTypeConfiguration<Hosting>
{
    public void Configure(EntityTypeBuilder<Hosting> builder)
    {
        builder.ToTable("hosting");

        builder.HasKey(p => p.Id);
        
        builder.HasQueryFilter(p=> p.Deleted == false);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("varchar")
            .HasMaxLength(255);

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();
        
        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp")
            .IsRequired();
        
        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp");
        
        builder.Property(p => p.Deleted)
            .HasColumnName("deleted")
            .HasColumnType("boolean")
            .HasDefaultValue(false)
            .IsRequired();
    }
}