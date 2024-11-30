using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using TaskPulse.Application.Helper;
using TaskPulse.Domain.Options;

namespace TaskPulse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.NotificationPublisher = new TaskWhenAllPublisher();
        });
        
        services.AddTransient<CaptchaVerificationService>();
        
        services.AddOptions<CaptchaOptions>().BindConfiguration(CaptchaOptions.SectionName);

        return services;
    }
}