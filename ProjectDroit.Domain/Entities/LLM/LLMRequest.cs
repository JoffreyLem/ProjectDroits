namespace ProjectDroit.Domain.Entities.LLM;

public class LLMRequest
{

    public string Model { get; set; } = "mistral";
    public string Prompt { get; set; } = string.Empty;

    public string? Template { get; set; }

    public LlmDomainOptions Options { get; set; } = new LlmDomainOptions();

    public bool Stream { get; set; } = false;
    
}