using ChessClock.Model;
using System;

namespace ChessClock.SyncEngine.Events
{
    public class MyTurnEventArgs : BaseGameEventArgs
    {
        public MyTurnEventArgs(Game game) : base(game)
        {
        }
    }
}
