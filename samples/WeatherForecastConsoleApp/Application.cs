using WeatherForecastClient;

namespace WeatherForecastConsoleApp;

public class Application
{
    private readonly IWeatherForecastService weatherForecastClient;

    public Application(IWeatherForecastService weatherForecastClient)
    {
        this.weatherForecastClient = weatherForecastClient;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Search the weather for your city: ");
        var city = Console.ReadLine() ?? string.Empty;

        var weatherResponse = await weatherForecastClient.SearchAsync(city);
        if (weatherResponse.IsSuccessStatusCode)
        {
            Console.WriteLine(weatherResponse.Content.Name);
        }

        Console.ReadLine();
    }
}