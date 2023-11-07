using WeatherForecastClient.Models;

namespace WeatherForecastClient;

public interface IWeatherForecastClient
{
    Task<WeatherResponse> SearchAsync(string city, CancellationToken cancellationToken = default);
}