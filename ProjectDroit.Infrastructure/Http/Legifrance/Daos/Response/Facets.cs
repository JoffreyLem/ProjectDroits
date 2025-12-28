using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

public class Facets
{
    [JsonProperty("facetElem")]
    public string FacetElem { get; set; }

    [JsonProperty("field")]
    public string Field { get; set; }

    [JsonProperty("values")]
    public Values Values { get; set; }

    [JsonProperty("childs")]
    public object Childs { get; set; }

    [JsonProperty("totalElement")]
    public int TotalElement { get; set; }
}