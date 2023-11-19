using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models
{
    public class WeatherCondition
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("main")]
        public string Condition { get; set; } = null!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("icon")]
        public string ConditionIcon { get; set; } = null!;
    }
}