using PlaceEvalution.API.Domain.Contracts;
using PlaceEvalution.API.Domain.Services;
using PlaceEvalution.API.Infrastructure.Services;

namespace PlaceEvalution.API.API.DependencyInjection;

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