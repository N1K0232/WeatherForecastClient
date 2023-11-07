using Polly.Registry;

namespace WeatherForecastClient.Handlers;

internal class TransientErrorDelegatingHandler : DelegatingHandler
{
    private readonly ResiliencePipelineProvider<string> pipelineProvider;

    public TransientErrorDelegatingHandler(ResiliencePipelineProvider<string> pipelineProvider)
    {
        this.pipelineProvider = pipelineProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var pipeline = pipelineProvider.GetPipeline<HttpResponseMessage>("http");
        return await pipeline.ExecuteAsync(async token => await base.SendAsync(request, token), cancellationToken);
    }
}