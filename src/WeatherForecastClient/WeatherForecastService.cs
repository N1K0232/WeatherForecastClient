using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using WeatherForecastClient.Models;

namespace WeatherForecastClient
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private HttpClient httpClient;

        public WeatherForecastService(string apiKey)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("")
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ApiResponse<CurrentWeather>> SearchAsync(string city, CancellationToken cancellationToken = default)
        {
            var openWeatherMapApi = RestService.For<IOpenWeatherMapApi>("https://api.openweathermap.org/data/2.5");
            var response = await openWeatherMapApi.SearchAsync(city, cancellationToken);

            return response;
        }
    }
}