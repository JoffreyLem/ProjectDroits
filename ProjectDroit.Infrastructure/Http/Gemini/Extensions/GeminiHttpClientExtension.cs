using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Infrastructure.Http.Gemini.Configuration;
using ProjectDroit.Infrastructure.Http.Gemini.Handlers;
using ProjectDroit.Infrastructure.Http.Gemini.Repositories;
using ProjectDroit.Infrastructure.Http.Ollama.Configuration;

namespace ProjectDroit.Infrastructure.Http.Gemini.Extensions;

internal static class GeminiHttpClientExtension
{
    public static void AddGeminiHttpClient(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<GeminiSettings>(configuration.GetSection("Gemini"));
        services.AddTransient<HttpGeminiLogger>();
        services.AddHttpClient("Gemini", (serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<GeminiSettings>>().Value;
                client.BaseAddress = new Uri(settings.BaseUrl);
            })
            .AddLogger<HttpGeminiLogger>();

        services.AddScoped<ILLMRepository, GeminiRepository>();
    }
}