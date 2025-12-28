using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Infrastructure.Http.Ollama.Configuration;
using ProjectDroit.Infrastructure.Http.Ollama.Repositories;

namespace ProjectDroit.Infrastructure.Http.Ollama.Extensions;

internal static class OllamaHttpClientExtension
{
    public static void AddOllamaHttpClient(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<OllamaSettings>(configuration.GetSection("Ollama"));
        services.AddHttpClient("Ollama", (serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<OllamaSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
            })
            .AddLogger<HttpLogger>();
        
                
        services.AddScoped<ILLMRepository, OllamaRepository>();

    }
}