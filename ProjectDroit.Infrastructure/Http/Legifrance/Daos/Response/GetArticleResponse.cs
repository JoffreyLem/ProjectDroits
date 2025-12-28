using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

public class GetArticleResponse
{
    [JsonProperty(PropertyName = "article")]
    public Article? Article { get; set; }
}

public class Article
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "texte")]
    public string Texte { get; set; }
}