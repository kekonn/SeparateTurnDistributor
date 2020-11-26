using ChessClock.Model;
using ChessClock.UI.Properties;
using System;
using System.Linq;
using System.Text;
using NLog;

namespace ChessClock.UI
{
    public static class PlayerUtilities
    {
        private static readonly ILogger Logger = LogManager.GetLogger(nameof(PlayerUtilities));
        private static Player systemPlayer = Player.One;

        static PlayerUtilities()
        {
            var playerName = Settings.Default.PlayerName;
            var playerSeed = Settings.Default.PlayerSeed;
            if (playerName is {Length: 0} || playerSeed is {Length: 0})
            {
                Settings.Default.Reset();

                systemPlayer = Player.One;
            }
            else
            {
                systemPlayer = FromSeed(playerName, playerSeed);
            }
        }

        public static Player GetSystemPlayer()
        {
            return systemPlayer;
        }

        public static string GetSystemPlayerSeed()
        {
            return LoadSystemIdentifier();
        }

        private static string LoadSystemIdentifier()
        {
            var storedIdentifier = Settings.Default.SystemIdentifier;

            if (string.IsNullOrEmpty(storedIdentifier))
            {
                storedIdentifier = CalculateSystemIdentifier();

                Settings.Default.SystemIdentifier = storedIdentifier;
            }

            return storedIdentifier.ToLower();
        }

        private static string CalculateSystemIdentifier()
        {
            var systemName = Environment.MachineName;
            var textBytes = Encoding.UTF8.GetBytes(systemName);

            while (textBytes.Length < 16)
            {
                var newBytes = Encoding.UTF8.GetBytes(systemName);
                textBytes = textBytes.Concat(newBytes).ToArray();
            }

            return Convert.ToBase64String(textBytes.Take(16).ToArray()).ToLower();
        }

        public static void SaveSystemPlayer(Player player)
        {
            Settings.Default.PlayerId = player.Id;
            Settings.Default.PlayerName = player.Name;
            Settings.Default.PlayerSeed = GetSystemPlayerSeed();

            Settings.Default.Save();

            systemPlayer = player;
        }

        public static Player FromSeed(string name, string seed)
        {
            Logger.Trace($"Creating player {name} from seed {seed}");

            var player = new Player
            {
                Name = name,
                Id = new Guid(Convert.FromBase64String(seed))
            };

            return player;
        }
    }
}
