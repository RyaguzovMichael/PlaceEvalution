namespace LastExamBackEndProject.API.DependencyInjection;

public static class SessionsConfigurator
{
    public static IServiceCollection AddSessions(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.Name = ".LastExam.Session";
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });
        services.AddDistributedMemoryCache();

        return services;
    }
}