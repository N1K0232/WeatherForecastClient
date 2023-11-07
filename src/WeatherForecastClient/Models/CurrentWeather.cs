using System.Text.Json.Serialization;
using WeatherForecastClient.Converters;

namespace WeatherForecastClient.Models;

public class CurrentWeather
{
    [JsonPropertyName("coord")]
    public Position Position { get; set; } = null!;

    [JsonPropertyName("weather")]
    public IEnumerable<WeatherCondition> Conditions { get; set; } = null!;

    [JsonPropertyName("main")]
    public CurrentWeatherDetail Detail { get; set; } = null!;

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; } = null!;

    [JsonPropertyName("clouds")]
    public Clouds Clouds { get; set; } = null!;

    [JsonPropertyName("sys")]
    public Sun Sun { get; set; } = null!;

    [JsonPropertyName("dt")]
    [JsonConverter(typeof(UnixToDateTimeConverter))]
    public DateTime Date { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("cod")]
    public int Code { get; set; }
}