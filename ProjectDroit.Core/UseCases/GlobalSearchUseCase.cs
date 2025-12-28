using ProjectDroit.Core.Dto;
using ProjectDroit.Core.Exceptions;
using ProjectDroit.Core.Interfaces;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Core.Interfaces.Services;
using ProjectDroit.Domain.Entities.Legifrance;

namespace ProjectDroit.Core.UseCases;

public class GlobalSearchUseCase(ILLMRepository llmRepository, ILegifranceRepository legifranceRepository) : IGlobalSearchUseCase
{
    public async Task<string?> Handle(PromptContentDto promptContentDto)
    {
        var keysWord = await llmRepository.ExtractKeywordsAsync(promptContentDto.Content);

        if (keysWord is { Count: 0 })
        {
            throw new NoDataException("La question n'est pas traitable, veuillez la reformuler.");
        }
        
        List<SearchResult> legifranceSearchResult =  await legifranceRepository.GlobalSearchLawsAsync(keysWord);
        
        if (legifranceSearchResult is { Count: 0 })
        {
            throw new NoDataException("Aucunes loi ne semble correspondre à la question, veuillez la reformuler.");
        }
        
        foreach (var searchResult in legifranceSearchResult)
        {
            foreach (var searchResultSection in searchResult.Sections)
            {
                searchResultSection.Text = await legifranceRepository.GetLawAsync(searchResultSection.Id);
            }
        }
        
        var LLMResult = await llmRepository.GetResponse(promptContentDto.Content,legifranceSearchResult);
        
        return LLMResult;
        
    }
}