using FinancialControllerServer.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace FinancialControllerServer.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception ex)
    {
        int statusCode;
        string message;
        List<string> errors;
        string traceId;
        switch (ex)
        {
            case AppException appEx:
                statusCode = appEx.StatusCode;
                message = appEx.Message;
                traceId = context.TraceIdentifier;
                errors = appEx.Errors;
                _logger.LogWarning(ex,
                    "Handled exception on {Method} {Path} | TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.TraceIdentifier);
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "Erro interno do servidor.";
                traceId = context.TraceIdentifier;
                errors = null;
                _logger.LogError(ex,
                    "Unhandled exception on {Method} {Path} | TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.TraceIdentifier);
                break;
        }

        var response = new
        {
            message,
            errors,
            traceId 
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);

    }
}