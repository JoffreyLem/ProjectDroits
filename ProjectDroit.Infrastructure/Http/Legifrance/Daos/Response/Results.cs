using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

public class Results
{
    [JsonProperty("titles")]
    public List<Titles> Titles { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("nature")]
    public string Nature { get; set; }

    [JsonProperty("origin")]
    public string Origin { get; set; }

    [JsonProperty("etat")]
    public string Etat { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("sections")]
    public List<Sections> Sections { get; set; }

    [JsonProperty("num")]
    public string Num { get; set; }

    [JsonProperty("jorfText")]
    public string JorfText { get; set; }

    [JsonProperty("numParution")]
    public string NumParution { get; set; }

    [JsonProperty("datePublication")]
    public string DatePublication { get; set; }

    [JsonProperty("dossiersLegislatifs")]
    public List<DossierLegislatif> DossiersLegislatifs { get; set; }

    [JsonProperty("nor")]
    public string Nor { get; set; }

    [JsonProperty("motsCles")]
    public List<MotCle> MotsCles { get; set; }

    [JsonProperty("appellations")]
    public List<Appellation> Appellations { get; set; }

    [JsonProperty("idAttachment")]
    public string IdAttachment { get; set; }

    [JsonProperty("sizeAttachment")]
    public string SizeAttachment { get; set; }

    [JsonProperty("moreArticle")]
    public bool MoreArticle { get; set; }

    [JsonProperty("additionalResult")]
    public object AdditionalResult { get; set; }

    [JsonProperty("raisonSociale")]
    public string RaisonSociale { get; set; }

    [JsonProperty("idcc")]
    public string Idcc { get; set; }

    [JsonProperty("descriptionFusionHtml")]
    public string DescriptionFusionHtml { get; set; }

    [JsonProperty("dateSignature")]
    public string DateSignature { get; set; }

    [JsonProperty("dateDiffusion")]
    public string DateDiffusion { get; set; }

    [JsonProperty("reference")]
    public string Reference { get; set; }

    [JsonProperty("themes")]
    public List<Theme> Themes { get; set; }

    [JsonProperty("conforme")]
    public bool Conforme { get; set; }

    [JsonProperty("resumePrincipal")]
    public List<ResumePrincipal> ResumePrincipal { get; set; }

    [JsonProperty("autreResume")]
    public List<AutreResume> AutreResume { get; set; }
}