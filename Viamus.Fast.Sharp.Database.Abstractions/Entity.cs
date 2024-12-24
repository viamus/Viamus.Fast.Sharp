namespace Viamus.Fast.Sharp.Database.Abstractions;

/// <summary>
/// Abstract Class <c>Entity</c> base database entity class.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; }

    public DateTime CreatedAt { get; protected set; }

    public DateTime? UpdatedAt { get; protected set; }

    public bool Deleted { get; protected set; }

    public Guid ConcurrentFlag { get; protected set; } = Guid.NewGuid();

    protected Entity()
    {
        Id = Guid.CreateVersion7();
        Deleted = false;
    }

    protected Entity(Guid id)
    {
        Id = Guid.CreateVersion7();
        Deleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Delete()
    {
        Deleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}