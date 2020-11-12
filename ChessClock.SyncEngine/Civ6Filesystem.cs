using System;
using System.IO;
using ChessClock.Model;

namespace ChessClock.SyncEngine
{
    public sealed class Civ6Filesystem
    {
        private const string SaveExtension = ".Civ6Save";

        private readonly string saveDir;

        public Civ6Filesystem(string saveDir)
        {
            this.saveDir = saveDir;
        }

        /// <summary>
        /// Gets the expected file name of a game
        /// </summary>
        /// <param name="game">The game to get the filename off</param>
        /// <returns>The filename, NOT the full path</returns>
        public static string GetSaveFileName(Game game) => $"{game.SavefileName}{SaveExtension}";

        private string GetHotSeatSaveFileFullName(string savefileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(savefileName) + SaveExtension;
            return Path.GetFullPath(Path.Combine(saveDir, fileName));
        }

        public string GetHotSeatSaveFileFullName(Game game) => GetHotSeatSaveFileFullName(game.SavefileName);

        public DateTimeOffset GetSaveFileLastWrite(Game game)
        {
            var fileName = GetSaveFileName(game);
            fileName = GetHotSeatSaveFileFullName(fileName);

            var baseTime = File.GetLastWriteTime(fileName);

            return new DateTimeOffset(baseTime, TimeZoneInfo.Local.GetUtcOffset(baseTime));
        }
    }
}
