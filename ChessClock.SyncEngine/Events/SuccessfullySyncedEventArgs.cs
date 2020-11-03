using ChessClock.Model;
using System;

namespace ChessClock.SyncEngine.Events
{
    public class SuccessfullySyncedEventArgs : BaseGameEventArgs
    {
        public DateTimeOffset SyncTime { get; protected set; }

        public SuccessfullySyncedEventArgs(Game game, DateTimeOffset? syncTime) : base(game)
        {
            SyncTime = syncTime ?? DateTimeOffset.Now;
        }
    }
}
