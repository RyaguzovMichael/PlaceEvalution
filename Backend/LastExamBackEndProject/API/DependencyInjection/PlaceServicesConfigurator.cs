using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Domain.Services;
using LastExamBackEndProject.Infrastructure.Repositories;
using LastExamBackEndProject.Infrastructure.Services;

namespace LastExamBackEndProject.API.DependencyInjection;

public static class PlaceServicesConfigurator
{
    public static IServiceCollection AddPlaceServices(this IServiceCollection services)
    {
        services.AddTransient<IPalceDbService, PlaceDbService>();
        services.AddTransient<PlaceFactory>();
        services.AddTransient<IReviewDbService, ReviewDbService>();
        services.AddTransient<ReviewFactory>();

        return services;
    }
}
