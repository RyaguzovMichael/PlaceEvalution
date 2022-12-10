using LastExamBackEndProject.API.DependencyInjection;
using LastExamBackEndProject.API.Pipeline;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddAutomapper();
services.AddUserServices();
services.AddDatabase(builder.Configuration);
services.AddPlaceServices();

// Configure API
services.AddCorsPolicy();
services.AddControllers();
services.AddSwagger();
services.AddSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandler>();

app.UseSwaggerUiInDev();

app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllers();

app.Run();