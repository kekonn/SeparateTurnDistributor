using ChessClock.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessClock.SyncEngine
{
    internal class DefaultAutoSyncStrategy : IAutoSyncStrategy
    {
        internal TimeSpan MinimumInterval { get; private set; } = TimeSpan.FromMinutes(1);

        private Dictionary<Game, DateTimeOffset> lastSyncedTimes = new Dictionary<Game, DateTimeOffset>();

        public virtual void HasSyncedSuccesfully(Game game)
        {

        }

        public virtual bool ShouldSync(Game game)
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
