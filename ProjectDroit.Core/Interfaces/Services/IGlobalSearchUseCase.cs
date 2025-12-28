using ProjectDroit.Core.Dto;
using ProjectDroit.Domain.Entities.Legifrance;

namespace ProjectDroit.Core.Interfaces.Services;

public interface IGlobalSearchUseCase
{
    Task<string?> Handle(PromptContentDto promptContentDto);
}