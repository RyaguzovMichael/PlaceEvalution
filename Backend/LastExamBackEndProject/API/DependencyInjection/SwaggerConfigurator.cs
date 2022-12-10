using Microsoft.OpenApi.Models;

namespace LastExamBackEndProject.API.DependencyInjection;

public static class SwaggerConfigurator
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "LastExam.API", Version = "v1" });
        });
        return services;
    }
}