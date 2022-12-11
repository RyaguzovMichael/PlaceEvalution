using Microsoft.EntityFrameworkCore;
using PlaceEvolution.API.Infrastructure.Abstractions;
using PlaceEvolution.API.Infrastructure.Models;
using PlaceEvolution.API.Infrastructure.Services;

namespace PlaceEvolution.API.API.DependencyInjection;

public static class DataBaseConfigurator
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataBaseContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnectionString")));

        services.AddTransient<IUserRepository, UserDbService>();
        services.AddTransient<IPlaceRepository, PlaceDbService>();
        services.AddTransient<IReviewRepository, ReviewDbService>();

        return services;
    }
}