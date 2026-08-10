using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace StockBridge.API.Middleware
{
    public class UserLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserLoggingMiddleware> _logger;

        public UserLoggingMiddleware(RequestDelegate next, ILogger<UserLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Extract user information from the HttpContext
            var userName = context.User?.Identity?.Name ?? "Anonymous";
            var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

            // 2. Create a dictionary to hold the scope data
            var logContext = new Dictionary<string, object>
            {
                { "UserName", userName },
                { "UserId", userId }
            };

            // 3. Wrap the rest of the request pipeline in the logger scope
            using (_logger.BeginScope(logContext))
            {
                // All logs written by any component after this point (Controllers, Services, etc.)
                // will automatically include the "UserName" and "UserId" properties.
                await _next(context);
            }
        }
    }
}