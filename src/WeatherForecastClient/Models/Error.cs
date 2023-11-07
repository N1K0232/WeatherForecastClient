using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models;

public class Error
{
    [JsonPropertyName("cod")]
    public int Code { get; set; }

    public string Message { get; set; } = null!;
}