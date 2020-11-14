using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace ChessClock.UI.Extensions
{
    public static class LoggingServiceExtensions
    {
        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration config)
        {
            return services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog(config);
            });
        }
    }
}
