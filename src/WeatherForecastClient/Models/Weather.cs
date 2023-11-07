namespace WeatherForecastClient.Models;

public class Weather
{
    public Weather(CurrentWeather currentWeather)
    {
        CityName = currentWeather.Name;
        Condition = currentWeather.Conditions.First().Description;
        ConditionIcon = currentWeather.Conditions.First().ConditionIcon;
        Temperature = currentWeather.Detail.Temperature;
    }

    public string CityName { get; }

    public string Condition { get; }

    public string ConditionIcon { get; }

    public string ConditionIconUrl => $"https://openweathermap.org/img/w/{ConditionIcon}.png";

    public decimal Temperature { get; }
}