namespace Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Dtos;

public record TenancyDto
{
    private TenancyDto(Guid Id, string Name, string? Description, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt)
    {
        this.Id = Id;
        this.Name = Name;
        this.Description = Description;
        this.IsActive = IsActive;
        this.CreatedAt = CreatedAt;
        this.UpdatedAt = UpdatedAt;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }

    public static TenancyDto? MapFrom(Database.Entities.Tenancy? tenancy)
    {
        return tenancy is null
            ? null
            : new TenancyDto(tenancy.Id, tenancy.Name, tenancy.Description, tenancy.IsActive, tenancy.CreatedAt,
                tenancy.UpdatedAt);
    }
}