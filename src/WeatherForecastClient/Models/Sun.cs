using System.Text.Json.Serialization;
using WeatherForecastClient.Converters;

namespace WeatherForecastClient.Models;

public class Sun
{
    [JsonPropertyName("country")]
    public string Country { get; set; } = null!;

    [JsonPropertyName("sunrise")]
    [JsonConverter(typeof(UnixToDateTimeConverter))]
    public DateTime Sunrise { get; set; }

    [JsonPropertyName("sunset")]
    [JsonConverter(typeof(UnixToDateTimeConverter))]
    public DateTime Sunset { get; set; }
}