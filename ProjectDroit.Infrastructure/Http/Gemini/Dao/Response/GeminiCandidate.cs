using Newtonsoft.Json;
using ProjectDroit.Infrastructure.Http.Gemini.Dao.Request;

namespace ProjectDroit.Infrastructure.Http.Gemini.Dao.Response;

internal class GeminiCandidate
{
    [JsonProperty("content")]
    public GeminiContent? Content { get; set; }
}