using ChessClock.Model;
using System;

namespace ChessClock.SyncEngine.Events
{
    public abstract class BaseGameEventArgs : EventArgs
    {
        public Game Game { get; protected set; }

        public BaseGameEventArgs(Game game) : base()
        {
            Game = game;
        }
    }
}
