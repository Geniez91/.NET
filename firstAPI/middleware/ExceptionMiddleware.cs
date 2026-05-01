using System.Net;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleException(context,ex);
        }
    }

    public static Task HandleException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        /// Gestion des status
        context.Response.StatusCode = ex switch
        {
            ArgumentException => (int) HttpStatusCode.BadRequest,
            NotFoundException => (int) HttpStatusCode.NotFound,
            ConflictException => (int) HttpStatusCode.Conflict,
            _ => (int) HttpStatusCode.InternalServerError
        };

        var response = new {
            message = ex.Message,
            status = context.Response.StatusCode,
            timestamp = DateTime.UtcNow,
            path = context.Request.Path
        };
        return context.Response.WriteAsJsonAsync(response);
    }
}