using FluentValidation.Results;
using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Dispatcher.Public.Domain.Validators;

namespace Viamus.Fast.Sharp.Dispatcher.Public.Domain.Entities;

public class Hosting : Entity
{
    public Hosting(string name, string? description)
    {
        Name = name;
        Description = description;
        IsActive = true;
    }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public ValidationResult Validate() => new HostingValidator().Validate(this);
}