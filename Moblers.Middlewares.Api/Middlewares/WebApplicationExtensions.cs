using System.Net;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Moblers.Middlewares.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Moblers.Middlewares.Api.Middlewares;
public static class WebApplicationExtensions
{
    public static void ExceptionHandler(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature is null)
                {
                    return;
                }

                logger.LogError(contextFeature.Error, "Unhadled Exception Occured");

                
                var response = new ApiResponse<object>(
                    Message: "Ooops, something really bad happened. Please try again later.",
                    Code: 500);

                var respJson = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });

                context.Response.ContentLength = respJson.Length;

                await context.Response.WriteAsync(respJson, Encoding.UTF8);

            });
        });
    }
}