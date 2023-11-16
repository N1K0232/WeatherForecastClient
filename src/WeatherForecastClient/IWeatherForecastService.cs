﻿using Refit;
using WeatherForecastClient.Models;

namespace WeatherForecastClient;

public interface IWeatherForecastService
{
    Task<ApiResponse<CurrentWeather>> SearchAsync(string city, CancellationToken cancellationToken = default);
}