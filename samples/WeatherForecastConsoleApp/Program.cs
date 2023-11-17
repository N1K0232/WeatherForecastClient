using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecastClient.Extensions;
using WeatherForecastConsoleApp;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(ConfigureServices)
    .Build();

var application = host.Services.GetRequiredService<Application>();
await application.ExecuteAsync();

void ConfigureServices(HostBuilderContext hostingContext, IServiceCollection services)
{
    services.AddWeatherClient(options =>
    {
        options.ApiKey = "";
    });

    services.AddSingleton<Application>();
}