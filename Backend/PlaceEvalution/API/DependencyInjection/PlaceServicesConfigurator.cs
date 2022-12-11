using PlaceEvolution.API.API.Contracts;
using PlaceEvolution.API.Domain.Contracts;
using PlaceEvolution.API.Domain.Services;
using PlaceEvolution.API.Infrastructure.Services;

namespace PlaceEvolution.API.API.DependencyInjection;

public static class PlaceServicesConfigurator
{
    public static IServiceCollection AddPlaceServices(this IServiceCollection services)
    {
        services.AddTransient<PlaceService>();
        services.AddTransient<IPalceDbService, PlaceDbService>();
        services.AddTransient<PlaceFactory>();
        services.AddTransient<IReviewDbService, ReviewDbService>();
        services.AddTransient<ReviewFactory>();
        services.AddTransient<IFileDbService, FileDbService>();

        return services;
    }
}
