using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using ProjectDroit.Infrastructure.Http.Gemini.Configuration;


namespace ProjectDroit.Infrastructure.Http.Gemini.Handlers;

public class AddApiKeyQueryParameterHandler(IOptions<GeminiSettings> geminiSettings) : DelegatingHandler
{
    private string ApiKey => geminiSettings.Value.ApiKey;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var currentUri = request.RequestUri;


        string parameterKey = "api_key";

        var newUri = QueryHelpers.AddQueryString(currentUri.OriginalString, parameterKey, ApiKey);

        request.RequestUri = new Uri(newUri, UriKind.Absolute);

        return await base.SendAsync(request, cancellationToken);
    }
}