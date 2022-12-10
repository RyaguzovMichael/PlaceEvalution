using LastExamBackEndProject.Infrastructure.DbContexts;
using LastExamBackEndProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.API.DependencyInjection;

public static class DataBaseConfigurator
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExamDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnectionString")));

        services.AddTransient<UserRepository>();
        return services;
    }
}