using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

public class Titles
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("cid")]
    public string Cid { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("legalStatus")]
    public string LegalStatus { get; set; }

    [JsonProperty("startDate")]
    public string StartDate { get; set; }

    [JsonProperty("endDate")]
    public string EndDate { get; set; }

    [JsonProperty("nature")]
    public string Nature { get; set; }
}