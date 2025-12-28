using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Ollama.Daos;

public class RawOllamaResponseDao
{
    [JsonProperty("response")]
    public string Response { get; set; } = string.Empty;
}