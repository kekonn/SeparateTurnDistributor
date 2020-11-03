using ChessClock.Model;
using ChessClock.SyncEngine.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace ChessClock.SyncEngine
{
    public abstract class BaseSyncEngine : ISyncEngine, INotifyPropertyChanged
    {
        private Timer autoSyncIntervalTimer;

        private bool autoSync = true;
        public bool AutoSync
        {
            get
            {
                return autoSync;
            }
            set
            {
                if (value != autoSync)
                {
                    autoSync = value;
                    FirePropertiesChanged(nameof(AutoSync));
                    AutoSyncStrategyChanged();
                }
            }
        }

        public bool autoSubmit = false;
        public bool AutoSubmit
        {
            get
            {
                return autoSubmit;
            }
            set
            {
                if (value != autoSubmit)
                {
                    autoSubmit = value;
                    FirePropertiesChanged(nameof(AutoSubmit));
                }
            }
        }

        private IAutoSyncStrategy autoSyncStrategy = new DefaultAutoSyncStrategy();
        public IAutoSyncStrategy AutoSyncStrategy
        {
            get => autoSyncStrategy;
            set
            { 
                if (autoSyncStrategy != value)
                {
                    autoSyncStrategy = value;
                    AutoSyncStrategyChanged();
                }
            }
        }

        /// <summary>
        /// The system player decides the perspective for the SyncEngine
        /// </summary>
        public Player SystemPlayer { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<MyTurnEventArgs> MyTurn;
        public event EventHandler<SuccessfullySyncedEventArgs> SuccessfullySynced;

        /// <summary>
        /// Initializes a sync engine for a system player
        /// </summary>
        /// <param name="player">The system player. This is the person from whose perspective we are syncing.</param>
        public BaseSyncEngine(Player player)
        {
            SystemPlayer = player;
        }

        /// <summary>
        /// Allows you to fire the PropertyChanged Event for multiple properties at once
        /// </summary>
        /// <param name="properties">A list of property names. It is best to use the nameof keyword</param>
        protected void FirePropertiesChanged(params string[] properties)
        {
            foreach (var prop in properties)
            {
                var args = new PropertyChangedEventArgs(prop);
                var handler = PropertyChanged;
                handler?.Invoke(this, args);
            }
        }

        /// <summary>
        /// Fires the MyTurn event
        /// </summary>
        /// <param name="args">An instance of MyTurnEventArgs</param>
        protected virtual void OnMyTurnReached(MyTurnEventArgs args)
        {
            var handler = MyTurn;
            handler?.Invoke(this, args);
        }

        /// <summary>
        /// Fires the SuccessfullySynced Event
        /// </summary>
        /// <param name="args">Event arguments containing sync time and the game that was synced</param>
        protected virtual void OnSuccesfullySynced(SuccessfullySyncedEventArgs args)
        {
            var handler = SuccessfullySynced;
            handler?.Invoke(this, args);
        }
        
        /// <summary>
        /// Asynchronously gets all games that involve a given player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<Game>> GamesForAsync(Player player)
        {
            return Task.FromResult(CreateGameSource().Where(g => g.Players.Contains(player)).AsEnumerable());
        }

        /// <summary>
        /// Gets a given game
        /// </summary>
        /// <param name="id">The Id of the Game</param>
        /// <returns>The game</returns>
        public Task<Game> GetGameAsync(Guid id)
        {
            return Task.FromResult(CreateGameSource().First(g => g.Id.Equals(id)));
        }

        /// <summary>
        /// Get all visible players
        /// </summary>
        /// <returns>An IEnumerable of all visible players</returns>
        public Task<IEnumerable<Player>> GetPlayersAsync()
        {
            return Task.FromResult(CreatePlayerSource().ToArray().AsEnumerable());
        }

        /// <summary>
        /// Method is called when either AutoSync is toggled or the AutoSync strategy has been changed
        /// </summary>
        protected virtual void AutoSyncStrategyChanged()
        {
            SuccessfullySynced = null;
            SuccessfullySynced = autoSyncStrategy.GameSyncedSuccesfully;

            if (autoSyncStrategy is DefaultAutoSyncStrategy)
            {
                var defaultSyncStrat = autoSyncStrategy as DefaultAutoSyncStrategy;
                if (autoSyncIntervalTimer == null)
                {
                    autoSyncIntervalTimer = new Timer();
                    autoSyncIntervalTimer.Stop();
                }
                
            }
        }

        /// <summary>
        /// This method is called when the AutoSync timer has ellapsed
        /// </summary>
        /// <remarks>This is not the only event through which an AutoSync event can occur</remarks>
        protected virtual void AutoSyncTimerElapsed()
        {
            var gamesToCheck = GamesForAsync(SystemPlayer).GetAwaiter().GetResult().Where(g => autoSyncStrategy.ShouldSync(g));

            foreach (var game in gamesToCheck)
            {
                Sync(game);
            }
        }

        public virtual void Sync(Game game)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Orders the SyncEngine to submit the current turn for the given game
        /// </summary>
        /// <param name="game">The game to submit</param>
        /// <returns>An awaitable task</returns>
        public abstract Task SubmitTurnAsync(Game game);

        protected abstract IQueryable<Game> CreateGameSource();
        protected abstract IQueryable<Player> CreatePlayerSource();
    }
}
