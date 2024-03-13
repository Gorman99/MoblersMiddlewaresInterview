using System.Text;

namespace Moblers.Middlewares.Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(ILogger<RequestLoggingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    
     public async Task Invoke(HttpContext context)
    {
        var request = await FormatRequest(context.Request);

        _logger.LogInformation(request);
        
        await _next(context);
      
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        request.Body.Position = 0;
        var sb = new StringBuilder();
        foreach (var requestHeader in request.Headers)
        {
            sb.Append($"{requestHeader.Key}={requestHeader.Value}");
            sb.AppendLine();
        }

        var log = $"Request:\r\nUrl: {request.Scheme}://{request.Host}{request.Path} {request.QueryString} " +
                  $"\r\nMethod: {request.Method}" +
                  $"\r\nCLIENT IP: {request.HttpContext.Connection.RemoteIpAddress}" +
                  $"\r\nCLIENT Port: {request.HttpContext.Connection.RemotePort}" +
                  $"\r\nConnection ID: {request.HttpContext.Connection.Id}" +
                  $"\r\nHeaders:\r\n{sb}" +
                  $"\r\nBody:\r\n{bodyAsText}" +
                  "\r\n";
        return log;
    }

    
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}