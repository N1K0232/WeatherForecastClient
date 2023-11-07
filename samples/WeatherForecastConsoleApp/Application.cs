using WeatherForecastClient;

namespace WeatherForecastConsoleApp;

public class Application
{
    private readonly IWeatherForecastClient weatherForecastClient;

    public Application(IWeatherForecastClient weatherForecastClient)
    {
        this.weatherForecastClient = weatherForecastClient;
    }

    public async Task ExecuteAsync()
    {
        Console.Write("Search the weather for your city: ");
        var city = Console.ReadLine() ?? string.Empty;

        var weatherResponse = await weatherForecastClient.SearchAsync(city);
        if (weatherResponse.IsSuccess)
        {
            Console.WriteLine(weatherResponse.CurrentWeather!.Name);
        }

        Console.ReadLine();
    }
}