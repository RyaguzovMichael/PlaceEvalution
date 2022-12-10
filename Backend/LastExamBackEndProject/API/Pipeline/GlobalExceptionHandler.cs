using System.Net;
using System.Text;
using LastExamBackEndProject.API.Models;
using LastExamBackEndProject.Common.Exceptions;

namespace LastExamBackEndProject.API.Pipeline;

internal class GlobalExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (BaseBusinessException ex)
        {
            _logger.LogWarning(GetStackTrace(ex));
            DefaultResponse<object> response = new(ex.ExceptionCode, GetBusinessExceptionMessage(ex));
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(GetStackTrace(ex));
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync("Something went wrong");
        }
    }

    private static string GetStackTrace(Exception? innerException)
    {
        StringBuilder builder = new();

        while (innerException is not null)
        {
            builder.Append(innerException.Message);
            builder.Append('\n');

            innerException = innerException.InnerException;
        }

        return builder.ToString();
    }

    private static string GetBusinessExceptionMessage(BaseBusinessException ex)
    {
        return ex.ExceptionCode switch
        {
            ExceptionCode.ValidationDataException => ex.Message,
            ExceptionCode.UserAccessException => "User don't have permissions to this action",
            ExceptionCode.UserAuthorizeException => "User is not authorized",
            ExceptionCode.DbException => "Database read/write exception. Try later or with another data",
            _ => "Unknown business error"
        };
    }
}