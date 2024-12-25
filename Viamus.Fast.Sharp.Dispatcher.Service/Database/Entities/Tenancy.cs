using System.Diagnostics.CodeAnalysis;
using Viamus.Fast.Sharp.Database.Abstractions;

namespace Viamus.Fast.Sharp.Dispatcher.Service.Database.Entities;

[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global")]
public class Tenancy : Entity
{
    [SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
    public Tenancy(string name, string? description)
    {
        Name = name;
        Description = description;
        IsActive = true;
    }
    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}