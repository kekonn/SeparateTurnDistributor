using ChessClock.Model;
using ChessClock.SyncEngine.Events;
using System;
using System.Collections.Generic;

namespace ChessClock.SyncEngine
{
    internal class DefaultAutoSyncStrategy : IAutoSyncStrategy
    {
        internal TimeSpan MinimumInterval { get; private set; } = TimeSpan.FromMinutes(1);

        private static object lockObject = new object();

        private Dictionary<Game, DateTimeOffset> lastSyncedTimes = new Dictionary<Game, DateTimeOffset>();

        public virtual void GameSyncedSuccesfully(object sender, SuccessfullySyncedEventArgs e)
        {
            var syncTime = e.SyncTime;
            var game = e.Game;

            lock (lockObject)
            {
                if (lastSyncedTimes.ContainsKey(game))
                {
                    lastSyncedTimes[game] = syncTime;
                }
                else
                {
                    lastSyncedTimes.Add(game, syncTime);
                }
            }
        }

        /// <summary>
        /// Checks if a game should sync. This implementation is very naive and simply checks if more than 60 seconds have passed since the last sync
        /// </summary>
        /// <param name="game">the game to perform the check on</param>
        /// <returns>True if the game should sync, false if otherwise</returns>
        /// <remarks>This method is thread safe because it locks before changing internal state.</remarks>
        public virtual bool ShouldSync(Game game)
        {
            lock (lockObject)
            {
                var isKnown = lastSyncedTimes.ContainsKey(game);
                if (!isKnown)
                {
                    return true;
                }

                var lastSyncTime = lastSyncedTimes[game];

                return (DateTimeOffset.UtcNow - lastSyncTime).TotalSeconds >= MinimumInterval.TotalSeconds;
            }
        }
    }
}
