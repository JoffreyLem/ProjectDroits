using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Gemini.Dao.Response;

internal class GeminiGenerationResponse
{
    [JsonProperty("candidates")]
    public List<GeminiCandidate>? Candidates { get; set; }
}