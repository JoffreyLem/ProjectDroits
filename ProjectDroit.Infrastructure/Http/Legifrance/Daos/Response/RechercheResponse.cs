using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response
{
    public class RechercheResponse
    {
        [JsonProperty("executionTime")]
        public int ExecutionTime { get; set; }

        [JsonProperty("results")]
        public List<Results> Results { get; set; }

        [JsonProperty("facets")]
        public List<Facets> Facets { get; set; }

        [JsonProperty("totalResultNumber")]
        public int TotalResultNumber { get; set; }

        [JsonProperty("totalArticleResultNumber")]
        public int TotalArticleResultNumber { get; set; }

        [JsonProperty("typePagination")]
        public string TypePagination { get; set; }

        [JsonProperty("descriptionFusionHtml")]
        public string DescriptionFusionHtml { get; set; }
    }
}
