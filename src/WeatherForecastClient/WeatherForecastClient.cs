using Microsoft.Extensions.Logging;
using WeatherForecastClient.Models;

namespace WeatherForecastClient;

internal class WeatherForecastClient : IWeatherForecastClient
{
    private readonly IOpenWeatherMapApiClient client;
    private readonly IWeatherForecastCache cache;
    private readonly ILogger<WeatherForecastClient> logger;

    public WeatherForecastClient(IOpenWeatherMapApiClient client,
        IWeatherForecastCache cache,
        ILogger<WeatherForecastClient> logger)
    {
        this.client = client;
        this.cache = cache;
        this.logger = logger;
    }

    public async Task<WeatherResponse> SearchAsync(string city, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting weather condition for {city}...", city);

        var cacheWeatherResponse = await cache.GetAsync(city, cancellationToken);
        if (cacheWeatherResponse != null)
        {
            var content = cacheWeatherResponse.Content!;
            return WeatherResponse.Ok(content);
        }

        var weatherResponse = await client.SearchAsync(city, cancellationToken);
        if (weatherResponse.IsSuccessStatusCode)
        {
            await cache.SetAsync(city, weatherResponse, TimeSpan.FromHours(1), cancellationToken);
            return WeatherResponse.Ok(weatherResponse.Content);
        }
        else
        {
            logger.LogError("Unable to retrieve weather condition: {StatusCode}", weatherResponse.StatusCode);

            var error = await weatherResponse.Error.GetContentAsAsync<Error>();
            return WeatherResponse.Fail(error);
        }
    }
}