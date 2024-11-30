using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskPulse.Application.Helper;
using TaskPulse.Domain.interfaces;
using TaskPulse.Domain.interfaces.Helper;
using TaskPulse.Domain.Options;
using TaskPulse.Infrastructure.Data;
using TaskPulse.Infrastructure.Helper;
using TaskPulse.Infrastructure.Repositories;
using TaskPulse.Infrastructure.Services;

namespace TaskPulse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DatabaseConnection);
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenCreator, TokenCreator>();
        services.AddScoped<IEncryptPassword, EncryptPassword>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ICaptchaVerificationService, CaptchaVerificationService>();
        
        services.AddOptions<EncryptPasswordOptions>().BindConfiguration(EncryptPasswordOptions.SectionName);
        
        services.AddAutoMapper(typeof(Automapper));
        
        return services;

    }

}