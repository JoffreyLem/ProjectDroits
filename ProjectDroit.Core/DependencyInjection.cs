using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectDroit.Core.Interfaces.Services;
using ProjectDroit.Core.UseCases;

namespace ProjectDroit.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IGlobalSearchUseCase, GlobalSearchUseCase>();
        services.AddScoped<IAdvancedSearchUseCase, AdvancedSearchUseCase>();
        return services;
    }
}