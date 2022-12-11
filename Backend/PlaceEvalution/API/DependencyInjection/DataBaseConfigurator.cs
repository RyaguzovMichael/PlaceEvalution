using Microsoft.EntityFrameworkCore;
using PlaceEvalution.API.Infrastructure.Abstractions;
using PlaceEvalution.API.Infrastructure.Models;
using PlaceEvalution.API.Infrastructure.Services;

namespace PlaceEvalution.API.API.DependencyInjection;

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