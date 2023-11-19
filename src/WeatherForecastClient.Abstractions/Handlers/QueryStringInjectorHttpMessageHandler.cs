using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WeatherForecastClient.Handlers
{
    public class QueryStringInjectorHttpMessageHandler : DelegatingHandler
    {
        public IDictionary<string, string> Parameters { get; }

        public QueryStringInjectorHttpMessageHandler(Dictionary<string, string>? parameters = null, HttpMessageHandler? innerHandler = null)
              : base(innerHandler ?? new HttpClientHandler())
        {
            Parameters = parameters ?? new Dictionary<string, string>();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            NameValueCollection parameters = HttpUtility.ParseQueryString(request.RequestUri!.Query);

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
}