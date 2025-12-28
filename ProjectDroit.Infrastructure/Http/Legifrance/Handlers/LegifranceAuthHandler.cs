using System.Net.Http.Headers;
using ProjectDroit.Infrastructure.Http.Legifrance.Repositories;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Handlers;

public class LegifranceAuthHandler : DelegatingHandler
{
    private readonly ITokenProvider _tokenProvider;

    public LegifranceAuthHandler(ITokenProvider tokenProvider) => _tokenProvider = tokenProvider;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenProvider.GetTokenAsync(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken);
    }
}