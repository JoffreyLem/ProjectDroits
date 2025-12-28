using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Gemini.Dao.Request;

internal class GeminiPart
{
    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;
}