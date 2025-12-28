using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Gemini.Dao.Request;

internal class GeminiContent
{
    [JsonProperty("parts")]
    public List<GeminiPart> Parts { get; set; } = new List<GeminiPart>();
}