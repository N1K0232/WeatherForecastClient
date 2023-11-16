using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models
{
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int Cloudiness { get; set; }
    }
}