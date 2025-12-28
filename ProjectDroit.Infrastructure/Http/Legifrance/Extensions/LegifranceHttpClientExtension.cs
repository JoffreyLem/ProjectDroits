using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Infrastructure.Http.Legifrance.Configuration;
using ProjectDroit.Infrastructure.Http.Legifrance.Handlers;
using ProjectDroit.Infrastructure.Http.Legifrance.Repositories;

namespace ProjectDroit.Infrastructure.Http.Legifrance.Extensions;

internal static class LegifranceHttpClientExtension
{
    public static void AddLegifranceHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.Configure<LegifranceSettings>(configuration.GetSection("Legifrance"));
        services.AddTransient<LegifranceAuthHandler>();
        services.AddSingleton<ITokenProvider, OAuthTokenProvider>();

        services.AddHttpClient("Legifrance", (serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<LegifranceSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
            })
            .AddHttpMessageHandler<LegifranceAuthHandler>()
            .AddLogger<HttpLogger>();
        
        
        services.AddScoped<ILegifranceRepository, LegifranceRepository>();

    }
}