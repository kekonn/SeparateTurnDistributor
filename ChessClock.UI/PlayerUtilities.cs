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
        private static Player _systemPlayer = Player.One;

        public static Player LoadSystemPlayer()
        {
            Logger.Trace("Loading System Player");

            if (_systemPlayer != Player.One)
            {
                return _systemPlayer;
            }

            var systemId = LoadSystemIdentifier();
            var playerId = Settings.Default.PlayerId;
            var playerName = Settings.Default.PlayerName;

            if (playerId == Guid.Empty)
            {
                byte[] guidBytes = Convert.FromBase64String(systemId);
                playerId = new Guid(guidBytes);

                Settings.Default.PlayerId = playerId;
                Settings.Default.Save();
            }

            _systemPlayer = new Player()
            {
                Id = playerId,
                Name = playerName
            };

            Logger.Trace($"Found System Player: {_systemPlayer}");


            return _systemPlayer;
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
                Settings.Default.Save();
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

            Settings.Default.Save();
        }

        public static Player FromSeed(string name, string seed)
        {
            Logger.Trace($"Creating player {name} from seed {seed}");

            var player = new Player() { Name = name };
            player.Id = new Guid(Convert.FromBase64String(seed));

            return player;
        }
    }
}
