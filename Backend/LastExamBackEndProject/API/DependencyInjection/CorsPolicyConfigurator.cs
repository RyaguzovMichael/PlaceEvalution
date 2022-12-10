namespace LastExamBackEndProject.API.DependencyInjection;

public static class CorsPolicyConfigurator
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy",
            policy =>
            {
                policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            }));
        return services;
    }
}