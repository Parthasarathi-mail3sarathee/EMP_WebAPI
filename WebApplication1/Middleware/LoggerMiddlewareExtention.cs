using Microsoft.AspNetCore.Builder;

namespace WebApplication_WebAPI.Middleware
{
    public static class LoggerMiddlewareExtention
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
