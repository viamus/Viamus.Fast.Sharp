using FluentValidation;
using Viamus.Fast.Sharp.Dispatcher.Public.Domain.Entities;

namespace Viamus.Fast.Sharp.Dispatcher.Public.Domain.Validators;

public class HostingValidator: AbstractValidator<Hosting>
{
    public HostingValidator()
    {
        RuleFor(h=> h.Name).NotEmpty().MaximumLength(100);
        RuleFor(h=> h.Description).MaximumLength(255);
    }
}