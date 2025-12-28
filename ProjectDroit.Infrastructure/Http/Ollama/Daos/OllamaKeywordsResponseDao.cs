using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Ollama.Daos;

public class OllamaKeywordsResponseDao
{
    [JsonProperty("response")]
    public List<string> Response { get; set; } = new List<string>();
}