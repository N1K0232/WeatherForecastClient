using Refit;
using WeatherForecastClient.Models;

namespace WeatherForecastClient;

public interface IWeatherForecastCache
{
    Task DeleteAsync(string city, CancellationToken cancellationToken = default);

    Task<ApiResponse<CurrentWeather>?> GetAsync(string city, CancellationToken cancellationToken = default);

    Task SetAsync(string city, ApiResponse<CurrentWeather> response, TimeSpan expiration, CancellationToken cancellationToken = default);
}