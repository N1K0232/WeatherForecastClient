using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastClient;
using WeatherForecastClient.Models;

namespace WeatherForecastWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService weatherForecastClient;

    public WeatherForecastController(IWeatherForecastService weatherForecastClient)
    {
        this.weatherForecastClient = weatherForecastClient;
    }

    [HttpGet("search/{city}")]
    public async Task<IActionResult> Search(string city)
    {
        var response = await weatherForecastClient.SearchAsync(city);
        if (response.IsSuccessStatusCode)
        {
            return Ok(response.Content);
        }

        var error = await response.Error.GetContentAsAsync<Error>();
        return StatusCode(Convert.ToInt32(error!.Code), error.Message);
    }
}