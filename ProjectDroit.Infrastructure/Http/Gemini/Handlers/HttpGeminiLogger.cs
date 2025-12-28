using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace ProjectDroit.Infrastructure.Http.Gemini.Handlers
{
    public class HttpGeminiLogger(ILogger<HttpGeminiLogger> logger) : IHttpClientLogger
    {
        private readonly ILogger<HttpGeminiLogger> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private const string ApiKeyName = "key";

        private string GetSanitizedUriString(HttpRequestMessage request)
        {
            if (request.RequestUri == null) return "(URI non disponible)";
            return UriExtensions.RemoveQueryStringByKey(request.RequestUri.OriginalString, ApiKeyName);
        }

        public object? LogRequestStart(HttpRequestMessage request)
        {
            string sanitizedUri = GetSanitizedUriString(request);
            _logger.LogInformation("➡️ HTTP {Method} {Uri} - Sending request",
                request.Method,
                sanitizedUri); 
            return null;
        }

        public void LogRequestStop(object? context, HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed)
        {
            string sanitizedUri = GetSanitizedUriString(request);
            _logger.LogInformation("✅ HTTP {Method} {Uri} completed in {Elapsed}ms with status {StatusCode}",
                request.Method,
                sanitizedUri,
                elapsed.TotalMilliseconds,
                (int)response.StatusCode);
        }

        public void LogRequestFailed(
            object? context,
            HttpRequestMessage request,
            HttpResponseMessage? response,
            Exception exception,
            TimeSpan elapsed)
        {
            string sanitizedUri = GetSanitizedUriString(request);
            _logger.LogError(exception,
                "❌ HTTP {Method} {Uri} failed after {Elapsed}ms. Status: {StatusCode}",
                request.Method,
                sanitizedUri,
                elapsed.TotalMilliseconds,
                response?.StatusCode.ToString() ?? "No response");
        }
    }
}