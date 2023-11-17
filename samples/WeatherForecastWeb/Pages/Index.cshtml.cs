using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherForecastClient;
using WeatherForecastClient.Models;

namespace WeatherForecastWeb.Pages;
public class IndexModel : PageModel
{
    private readonly IWeatherForecastService weatherForecastService;

    public IndexModel(IWeatherForecastService weatherForecastService)
    {
        this.weatherForecastService = weatherForecastService;
    }

    [BindProperty(SupportsGet = true)]
    public string City { get; set; }

    public Weather Weather { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (!string.IsNullOrWhiteSpace(City))
        {
            var response = await weatherForecastService.SearchAsync(City);
            Weather = response.IsSuccessStatusCode ? new Weather(response.Content) : null;
        }

        return Page();
    }
}