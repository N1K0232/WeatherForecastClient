using System.Threading;
using System.Threading.Tasks;
using Refit;
using WeatherForecastClient.Models;

namespace WeatherForecastClient
{
    public interface IOpenWeatherMapApi
    {
        [Get("/weather?units=metric")]
        Task<ApiResponse<CurrentWeather>> SearchAsync([AliasAs("q")] string city, CancellationToken cancellationToken = default);
    }
}