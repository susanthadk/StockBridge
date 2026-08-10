using Microsoft.AspNetCore.Builder;

namespace StockBridge.API.Middleware
{
    public static class UserLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserLoggingMiddleware>();
        }
    }
}