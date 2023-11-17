using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecastClient.Extensions;

namespace WeatherForecastClient.WpfApp;

public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddWeatherClient(options =>
        {
            options.ApiKey = "";
        });

        services.AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var window = host.Services.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }
}