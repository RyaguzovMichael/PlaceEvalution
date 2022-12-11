using PlaceEvalution.API.API.Contracts;
using PlaceEvalution.API.Domain.Contracts;
using PlaceEvalution.API.Domain.Services;
using PlaceEvalution.API.Infrastructure.Services;

namespace PlaceEvalution.API.API.DependencyInjection;

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
