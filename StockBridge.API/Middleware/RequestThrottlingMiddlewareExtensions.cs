using Microsoft.AspNetCore.Builder;

namespace StockBridge.API.Middleware
{
    public static class RequestThrottlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestThrottling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestThrottlingMiddleware>();
        }
    }
}