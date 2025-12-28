namespace ProjectDroit.Infrastructure.Http.Legifrance.Configuration;

public class LegifranceSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string TokenUrl { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
}