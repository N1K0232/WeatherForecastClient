using System.Windows;

namespace WeatherForecastClient.WpfApp;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var weatherForecastService = new WeatherForecastService();
        var window = new MainWindow(weatherForecastService);

        window.Show();
        base.OnStartup(e);
    }
}