using System.Windows;
using System.Windows.Media.Imaging;
using WeatherForecastClient.Models;

namespace WeatherForecastClient.WpfApp;

public partial class MainWindow : Window
{
    private readonly IWeatherForecastService weatherForecastService;

    public MainWindow(IWeatherForecastService weatherForecastService)
    {
        this.weatherForecastService = weatherForecastService;
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        ConditionCityTextBlock.Text = null;
        ConditionImage.Source = null;
        ConditionTextBlock.Text = null;
        TemperatureTextBlock.Text = null;

        if (!string.IsNullOrWhiteSpace(CityTextBox.Text))
        {
            var response = await weatherForecastService.SearchAsync(CityTextBox.Text);

            if (response.IsSuccessStatusCode)
            {
                var weather = new Weather(response.Content);
                ConditionCityTextBlock.Text = weather.CityName;
                ConditionImage.Source = new BitmapImage(new Uri(weather.ConditionIconUrl));
                ConditionTextBlock.Text = weather.Condition;
                TemperatureTextBlock.Text = $"{weather.Temperature} °C";
            }
            else
            {
                var error = await response.Error.GetContentAsAsync<Error>();
                MessageBox.Show($"Unable to retrieve weather codition for {CityTextBox.Text}: {error.Message}.", "Weather Client", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}