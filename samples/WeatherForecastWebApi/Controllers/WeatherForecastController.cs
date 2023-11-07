using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastClient;

namespace WeatherForecastWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastClient weatherForecastClient;

    public WeatherForecastController(IWeatherForecastClient weatherForecastClient)
    {
        this.weatherForecastClient = weatherForecastClient;
    }

    [HttpGet("search/{city}")]
    public async Task<IActionResult> Search(string city)
    {
        var response = await weatherForecastClient.SearchAsync(city);
        if (response.IsSuccess)
        {
            return Ok(response.CurrentWeather);
        }

        return StatusCode(response.Error!.Code, response.Error!.Message);
    }
}