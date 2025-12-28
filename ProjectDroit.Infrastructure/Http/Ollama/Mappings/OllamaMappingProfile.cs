using AutoMapper;
using ProjectDroit.Domain.Entities;
using ProjectDroit.Domain.Entities.LLM;
using ProjectDroit.Infrastructure.Http.Ollama.Daos;

namespace ProjectDroit.Infrastructure.Http.Ollama.Mappings;

public class OllamaMappingProfile : Profile
{
    public OllamaMappingProfile()
    {
        CreateMap<LLMRequest, OllamaInfraRequestDao>();
    }
}