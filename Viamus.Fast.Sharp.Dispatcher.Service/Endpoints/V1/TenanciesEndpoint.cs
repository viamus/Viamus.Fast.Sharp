using Microsoft.AspNetCore.Mvc;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Shared.Models;
using Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Tenancy.Interfaces;

namespace Viamus.Fast.Sharp.Dispatcher.Service.Endpoints.V1;

public static class TenanciesEndpoint
{
    private static IResult MapResponse<T>(HandlerResponse<T> handlerResponse) where T : class => handlerResponse switch
    {
        { Type: HandlerResponseType.Success } => Results.Ok(handlerResponse.Result),
        { Type: HandlerResponseType.NotFound } => Results.NotFound(),
        { Type: HandlerResponseType.BadRequest } => Results.Problem
        (
            "An Business logic error has occurred",
            "TenancyHandler",
            400,
            "An error occured",
            handlerResponse?.Exception?.Message
        ),
        _ => throw new ArgumentOutOfRangeException(nameof(handlerResponse), handlerResponse, null)
    };

    public static WebApplication AddTenancyEndpoints(this WebApplication app)
    {
        app.MapGet("/api/tenancies/{id:guid}",
            async (Guid id, CancellationToken cancellationToken, [FromServices]ITenancyHandler handler) =>
                MapResponse(await handler.GetByIdAsync(id, cancellationToken))).WithName("GetTenancyById");

        return app;
    }
}