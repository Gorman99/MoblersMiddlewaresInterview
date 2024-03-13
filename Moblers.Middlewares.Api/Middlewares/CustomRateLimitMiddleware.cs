using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using Moblers.Middlewares.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Moblers.Middlewares.Api.Middlewares;

public class CustomRateLimitMiddleware :IpRateLimitMiddleware
{
    public CustomRateLimitMiddleware(RequestDelegate next, IProcessingStrategy processingStrategy, IOptions<IpRateLimitOptions> options, IIpPolicyStore policyStore, IRateLimitConfiguration config, ILogger<IpRateLimitMiddleware> logger) : base(next, processingStrategy, options, policyStore, config, logger)
    {
        
    }
    
    public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
    {
            
          
        var str = $"API calls quota exceeded! maximum admitted {rule.Limit} per {rule.Period}.";
        
        var response = new ApiResponse<object>(
            Message: str,
            Code: 500);
        var result = JsonConvert.SerializeObject(response,new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });
        httpContext.Response.Headers["Retry-After"] = retryAfter;
        httpContext.Response.StatusCode = 429;
        httpContext.Response.ContentType = "application/json";

        return httpContext.Response.WriteAsync(result);

    }
}