using AutoMapper;
using ProjectDroit.Domain.Entities.Legifrance;
using ProjectDroit.Infrastructure.Http.Legifrance.Daos.Response;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Mapping;

public class LegifranceMappingProfile : Profile
{
    public LegifranceMappingProfile()
    {
  
        CreateMap<Results, SearchResult>() 
            .ForMember(dest => dest.TextId, opt => opt.MapFrom(src => src.Titles.FirstOrDefault().Id)) 
            .ForMember(dest => dest.TextTitle, opt => opt.MapFrom(src => src.Titles.FirstOrDefault().Title))
            .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections.FirstOrDefault().Extracts));

        CreateMap<Extracts, ProjectDroit.Domain.Entities.Legifrance.Sections>();
    }
}