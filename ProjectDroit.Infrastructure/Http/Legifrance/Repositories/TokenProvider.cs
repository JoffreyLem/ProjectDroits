using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjectDroit.Infrastructure.Http.Legifrance.Configuration;
using ProjectDroit.Infrastructure.Http.Legifrance.Daos;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Repositories;

public interface ITokenProvider
{
    Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
}

public class OAuthTokenProvider : ITokenProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly LegifranceSettings _settings;
    private string? _token;
    private DateTime _expiresAt = DateTime.MinValue;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public OAuthTokenProvider(IHttpClientFactory factory, IOptions<LegifranceSettings> settings)
    {
        _httpClientFactory = factory;
        _settings = settings.Value;
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _expiresAt)
            return _token;

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (!string.IsNullOrEmpty(_token) && DateTime.UtcNow < _expiresAt)
                return _token;

            var client = _httpClientFactory.CreateClient();

            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _settings.ClientId),
                new KeyValuePair<string, string>("client_secret", _settings.ClientSecret),
                new KeyValuePair<string, string>("scope", _settings.Scope)
            });

            var response = await client.PostAsync(_settings.TokenUrl, requestContent, cancellationToken);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var parsed = JsonConvert.DeserializeObject<OAuthResponse>(content);

            _token = parsed?.AccessToken;
            _expiresAt = DateTime.UtcNow.AddSeconds(parsed?.ExpiresIn ?? 3600);

            return _token!;
        }
        finally
        {
            _lock.Release();
        }
    }
}
