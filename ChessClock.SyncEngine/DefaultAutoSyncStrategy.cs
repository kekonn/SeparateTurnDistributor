using ChessClock.Model;
using ChessClock.SyncEngine.Events;
using System;
using System.Collections.Generic;

namespace ChessClock.SyncEngine
{
    internal class DefaultAutoSyncStrategy : IAutoSyncStrategy
    {
        internal TimeSpan MinimumInterval { get; private set; } = TimeSpan.FromMinutes(1);

        private static readonly object LockObject = new object();

        private readonly Dictionary<Game, DateTimeOffset> lastSyncedTimes = new Dictionary<Game, DateTimeOffset>();
        private readonly Player systemPlayer;

        internal DefaultAutoSyncStrategy(Player systemPlayer)
        {
            this.systemPlayer = systemPlayer;
        }

        /// <summary>
        /// Event handler for when the game was synced successfully.
        /// </summary>
        /// <param name="sender">the sender of the event</param>
        /// <param name="e">Event arguments</param>
        public void GameSyncedSuccessfully(object sender, SuccessfullySyncedEventArgs e)
        {
            var syncTime = e.SyncTime;
            var game = e.Game;

            lock (LockObject)
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
        public bool ShouldSync(Game game)
        {
            if (IsMyTurn(game)) return false;

            lock (LockObject)
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

        private bool IsMyTurn(Game game)
        {
            return game.CurrentPlayer == systemPlayer;
        }
    }
}
