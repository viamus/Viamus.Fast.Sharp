using Microsoft.Extensions.Caching.Hybrid;
using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Shared.Models;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Dtos;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Interfaces;

namespace Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy;

public class TenancyHandler : ITenancyHandler
{
    private readonly HybridCache _caching;
    private readonly ILogger<TenancyHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public TenancyHandler(IUnitOfWork unitOfWork, ILogger<TenancyHandler> logger, HybridCache caching)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _caching = caching;
    }

    public async Task<HandlerResponse<TenancyDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _caching.GetOrCreateAsync
        (
            "entity_tenancy_get_by_id_" + id,
            async (token) =>
                await _unitOfWork.GetRepository<Database.Entities.Tenancy>().GetAsync(id, token),
            new HybridCacheEntryOptions() { Expiration = TimeSpan.FromMinutes(5) },
            cancellationToken: cancellationToken
        );
        
        var response = TenancyDto.MapFrom(entity);

        return response is not null
            ? HandlerResponse<TenancyDto>.Success(response)
            : HandlerResponse<TenancyDto>.NotFound();
    }
}