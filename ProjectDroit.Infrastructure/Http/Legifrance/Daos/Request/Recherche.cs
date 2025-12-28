namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request;

public class Recherche
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string Sort { get; set; } = "PERTINENCE";
    public string TypePagination { get; set; } = "DEFAUT";
    public List<Champ> Champs { get; set; }
    
    public List<Filtres> Filtres { get; set; } = new List<Filtres>() ;
}