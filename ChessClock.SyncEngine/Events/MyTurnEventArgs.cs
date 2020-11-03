using ChessClock.Model;
using System;

namespace ChessClock.SyncEngine.Events
{
    public class MyTurnEventArgs : EventArgs
    {
        public Game Game { get; set; }
    }
}
