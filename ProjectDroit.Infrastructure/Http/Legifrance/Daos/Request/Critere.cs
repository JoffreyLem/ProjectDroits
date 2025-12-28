using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request;

public class Critere
{
    public string Valeur { get; set; }
    public OperatorEnum Operateur { get; set; }
    public TypeRechercheEnum TypeRecherche { get; set; }
}