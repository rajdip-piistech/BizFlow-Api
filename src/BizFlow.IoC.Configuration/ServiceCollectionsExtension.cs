using BizFlow.Application;
using BizFlow.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BizFlow.IoC.Configuration;
public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddIOCConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);
        services.AddApplicationServices(configuration);
        return services;
    }
}
