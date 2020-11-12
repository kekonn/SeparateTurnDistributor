using ChessClock.Model;
using ChessClock.SyncEngine.Events;
using System.ComponentModel;

namespace ChessClock.SyncEngine
{
    public interface IAutoSyncStrategy
    {
        void GameSyncedSuccessfully(object sender, SuccessfullySyncedEventArgs e);
        bool ShouldSync(Game game);
    }
}
