using System.Reflection;
using ChessClock.Data.Azure;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using ChessClock.Data;

namespace ChessClock.UI
{
    internal class ChessClockApplicationContext : ApplicationContext
    {
        private const string StorageConnectionString = "StorageConnectionString";
        private const string IniSettingsFilePath = "settings.ini";
        private readonly IGameRepository gamesRepo;
        private IConfiguration configuration;

        public ChessClockApplicationContext() : base()
        {
            InitConfig();

            gamesRepo = new AzureBlobGameRepository(configuration.GetConnectionString(StorageConnectionString));

            MainForm = BuildMainForm();
        }

        private ClockForm BuildMainForm()
        {
            var systemPlayer = PlayerUtilities.LoadSystemPlayer();

            return new ClockForm()
            {
                GameRepository = gamesRepo,
                SystemPlayer = systemPlayer
            };
        }

        private void InitConfig()
        {
            var configBuilder = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly(), true);
            configBuilder.AddIniFile(IniSettingsFilePath, true);
            configuration = configBuilder.Build();
        }
    }
}
