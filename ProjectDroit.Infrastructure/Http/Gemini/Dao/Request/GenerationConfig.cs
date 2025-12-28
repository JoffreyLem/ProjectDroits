using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Gemini.Dao.Request;

public class GenerationConfig
{
    [JsonProperty("response_mime_type")]
    public string ResponseMimeType { get; set; }
}