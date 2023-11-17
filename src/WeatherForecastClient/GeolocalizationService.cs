using Microsoft.Extensions.Logging;

namespace WeatherForecastClient
{
    public class GeolocalizationService : IGeolocalizationService
    {
        private readonly ILogger<GeolocalizationService> logger;

        public GeolocalizationService(ILogger<GeolocalizationService> logger)
        {
            this.logger = logger;
        }
    }
}