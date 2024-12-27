namespace Viamus.Fast.Sharp.Dispatcher.Service.Handlers.Shared.Models;

public enum HandlerResponseType
{
    Success,
    NotFound,
    ServerError,
    BadRequest,
}

public record HandlerResponse<T>(T? Result, HandlerResponseType Type, Exception? Exception = null) where T: class
{
    public static HandlerResponse<T> Success(T result) => new(result, HandlerResponseType.Success);
    public static HandlerResponse<T> NotFound() => new(null, HandlerResponseType.NotFound);
    public static HandlerResponse<T> ServerError(Exception exception) => new(null, HandlerResponseType.ServerError, exception);
    public static HandlerResponse<T> BadRequest(Exception exception) => new(null, HandlerResponseType.BadRequest, exception);
}