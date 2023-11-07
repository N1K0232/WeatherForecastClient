using System.Web;

namespace WeatherForecastClient.Handlers;

internal class QueryStringInjectorHttpMessageHandler : DelegatingHandler
{
    public Dictionary<string, string> Parameters { get; }

    public QueryStringInjectorHttpMessageHandler(Dictionary<string, string> parameters = null, HttpMessageHandler innerHandler = null)
          : base(innerHandler ?? new HttpClientHandler())
    {
        Parameters = parameters ?? new Dictionary<string, string>();
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var parameters = HttpUtility.ParseQueryString(request.RequestUri!.Query);

        foreach (var parameter in Parameters)
        {
            parameters.Add(parameter.Key, parameter.Value);
        }

        var uriBuilder = new UriBuilder(request.RequestUri!)
        {
            Query = parameters.ToString()
        };

        request.RequestUri = new Uri(uriBuilder.ToString());
        return base.SendAsync(request, cancellationToken);
    }
}