﻿using Refit;
using WeatherForecastClient.Models;

namespace WeatherForecastClient;

public interface IOpenWeatherMapApiClient
{
    [Get("/weather?units=metric")]
    Task<ApiResponse<CurrentWeather>> SearchAsync([AliasAs("q")] string city, CancellationToken cancellationToken = default);
}