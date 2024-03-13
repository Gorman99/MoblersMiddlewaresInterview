using Microsoft.AspNetCore.Mvc;
using Moblers.Middlewares.Api.Models;

namespace Moblers.Middlewares.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public ApiResponse<IEnumerable<WeatherForecast>>  Get()
    {
        var data =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        return new ApiResponse<IEnumerable<WeatherForecast>>("Success", 200, data);
    }
    
    [HttpGet("exception")]
    public ActionResult  GetException()
    {
        throw new Exception("An error occurred in the API endpoint.");
    }

}