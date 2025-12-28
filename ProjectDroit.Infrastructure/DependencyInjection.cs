using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectDroit.Core.Interfaces;
using ProjectDroit.Core.Interfaces.Infrastructure;
using ProjectDroit.Infrastructure.Http;
using ProjectDroit.Infrastructure.Http.Gemini.Extensions;
using ProjectDroit.Infrastructure.Http.Legifrance.Extensions;
using ProjectDroit.Infrastructure.Http.Legifrance.Repositories;
using ProjectDroit.Infrastructure.Http.Ollama.Extensions;
using ProjectDroit.Infrastructure.Http.Ollama.Repositories;

namespace ProjectDroit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<HttpLogger>();
        
        services.AddLegifranceHttpClient(configuration);
        //services.AddOllamaHttpClient(configuration);
        services.AddGeminiHttpClient(configuration);
        


        return services;
    }
}