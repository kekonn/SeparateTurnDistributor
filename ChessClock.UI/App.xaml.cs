using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.Configuration.UserSecrets;
using ChessClock.UI.Extensions;


namespace ChessClock.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ConfigFilePath = "configuration.ini";

        private readonly IServiceProvider serviceProvider;
        public IServiceProvider ServiceProvider => serviceProvider;

        private IConfigurationRoot configuration;

        public App() : base()
        {
            var configBuilder = new ConfigurationBuilder();

            Configure(configBuilder);

            configuration = configBuilder.Build();

            var serviceProviderFactory = new DefaultServiceProviderFactory();
            var serviceCollection = serviceProviderFactory.CreateBuilder(new ServiceCollection());

            AddServices(serviceCollection, configuration);

            serviceProvider = serviceProviderFactory.CreateServiceProvider(serviceCollection);
        }

        private void Configure(IConfigurationBuilder builder)
        {
            builder.AddIniFile(ConfigFilePath, true);
            builder.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        }

        private void AddServices(IServiceCollection services, IConfiguration config)
        {
            services.AddLogging(config);
            services.AddAzureSyncEngine(options =>
            {
                options.ConnectionString = config.GetConnectionString("AzureStorage");
                options.ContainerName = "game";
                options.TableName = "game";
            });
        }
    }
}
