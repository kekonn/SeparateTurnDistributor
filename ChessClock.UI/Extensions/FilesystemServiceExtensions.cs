using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;
using ChessClock.SyncEngine;
using ChessClock.UI.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChessClock.UI.Extensions
{
    public static class FilesystemServiceExtensions
    {
        public static IServiceCollection AddCiv6Filesystem(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(provider => new Civ6Filesystem(Settings.Default.HotseatSaveFolder));

            return services;
        }
    }
}
