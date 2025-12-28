using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request;

public class RechercheRequest
{
    public FondEnum Fond { get; set; } = FondEnum.CODE_DATE;
    public bool FromAdvancedRecherche { get; set; } = false;
    public Recherche Recherche { get; set; }



}