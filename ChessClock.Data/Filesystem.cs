using System.IO;
using System.Collections.Generic;
using ChessClock.Model;
using System;

namespace ChessClock.Data
{
    public static class Filesystem
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

        public static string GetHotSeatSaveFileFullName(string directory, string savefileName)
        {
            return Path.GetFullPath(Path.GetFileNameWithoutExtension(savefileName) + SaveExtension, directory);
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
