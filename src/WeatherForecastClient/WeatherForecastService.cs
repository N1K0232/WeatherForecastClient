using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Refit;
using WeatherForecastClient.Models;

namespace WeatherForecastClient
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IOpenWeatherMapApi openWeatherMapApi;
        private readonly IGeolocalizationService geolocalizationService;
        private readonly IWeatherForecastCache cache;
        private readonly ILogger<WeatherForecastService> logger;

        public WeatherForecastService(IOpenWeatherMapApi openWeatherMapApi,
            IGeolocalizationService geolocalizationService,
            IWeatherForecastCache cache,
            ILogger<WeatherForecastService> logger)
        {
            this.openWeatherMapApi = openWeatherMapApi;
            this.geolocalizationService = geolocalizationService;
            this.cache = cache;
            this.logger = logger;
        }

        public async Task<ApiResponse<CurrentWeather>> SearchAsync(string city, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Getting weather condition for {city}...", city);

            var cacheResponse = await cache.GetAsync(city, cancellationToken);
            if (cacheResponse != null)
            {
                logger.LogDebug("Retrieving value for {City} from cache", city);
                return cacheResponse;
            }

            var response = await openWeatherMapApi.SearchAsync(city, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Unable to retrieve weather condition: {StatusCode}", response.StatusCode);
            }
            else
            {
                await cache.SetAsync(city, response, TimeSpan.FromHours(1), cancellationToken);
            }

            return response;
        }
    }
}