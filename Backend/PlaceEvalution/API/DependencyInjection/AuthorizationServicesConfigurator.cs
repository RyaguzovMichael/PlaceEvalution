using PlaceEvolution.API.Domain.Contracts;
using PlaceEvolution.API.Domain.Services;
using PlaceEvolution.API.Infrastructure.Services;

namespace PlaceEvolution.API.API.DependencyInjection;

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