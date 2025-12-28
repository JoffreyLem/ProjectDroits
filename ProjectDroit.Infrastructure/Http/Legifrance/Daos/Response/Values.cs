using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

public class Values
{
    [JsonProperty("Code de la recherche")]
    public int CodeDeLaRecherche { get; set; }

    [JsonProperty("Code rural et de la pêche maritime")]
    public int CodeRuralEtPeche { get; set; }

    [JsonProperty("Code de justice administrative")]
    public int CodeJusticeAdministrative { get; set; }

    [JsonProperty("Code de la défense")]
    public int CodeDefense { get; set; }

    [JsonProperty("Code de la santé publique")]
    public int CodeSantePublique { get; set; }

    [JsonProperty("Code rural (nouveau)")]
    public int CodeRuralNouveau { get; set; }

    [JsonProperty("Code de commerce")]
    public int CodeCommerce { get; set; }

    [JsonProperty("Code de l'organisation judiciaire")]
    public int CodeOrganisationJudiciaire { get; set; }

    [JsonProperty("Code du travail")]
    public int CodeTravail { get; set; }

    [JsonProperty("ABROGE")]
    public int Abroge { get; set; }

    [JsonProperty("ABROGE_DIFF")]
    public int AbrogeDiff { get; set; }

    [JsonProperty("ANNULE")]
    public int Annule { get; set; }

    [JsonProperty("DENONCE")]
    public int Denonce { get; set; }

    [JsonProperty("DISJOINT")]
    public int Disjoint { get; set; }

    [JsonProperty("INITIALE")]
    public int Initiale { get; set; }

    [JsonProperty("MODIFIE")]
    public int Modifie { get; set; }

    [JsonProperty("MODIFIE_MORT_NE")]
    public int ModifieMortNe { get; set; }

    [JsonProperty("PERIME")]
    public int Perime { get; set; }

    [JsonProperty("REMPLACE")]
    public int Remplace { get; set; }

    [JsonProperty("SANS_ETAT")]
    public int SansEtat { get; set; }

    [JsonProperty("SUBSTITUE")]
    public int Substitue { get; set; }

    [JsonProperty("TRANSFERE")]
    public int Transfere { get; set; }

    [JsonProperty("VIGUEUR")]
    public int Vigueur { get; set; }

    [JsonProperty("VIGUEUR_DIFF")]
    public int VigueurDiff { get; set; }

    [JsonProperty("VIGUEUR_ETEN")]
    public int VigueurEten { get; set; }

    [JsonProperty("VIGUEUR_NON_ETEN")]
    public int VigueurNonEten { get; set; }
}