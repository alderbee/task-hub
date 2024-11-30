using TaskPulse.Application;
using TaskPulse.Domain;
using TaskPulse.Infrastructure;

namespace TaskPulse.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDI()
            .AddInfrastructureDI()
            .AddCoreDI(configuration);
        
        return services;
    }
}