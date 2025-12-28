using ProjectDroit.Domain.Entities.Legifrance;

namespace ProjectDroit.Core.Interfaces.Infrastructure;

public interface ILLMRepository
{
    Task<List<string>> ExtractKeywordsAsync(string prompt);
    Task<string?> GetResponse(string prompt, List<SearchResult> searchResults);
}