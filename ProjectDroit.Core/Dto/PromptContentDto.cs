using ProjectDroit.Core.Dto.Enum;

namespace ProjectDroit.Core.Dto;

public class PromptContentDto
{
    public string Content { get; set; }
    public bool IsAdvancedSearch { get; set; }
    public  IEnumerable<FondApiName>? SelectedFonds { get; set; }

}