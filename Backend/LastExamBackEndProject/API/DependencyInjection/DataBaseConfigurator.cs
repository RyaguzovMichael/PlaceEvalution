using LastExamBackEndProject.Infrastructure.Abstractions;
using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.API.DependencyInjection;

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