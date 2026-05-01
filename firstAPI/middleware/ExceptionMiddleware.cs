using System.Net;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
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

        var response=new {message = ex.Message};
        return context.Response.WriteAsJsonAsync(response);
    }
}