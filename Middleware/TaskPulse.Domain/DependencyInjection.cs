using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskPulse.Domain.Entities.DTO;
using TaskPulse.Domain.Helpers.Validators;
using TaskPulse.Domain.Options;

namespace TaskPulse.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));
        services.AddScoped<IValidator<AddTask>, AddTaskValidator>();
        services.AddScoped<IValidator<UpdateTask>, UpdateTaskValidator>();
        services.AddScoped<IValidator<UserRegistration>, UserRegistrationValidator>();
        
        return services;
    }
    
}