using WeatherForecastClient;

Console.Write("Search weather for city: ");
string city = Console.ReadLine() ?? string.Empty;

var weatherForecastService = new WeatherForecastService();
var response = await weatherForecastService.SearchAsync(city);

Console.WriteLine(response.Content?.Name);