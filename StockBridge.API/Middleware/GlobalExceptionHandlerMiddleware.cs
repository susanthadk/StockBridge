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

        if (IsStringTruncation(ex))
            return (HttpStatusCode.BadRequest, GetTruncationMessage(ex));

        return (HttpStatusCode.InternalServerError, "An unexpected error occurred.");
    }

    private static bool IsStringTruncation(Exception ex)
    {
        var message = GetFullExceptionMessage(ex);
        return message.Contains("String or binary data would be truncated", StringComparison.OrdinalIgnoreCase)
            || message.Contains("will be truncated", StringComparison.OrdinalIgnoreCase)
            || message.Contains("too long", StringComparison.OrdinalIgnoreCase);
    }

    private static string GetTruncationMessage(Exception ex)
    {
        var message = GetFullExceptionMessage(ex);
        var columnMatch = Regex.Match(message, @"column\s+['""]?(.+?)['""]?\s+in table", RegexOptions.IgnoreCase);
        var valueMatch = Regex.Match(message, @"value of length\s+(\d+)\s+exceeds the maximum length of\s+(\d+)");

        if (valueMatch.Success)
        {
            var column = columnMatch.Success ? columnMatch.Groups[1].Value.Trim() : null;
            var columnInfo = string.IsNullOrEmpty(column) ? "" : $" for the field '{column}'";
            return $"The value is too long{columnInfo}. Maximum allowed length is {valueMatch.Groups[2].Value} characters, but the provided value is {valueMatch.Groups[1].Value} characters.";
        }

        return "One or more values are too long and exceed the maximum allowed length.";
    }

    private static string GetFullExceptionMessage(Exception ex)
    {
        var message = ex.InnerException?.Message ?? ex.Message;
        if (ex is DbUpdateException && ex.InnerException?.InnerException != null)
            message = ex.InnerException.InnerException.Message;

        return message;
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

        if (IsStringTruncation(ex))
            return GetTruncationMessage(ex);

        return "An unexpected error occurred while saving the record.";
    }
}