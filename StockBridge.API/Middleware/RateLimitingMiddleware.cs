using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace StockBridge.API.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, List<DateTime>> _requests = new();
        private readonly int _limit = 5; // max requests
        private readonly TimeSpan _window = TimeSpan.FromSeconds(10); // per 10 seconds
        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                    requests.Add(now);
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