using System.Text.Json.Serialization;

namespace ProjectDroit.Infrastructure.Http.Ollama.Daos;

public class OllamaInfraRequestDao
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = "mistral";

    [JsonPropertyName("prompt")]
    public string Prompt { get; set; } = string.Empty;
    
    [JsonPropertyName("template")]
    public string? Template { get; set; }

    [JsonPropertyName("options")]
    public OllamaInfraOptionsDao Options { get; set; } = new OllamaInfraOptionsDao();
    
    [JsonPropertyName("stream")]
    public bool Stream { get; set; } = false;
    
}