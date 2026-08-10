using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace StockBridge.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, List<DateTime>> _requests = new();
        private readonly int _limit; // max requests
        private readonly TimeSpan _window; // per window
        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            var rateLimiting = configuration.GetSection("RateLimiting");
            _limit = rateLimiting.GetValue("Limit", 5);
            _window = TimeSpan.FromSeconds(rateLimiting.GetValue("WindowSeconds", 10));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                var now = DateTime.UtcNow;
                var requests = _requests.GetOrAdd(clientIp, _ => new List<DateTime>());

                lock (requests)
                {
                    requests.RemoveAll(x => x < now - _window); // remove old requests
                    requests.Add(now); // record the current request BEFORE the limit check
                }

                if (requests.Count > _limit)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
            }
        }
    }
}