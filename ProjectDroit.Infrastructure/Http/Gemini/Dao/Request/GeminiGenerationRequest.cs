using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Gemini.Dao.Request;

internal class GeminiGenerationRequest
{
    [JsonProperty("contents")]
    public List<GeminiContent> Contents { get; set; } = new List<GeminiContent>();
    
    [JsonProperty("generationConfig")]
    public GenerationConfig GenerationConfig { get; set; } = new GenerationConfig();
}