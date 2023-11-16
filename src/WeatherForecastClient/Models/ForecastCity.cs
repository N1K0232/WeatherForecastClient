using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models
{
    public class ForecastCity
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("country")]
        public string Country { get; set; } = null!;
    }
}