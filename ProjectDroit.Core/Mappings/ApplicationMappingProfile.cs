using AutoMapper;
// Assurez-vous que les namespaces sont corrects et accessibles
using ProjectDroit.Core.Dto.Enum;
using ProjectDroit.Domain.Entities.Legifrance.Enum; // Namespace corrigé pour le domaine

namespace ProjectDroit.Core.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<Dto.Enum.FondApiName, Domain.Entities.Legifrance.Enum.FondApiName>()
            .ConvertUsing(source => MapFondApiNameDtoToDomain(source));
        CreateMap<Domain.Entities.Legifrance.Enum.FondApiName, Dto.Enum.FondApiName>()
            .ConvertUsing(source => MapFondApiNameDomainToDto(source)); 
    }

    private Domain.Entities.Legifrance.Enum.FondApiName MapFondApiNameDtoToDomain(Dto.Enum.FondApiName source)
    {
        return (Domain.Entities.Legifrance.Enum.FondApiName)source;
    }

    private Dto.Enum.FondApiName MapFondApiNameDomainToDto(Domain.Entities.Legifrance.Enum.FondApiName source)
    {
        return (Dto.Enum.FondApiName)source;
    }
}