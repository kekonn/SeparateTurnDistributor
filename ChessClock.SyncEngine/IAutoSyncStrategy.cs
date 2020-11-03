using ChessClock.Model;
using System;
using System.Collections.Generic;

namespace ChessClock.SyncEngine
{
    public interface IAutoSyncStrategy
    {

        bool ShouldSync(Game game);
    }
}
