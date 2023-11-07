using Microsoft.Extensions.Caching.Memory;
using Refit;
using WeatherForecastClient.Models;

namespace WeatherForecastClient;

internal class WeatherForecastCache : IWeatherForecastCache
{
    private readonly IMemoryCache cache;

    public WeatherForecastCache(IMemoryCache cache)
    {
        this.cache = cache;
    }

    public Task DeleteAsync(string city, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        cache.Remove($"weather-{city}");
        return Task.CompletedTask;
    }

    public Task<ApiResponse<CurrentWeather>?> GetAsync(string city, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var key = $"weather-{city}";

        var response = cache.Get<ApiResponse<CurrentWeather>>(key);
        return Task.FromResult(response);
    }

    public Task SetAsync(string city, ApiResponse<CurrentWeather> response, TimeSpan expiration, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var key = $"weather-{city}";

        cache.Set(key, response, expiration);
        return Task.CompletedTask;
    }
}