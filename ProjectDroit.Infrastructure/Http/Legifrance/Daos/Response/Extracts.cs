using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

public class Extracts
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("legalStatus")]
    public string LegalStatus { get; set; }

    [JsonProperty("dateVersion")]
    public string DateVersion { get; set; }

    [JsonProperty("dateDebut")]
    public string DateDebut { get; set; }

    [JsonProperty("dateFin")]
    public string DateFin { get; set; }

    [JsonProperty("searchFieldName")]
    public string SearchFieldName { get; set; }

    [JsonProperty("num")]
    public string Num { get; set; }

    [JsonProperty("values")]
    public List<string> Values { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}