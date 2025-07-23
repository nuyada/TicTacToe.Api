using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace TicTacToe.Api.Service;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, ProblemDetailsFactory problemDetailsFactory)
    {
        _next = next;
        _logger = logger;
        _problemDetailsFactory = problemDetailsFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var title = "An unexpected error occurred.";
        var detail = exception.Message;

        if (exception is ArgumentException)
            statusCode = (int)HttpStatusCode.BadRequest;
        else if (exception is InvalidOperationException)
            statusCode = (int)HttpStatusCode.BadRequest;
        else if (exception is KeyNotFoundException)
            statusCode = (int)HttpStatusCode.NotFound;
        else if (exception is Microsoft.AspNetCore.Http.BadHttpRequestException)
        {
            statusCode = (int)HttpStatusCode.BadRequest;
            title = "Invalid JSON or request body";
            detail = exception.Message;
        }

        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
            context,
            statusCode: statusCode,
            title: title,
            detail: detail
        );
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = statusCode;
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, options));
    }
} 