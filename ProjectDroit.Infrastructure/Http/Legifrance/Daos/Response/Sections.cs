using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

public class Sections
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("dateVersion")]
    public string DateVersion { get; set; }

    [JsonProperty("legalStatus")]
    public string LegalStatus { get; set; }

    [JsonProperty("extracts")]
    public List<Extracts> Extracts { get; set; }
}