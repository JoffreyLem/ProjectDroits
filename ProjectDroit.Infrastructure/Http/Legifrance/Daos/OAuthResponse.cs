using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Daos;

public class OAuthResponse
{
    
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}