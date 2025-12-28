using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request;
using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Request.Enums;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Repositories;

public static class RechercheRequestBuilder
{
    private static RechercheRequest CreateBaseRechercheRequest(FondEnum fond, List<string> keywordsToSearch)
    {
        string keywords = string.Join(" ", keywordsToSearch);

        var request = new RechercheRequest()
        {
            Fond = fond,
            FromAdvancedRecherche = true,
            Recherche = new Recherche()
            {
                Champs =
                [
                    new Champ()
                    {
                        TypeChamp = TypeChampEnum.ALL,
                        Operateur = OperatorEnum.ET,
                        Criteres =
                        [
                            new Critere()
                            {
                                TypeRecherche = TypeRechercheEnum.TOUS_LES_MOTS_DANS_UN_CHAMP,
                                Operateur = OperatorEnum.ET,
                                Valeur = keywords
                            }
                        ]
                    },

                    new Champ()
                    {
                        TypeChamp = TypeChampEnum.TITLE,
                        Operateur = OperatorEnum.ET,
                        Criteres =
                        [
                            new Critere()
                            {
                                TypeRecherche = TypeRechercheEnum.UN_DES_MOTS,
                                Operateur = OperatorEnum.ET,
                                Valeur = keywords
                            }
                        ]
                    }
                ],
                Filtres = []
            }
        };
        return request;
    }

    public static RechercheRequest BuildGlobalResearchRequest(List<string> keywordsToSearch)
    {
        var request = CreateBaseRechercheRequest(FondEnum.ALL, keywordsToSearch);

        request.Recherche.Filtres.Add(new Filtres()
        {
            Facette = "FOND",
            Valeurs = ["LEGI", "KALI", "JORF", "CODE"]
        });

        return request;
    }

    public static RechercheRequest BuildCodeOrLodaOrKaliResearchRequest(List<string> keywordsToSearch, FondEnum fond)
    {
        var request = CreateBaseRechercheRequest(fond, keywordsToSearch);

        request.Recherche.PageNumber = 1;
        request.Recherche.PageSize = 5;
        request.Recherche.TypePagination = "DEFAUT";
        request.Recherche.Sort = "PERTINENCE";

        request.Recherche.Filtres.Add(new Filtres()
        {
            Facette = "TEXT_LEGAL_STATUS",
            Valeurs = ["VIGUEUR"]
        });
        request.Recherche.Filtres.Add(new Filtres()
        {
            Facette = "ARTICLE_LEGAL_STATUS",
            Valeurs = ["VIGUEUR"]
        });

        return request;
    }

    public static RechercheRequest BuildJorfResearchRequest(List<string> keywordsToSearch)
    {
        var request = CreateBaseRechercheRequest(FondEnum.JORF, keywordsToSearch);

        request.Recherche.PageNumber = 1;
        request.Recherche.PageSize = 5;
        request.Recherche.TypePagination = "DEFAULT";
        request.Recherche.Sort = "PERTINENCE";

        return request;
    }
}