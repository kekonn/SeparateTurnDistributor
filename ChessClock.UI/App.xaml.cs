using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using ChessClock.SyncEngine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ChessClock.UI.Extensions;
using ChessClock.UI.Properties;
using ChessClock.UI.ViewModels;
using ChessClock.UI.Views;


namespace ChessClock.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ConfigFilePath = "configuration.ini";

        private IServiceProvider serviceProvider;
        public IServiceProvider ServiceProvider => serviceProvider;

        private IConfigurationRoot configuration;

        public static bool FirstTimeSetupFinished => Settings.Default.FirstRunSetupFinished;

        public App()
        {
            InitializeConfig();

            InitializeServices();

            InitializeUI();
        }

        private void InitializeUI(IViewModel? viewModel = default)
        {
            viewModel ??= GetMainViewMode();

            if (MainWindow?.Content is ContentHostWindow)
            {
                var contentHost = MainWindow.Content as ContentHostWindow;
                contentHost.DataContext = viewModel;
            }
            else
            {
                var contentHost = new ContentHostWindow() { DataContext = viewModel};
                viewModel.Initialize();

                MainWindow = contentHost;
                MainWindow.Show();
            }
        }

        private IViewModel GetMainViewMode()
        {
            if (FirstTimeSetupFinished)
            {
                return new GamesViewModel(ServiceProvider.GetRequiredService<ISyncEngine>());
            }
            else
            {
                return new FirstSetupWizardViewModel(ServiceProvider.GetRequiredService<Navigator>());
            }
        }

        private void InitializeConfig()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            Configure(configBuilder);

            configuration = configBuilder.Build();
        }

        private void InitializeServices()
        {
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

            services.AddCiv6Filesystem(config);

            services.AddAzureSyncEngine(options =>
            {
                options.ConnectionString = config.GetConnectionString("AzureStorage");
                options.ContainerName = "game";
                options.TableName = "games";
                options.SystemPlayer = PlayerUtilities.GetSystemPlayer();
            });

            services.AddSingleton(new Navigator());
        }

        internal void ShowViewModel(IViewModel viewModel)
        {
            InitializeUI(viewModel);
        }
    }
}
