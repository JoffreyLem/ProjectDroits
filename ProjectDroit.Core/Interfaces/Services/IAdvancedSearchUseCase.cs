using ProjectDroit.Core.Dto;

namespace ProjectDroit.Core.Interfaces.Services;

public interface IAdvancedSearchUseCase
{
    Task<string?> Handle(PromptContentDto promptContentDto);
}