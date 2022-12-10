using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Domain.Services;
using LastExamBackEndProject.Infrastructure.Services;

namespace LastExamBackEndProject.API.DependencyInjection;

public static class AuthorizationServicesConfigurator
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services.AddTransient<IUserDbService, UserDbService>();
        services.AddTransient<UserFactory>();
        services.AddTransient<UserService>();
        
        return services;
    }
}