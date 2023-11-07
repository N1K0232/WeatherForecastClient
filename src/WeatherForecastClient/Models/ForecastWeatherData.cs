using System.Text.Json.Serialization;
using WeatherForecastClient.Converters;

namespace WeatherForecastClient.Models;

public class ForecastWeatherData
{
    [JsonPropertyName("dt")]
    [JsonConverter(typeof(UnixToDateTimeConverter))]
    public DateTime Date { get; set; }

    [JsonPropertyName("main")]
    public ForecastWeatherDetail WeatherDetail { get; set; } = null!;

    [JsonPropertyName("weather")]
    public IEnumerable<WeatherCondition> Conditions { get; set; } = null!;

    [JsonPropertyName("clouds")]
    public Clouds Clouds { get; set; } = null!;

    [JsonPropertyName("wind")]
    public Wind Wind { get; set; } = null!;
}