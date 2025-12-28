namespace ProjectDroit.Domain.Entities.LLM;

public class LlmDomainOptions
{
    public float Temperature { get; set; } = 0.5f;

    public float TopP { get; set; } = 0.9f;

    public int? Seed { get; set; } = null; 
}