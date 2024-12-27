using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Shared;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Shared.Models;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Dtos;

namespace Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Interfaces;

public interface ITenancyHandler
{
     Task<HandlerResponse<TenancyDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}