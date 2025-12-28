
using ProjectDroit.Domain.Entities.Legifrance;
using ProjectDroit.Domain.Entities.Legifrance.Enum;

namespace ProjectDroit.Core.Interfaces.Infrastructure;

public interface ILegifranceRepository
{
    Task<List<SearchResult>> GlobalSearchLawsAsync(List<string> keywordsToSearch);
    Task<string> GetLawAsync(string id);

    Task<List<SearchResult>> SpecificSearchLawAsync(List<string> keywordsToSearch, IEnumerable<FondApiName> fonds);
}