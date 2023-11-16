using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models
{
    public class DailyForecastWeather
    {
        [JsonPropertyName("city")]
        public ForecastCity City { get; set; } = null!;

        [JsonPropertyName("cod")]
        public string Code { get; set; } = null!;

        [JsonPropertyName("list")]
        public IEnumerable<DailyForecastWeatherData> WeatherData { get; set; } = null!;
    }
}