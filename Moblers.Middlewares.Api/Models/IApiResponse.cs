namespace Moblers.Middlewares.Api.Models;

public interface IApiResponse<out T>
{
    public string Message { get; }
    
    public int Code { get; }
    
    public string SubCode { get; }
    
    public T? Data { get; }
    
    public IEnumerable<ErrorResponse>? Errors { get; }
    
    public static T? Default { get; } = default;
}

public sealed record ApiResponse<T>(
    string Message,
    int Code,
    T? Data = default,
    string SubCode = "0",
    IEnumerable<ErrorResponse>? Errors = default) : IApiResponse<T>
{
}
public sealed record ErrorResponse(string Field, string ErrorMessage);