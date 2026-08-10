using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace StockBridge.API.Middleware
{
    public class RequestThrottlingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly SemaphoreSlim _semaphore = new(3); // allow 3 concurrent requests
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(5); // max wait time
        private readonly ILogger<RequestThrottlingMiddleware> _logger;

        public RequestThrottlingMiddleware(RequestDelegate next, ILogger<RequestThrottlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}