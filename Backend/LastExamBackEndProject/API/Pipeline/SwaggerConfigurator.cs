namespace LastExamBackEndProject.API.Pipeline;

public static class SwaggerConfigurator
{
    public static WebApplication UseSwaggerUiInDev(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}