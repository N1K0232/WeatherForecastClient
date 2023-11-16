using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models
{
    public class Error
    {
        [JsonPropertyName("cod")]
        public string Code { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}