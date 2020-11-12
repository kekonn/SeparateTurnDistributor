using System;
using System.Collections.Generic;
using System.IO;
using ChessClock.Model;

namespace ChessClock.SyncEngine
{
    public static class Civ6Filesystem
    {
        private const string DefaultSavePath = @"%USERPROFILE%\Documents\My Games\Sid Meier's Civilization VI\Saves\Hotseat\";
        private const string SaveExtension = ".Civ6Save";

        public static IEnumerable<string> GetHotSeatSaves(string directory = DefaultSavePath)
        {
            if (!Directory.Exists(directory))
            {
                throw new FileNotFoundException("The directory does not seem to exist", directory);
            }

            return Directory.GetFiles(directory, $"*.{SaveExtension}");
        }

        /// <summary>
        /// Gets the expected file name of a game
        /// </summary>
        /// <param name="game">The game to get the filename off</param>
        /// <returns>The filename, NOT the full path</returns>
        public static string GetSaveFileName(Game game) => $"{game.SavefileName}{SaveExtension}";

        private static string GetHotSeatSaveFileFullName(string directory, string savefileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(savefileName) + SaveExtension;
            return Path.GetFullPath(Path.Combine(directory, fileName));
        }

        public static DateTimeOffset GetSaveFileLastWrite(Game game, string directory)
        {
            var fileName = GetSaveFileName(game);
            fileName = GetHotSeatSaveFileFullName(directory, fileName);

            var baseTime = File.GetLastWriteTime(fileName);

            return new DateTimeOffset(baseTime, TimeZoneInfo.Local.GetUtcOffset(baseTime));
        }
    }
}
