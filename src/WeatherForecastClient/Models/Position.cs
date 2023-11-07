using System.Text.Json.Serialization;

namespace WeatherForecastClient.Models;

public class Position
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
}