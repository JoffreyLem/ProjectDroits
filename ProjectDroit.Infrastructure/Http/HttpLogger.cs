using System.Net.Http;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace ProjectDroit.Infrastructure.Http;

public class HttpLogger : IHttpClientLogger
{
    private readonly ILogger<HttpLogger> _logger;

    public HttpLogger(ILogger<HttpLogger> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public object? LogRequestStart(HttpRequestMessage request)
    {
        _logger.LogInformation("➡️ HTTP {Method} {Uri} - Sending request", request.Method, request.RequestUri);


        return null;
    }

    public void LogRequestStop(object? context, HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed)
    {
        _logger.LogInformation("✅ HTTP {Method} {Uri} completed in {Elapsed}ms with status {StatusCode}",
            request.Method, request.RequestUri, elapsed.TotalMilliseconds, (int)response.StatusCode);

    }

    public void LogRequestFailed(
        object? context,
        HttpRequestMessage request,
        HttpResponseMessage? response,
        Exception exception,
        TimeSpan elapsed)
    {
        _logger.LogError(exception,
            "❌ HTTP {Method} {Uri} failed after {Elapsed}ms. Status: {StatusCode}",
            request.Method,
            request.RequestUri,
            elapsed.TotalMilliseconds,
            response?.StatusCode.ToString() ?? "No response");
    }
}
