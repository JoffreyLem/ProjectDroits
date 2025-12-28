using System.Text.Json.Serialization;

namespace ProjectDroit.Infrastructure.Http.Ollama.Daos;

public class OllamaInfraOptionsDao
{
    [JsonPropertyName("temperature")]
    public float Temperature { get; set; } = 0.5f;

    [JsonPropertyName("top_p")]
    public float TopP { get; set; } = 0.9f;

    [JsonPropertyName("seed")]
    public int? Seed { get; set; } = null; 
}