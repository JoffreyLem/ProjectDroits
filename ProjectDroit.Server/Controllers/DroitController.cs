using Microsoft.AspNetCore.Mvc;
using ProjectDroit.Infrastructure.Http.Legifrance.Repositories;
using ProjectDroit.Infrastructure.Http.Ollama.Repositories;
using ProjectDroit.Server.Dto;
using Newtonsoft.Json;
using ProjectDroit.Core.Dto;
using ProjectDroit.Core.Interfaces.Services;
using ProjectDroit.Core.UseCases;
using ProjectDroit.Domain.Entities.Legifrance;

namespace ProjectDroit.Server.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class DroitController(IGlobalSearchUseCase globalSearchUseCase, IAdvancedSearchUseCase advancedSearchUseCase) : ControllerBase
{
    [HttpPost("SearchLaw")]
    public async Task<IActionResult> SearchLaw([FromBody] PromptContentDto promptContent)
    {
        if (promptContent.IsAdvancedSearch)
        {
            if (!promptContent.SelectedFonds.Any())
            {
                throw new Exception("Au moins 1 fond doit être selectionné en recherche avancée.");
            }
            return Ok(await advancedSearchUseCase.Handle(promptContent));
        }
        else
        {
            var result= await globalSearchUseCase.Handle(promptContent);
            return Ok(result);
        }
        

    }

}