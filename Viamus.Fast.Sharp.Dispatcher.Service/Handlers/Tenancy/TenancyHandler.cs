using Viamus.Fast.Sharp.Database.Abstractions;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Shared.Models;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Dtos;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Interfaces;

namespace Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy;

public class TenancyHandler : ITenancyHandler
{
    private readonly ILogger<TenancyHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public TenancyHandler(IUnitOfWork unitOfWork, ILogger<TenancyHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<HandlerResponse<TenancyDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.GetRepository<Database.Entities.Tenancy>().GetAsync(id, cancellationToken);

        var response = TenancyDto.MapFrom(entity);

        return response is not null
            ? HandlerResponse<TenancyDto>.Success(response)
            : HandlerResponse<TenancyDto>.NotFound();
    }
}