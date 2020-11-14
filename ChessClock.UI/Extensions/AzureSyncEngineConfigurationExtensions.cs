using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ChessClock.SyncEngine;
using ChessClock.SyncEngine.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChessClock.UI.Extensions
{
    public static class AzureSyncEngineConfigurationExtensions
    {
        public static IServiceCollection AddAzureSyncEngine(this IServiceCollection services, Action<AzureSyncEngineOptions> configureOptions)
        {
            var options = new AzureSyncEngineOptions();
            configureOptions(options);

            services.AddSingleton<ISyncEngine>(s =>
            {
                var serviceProvider = (Application.Current as App)?.ServiceProvider ?? throw new InvalidOperationException($"Could not find service provider");

                var logger = serviceProvider.GetService<ILogger<ISyncEngine>>() ?? throw new Exception("Could not find logger for sync engine");
                var filesystem = serviceProvider.GetService<Civ6Filesystem>() ?? throw new Exception("Could not find filesystem for sync engine");

                var syncEngine = new AzureSyncEngine(options, logger:logger, filesystem:filesystem, autoSyncStrategy:null);

                return syncEngine;
            });

            return services;
        }
    }
}
