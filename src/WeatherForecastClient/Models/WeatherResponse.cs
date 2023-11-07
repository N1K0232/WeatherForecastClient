namespace WeatherForecastClient.Models;

public class WeatherResponse
{
    private WeatherResponse(CurrentWeather? currentWeather, Error? error)
    {
        CurrentWeather = currentWeather;
        Error = error;
    }

    public CurrentWeather? CurrentWeather { get; }

    public Error? Error { get; }

    public bool IsSuccess => CurrentWeather is not null && Error is null;

    public static WeatherResponse Ok(CurrentWeather? currentWeather) => new(currentWeather, null);

    public static WeatherResponse Fail(Error? error) => new(null, error);
}