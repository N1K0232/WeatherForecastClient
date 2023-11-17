using System;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using WeatherForecastClient.Handlers;

namespace WeatherForecastClient.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly JsonSerializerOptions defaultJsonSerializerOptions;

        static ServiceCollectionExtensions()
        {
            defaultJsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public static IServiceCollection AddWeatherClient(this IServiceCollection services,
            Action<WeatherForecastClientOptions> configuration,
            JsonSerializerOptions? jsonSerializerOptions = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var weatherForecastClientOptions = new WeatherForecastClientOptions();
            configuration.Invoke(weatherForecastClientOptions);

            services.AddWeatherClientCore(weatherForecastClientOptions, jsonSerializerOptions);
            return services;
        }

        public static IServiceCollection AddWeatherClient(this IServiceCollection services,
            IConfiguration configuration,
            string sectionName = nameof(WeatherForecastClientOptions),
            JsonSerializerOptions? jsonSerializerOptions = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (sectionName == null)
            {
                throw new ArgumentNullException(nameof(sectionName));
            }

            var section = configuration.GetSection(sectionName);
            var weatherForecastClientOptions = section.Get<WeatherForecastClientOptions>();

            services.AddWeatherClientCore
            (
                weatherForecastClientOptions ?? throw new InvalidOperationException("settings are required"),
                jsonSerializerOptions
            );

            return services;
        }

        private static IServiceCollection AddWeatherClientCore(this IServiceCollection services,
            WeatherForecastClientOptions weatherForecastClientOptions,
            JsonSerializerOptions? jsonSerializerOptions)
        {
            var jsonOptions = jsonSerializerOptions ?? defaultJsonSerializerOptions;

            services.AddRefitClient<IOpenWeatherMapApi>(new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(jsonOptions)
            })
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5");
            })
            .ConfigurePrimaryHttpMessageHandler(_ =>
            {
                var handler = new QueryStringInjectorHttpMessageHandler();
                handler.Parameters.Add("units", "metric");
                handler.Parameters.Add("APPID", weatherForecastClientOptions.ApiKey);

                var language = weatherForecastClientOptions.ResponseLanguage ?? Thread.CurrentThread.CurrentCulture.Name;
                handler.Parameters.Add("lang", language);

                return handler;
            })
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            }));

            services.AddMemoryCache();
            services.AddSingleton<IWeatherForecastCache, WeatherForecastCache>();

            services.AddScoped<IGeolocalizationService, GeolocalizationService>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            return services;
        }
    }
}