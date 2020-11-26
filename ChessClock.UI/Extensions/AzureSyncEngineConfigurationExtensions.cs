using System;
using System.Windows;
using ChessClock.SyncEngine;
using ChessClock.SyncEngine.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ChessClock.UI.Extensions
{
    public static class AzureSyncEngineConfigurationExtensions
    {
        public static IServiceCollection AddAzureSyncEngine(this IServiceCollection services, Action<AzureSyncEngineOptions> configureOptions)
        {
            Func<IServiceProvider, ISyncEngine> ImplementationFactory()
            {
                return s =>
                {
                    var serviceProvider = (Application.Current as App)?.ServiceProvider ?? throw new InvalidOperationException($"Could not find service provider");

                    var logger = serviceProvider.GetService<ILogger<ISyncEngine>>() ?? throw new Exception("Could not find logger for sync engine");
                    var filesystem = serviceProvider.GetService<Civ6Filesystem>() ?? throw new Exception("Could not find filesystem for sync engine");
                    var azureSyncEngineOptions = serviceProvider.GetService<AzureSyncEngineOptions>() ??
                                                 throw new Exception("Could not find Azure Engine Options");

                    var syncEngine = new AzureSyncEngine(azureSyncEngineOptions, logger:logger, filesystem:filesystem, autoSyncStrategy:null);

                    return syncEngine;
                };
            }



            services.AddTransient<AzureSyncEngineOptions>(provider =>
            {
                var options = new AzureSyncEngineOptions();
                configureOptions(options);
                return options;
            });

            services.AddSingleton<ISyncEngine>(ImplementationFactory());

            return services;
        }
    }
}
