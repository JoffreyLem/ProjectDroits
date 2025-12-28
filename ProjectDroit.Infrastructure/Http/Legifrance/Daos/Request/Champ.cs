using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request;

public class Champ
{
    public List<Critere> Criteres { get; set; }
    public OperatorEnum Operateur { get; set; } = OperatorEnum.ET;
    public TypeChampEnum TypeChamp { get; set; } = TypeChampEnum.ALL;
}