using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models;

public class ForecastWeather
{
    [JsonPropertyName("city")]
    public ForecastCity City { get; set; } = null!;

    [JsonPropertyName("cod")]
    public string Code { get; set; } = null!;

    [JsonPropertyName("list")]
    public IEnumerable<ForecastWeatherData> WeatherData { get; set; } = null!;
}