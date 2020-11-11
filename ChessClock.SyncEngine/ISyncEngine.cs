using ChessClock.Model;
using ChessClock.SyncEngine.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ChessClock.SyncEngine
{
    public interface ISyncEngine : INotifyPropertyChanged
    {
        bool AutoSync { get; set; }
        bool AutoSubmit { get; set; }
        IAutoSyncStrategy AutoSyncStrategy { get; set; }
        Player SystemPlayer { get; }

        event EventHandler<MyTurnEventArgs> MyTurn;
        event EventHandler<SuccessfullySyncedEventArgs> SuccessfullySynced;

        Task<IEnumerable<Game>> GamesForAsync(Player player);
        Task<Game> GetGameAsync(Guid id);
        Task<IEnumerable<Player>> GetPlayersAsync();
        ValueTask PassTurnAsync(Game game);
        ValueTask SubmitTurnAsync(Game game);
        ValueTask Sync();
    }
}
