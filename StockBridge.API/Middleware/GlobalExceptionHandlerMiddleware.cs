using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace StockBridge.API.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception in {Method} {Path}", context.Request.Method, context.Request.Path);

            if (context.Response.HasStarted)
                throw;

            var (statusCode, message) = GetErrorResponse(ex);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = ResponseInfo<object>.Failure(message, statusCode);

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private static (HttpStatusCode StatusCode, string Message) GetErrorResponse(Exception ex)
    {
        if (ex is DbUpdateException)
            return (HttpStatusCode.BadRequest, GetFriendlyDbUpdateMessage(ex));

        return (HttpStatusCode.InternalServerError, "An unexpected error occurred.");
    }

    private static string GetFriendlyDbUpdateMessage(Exception ex)
    {
        var errorMessage = ex.InnerException?.Message ?? ex.Message;

        if (errorMessage.Contains("Cannot insert duplicate key row", StringComparison.OrdinalIgnoreCase))
        {
            var match = Regex.Match(errorMessage, @"duplicate key value is \((.*?),\s*(.*?)\)");

            if (match.Success)
                return "A record with the same value already exists. Please use a different value.";

            return "A record with the same information already exists.";
        }

        return "An unexpected error occurred while saving the record.";
    }
}