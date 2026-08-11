using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StockBridge.API.Middleware
{
    public class RequestThrottlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SemaphoreSlim _semaphore; // allow N concurrent requests
        private readonly TimeSpan _delay; // max wait time
        private readonly ILogger<RequestThrottlingMiddleware> _logger;

        public RequestThrottlingMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<RequestThrottlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

            var throttling = configuration.GetSection("RequestThrottling");
            var maxConcurrent = throttling.GetValue("MaxConcurrentRequests", 3);
            var waitTimeoutSeconds = throttling.GetValue("WaitTimeoutSeconds", 5);

            _semaphore = new SemaphoreSlim(maxConcurrent, maxConcurrent);
            _delay = TimeSpan.FromSeconds(waitTimeoutSeconds);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!await _semaphore.WaitAsync(_delay))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Server is busy, please try again later.");
                return;
            }

            try
            {
                await _next(context);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}