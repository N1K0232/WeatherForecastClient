using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models;

public class Wind
{
    [JsonPropertyName("speed")]
    public decimal Speed { get; set; }

    [JsonPropertyName("deg")]
    public double Degree { get; set; }
}