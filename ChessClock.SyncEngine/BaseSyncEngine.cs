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
        private Timer autoSyncCheckTimer;

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

        private TimeSpan autoSyncInterval = TimeSpan.FromMinutes(1);

        public TimeSpan AutoSyncTriggerInterval
        {
            get
            {
                return autoSyncInterval;
            }
            set
            {
                if (value != autoSyncInterval)
                {
                    autoSyncInterval = value;
                    FirePropertiesChanged(nameof(AutoSyncTriggerInterval));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<MyTurnEventArgs> MyTurn;

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

        protected abstract IQueryable<Game> CreateGameSource();
        protected abstract IQueryable<Player> CreatePlayerSource();

        /// <summary>
        /// Method is called when either AutoSync is toggled or the AutoSync strategy has been changed
        /// </summary>
        protected virtual void AutoSyncStrategyChanged()
        {

        }

        public abstract Task SubmitTurn();
    }
}
