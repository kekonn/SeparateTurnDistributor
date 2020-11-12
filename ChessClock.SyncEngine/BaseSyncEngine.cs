﻿using ChessClock.Model;
using ChessClock.SyncEngine.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace ChessClock.SyncEngine
{
    public abstract class BaseSyncEngine : ISyncEngine
    {
        private Timer? autoSyncIntervalTimer;

        private bool autoSync = true;
        /// <summary>
        /// AutoSync sets if the server is allowed to check for new turns automatically
        /// </summary>
        public bool AutoSync
        {
            get => autoSync;
            set
            {
                if (value == autoSync) return;

                autoSync = value;
                FirePropertiesChanged(nameof(AutoSync));
                AutoSyncStrategyChanged();
            }
        }

        private bool autoSubmit = false;
        /// <summary>
        /// AutoSubmit decides if you want to automatically submit your turn on a file change trigger
        /// </summary
        public bool AutoSubmit
        {
            get => autoSubmit;
            set
            {
                if (value == autoSubmit) return;

                autoSubmit = value;
                FirePropertiesChanged(nameof(AutoSubmit));
            }
        }

        private IAutoSyncStrategy autoSyncStrategy;
        /// <summary>
        /// Sets the AutoSync strategy
        /// </summary>
        public IAutoSyncStrategy AutoSyncStrategy
        {
            get => autoSyncStrategy;
            set
            {
                if (autoSyncStrategy == value) return;

                autoSyncStrategy = value;
                AutoSyncStrategyChanged();
            }
        }

        protected readonly ILogger<BaseSyncEngine> Logger;

        /// <summary>
        /// The system player decides the perspective for the SyncEngine
        /// </summary>
        public Player SystemPlayer { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged = null;
        public event EventHandler<MyTurnEventArgs>? MyTurn = null;
        public event EventHandler<SuccessfullySyncedEventArgs>? SuccessfullySynced = null;

        /// <summary>
        /// Initializes a sync engine for a system player
        /// </summary>
        /// <param name="player">The system player. This is the person from whose perspective we are syncing.</param>
        /// <param name="autoSyncStrategy">The IAutoSync strategy to use, can always be changed later through the property</param>
        /// <param name="logger">The logger to use</param>
        protected BaseSyncEngine(Player player, IAutoSyncStrategy? autoSyncStrategy, ILogger<BaseSyncEngine> logger)
        {
            SystemPlayer = player;
            this.autoSyncStrategy = autoSyncStrategy ?? new DefaultAutoSyncStrategy(SystemPlayer);
            Logger = logger;
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
        protected virtual void OnSuccessfullySynced(SuccessfullySyncedEventArgs args)
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
            return Task.FromResult(CreateGameSource().Where(g => g.Players.Contains(player)).ToArray().AsEnumerable());
        }

        /// <summary>
        /// Gets a given game
        /// </summary>
        /// <param name="id">The Id of the Game</param>
        /// <returns>The game</returns>
        public Task<Game> GetGameAsync(Guid id)
        {
            return Task.FromResult(CreateGameSource().FirstOrDefault(g => g.Id.Equals(id)));
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
            SuccessfullySynced = autoSyncStrategy.GameSyncedSuccessfully;

            if (!(autoSyncStrategy is DefaultAutoSyncStrategy defaultSyncStrat)) return;

            autoSyncIntervalTimer ??= new Timer();

            autoSyncIntervalTimer.Stop();
            autoSyncIntervalTimer.Interval = defaultSyncStrat.MinimumInterval.TotalMilliseconds;
            autoSyncIntervalTimer.Start();
        }

        /// <summary>
        /// This method is called when the AutoSync timer has elapsed
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

        /// <summary>
        /// Sync all known games
        /// </summary>
        /// <returns>An awaitable task</returns>
        public virtual async ValueTask Sync()
        {
            var games = await GamesForAsync(SystemPlayer);

            var syncTasks = games.Where(g => autoSyncStrategy.ShouldSync(g))
                .Select(g => Task.Run(() => Sync(g))).ToArray();

            Task.WaitAll(syncTasks);
        }

        /// <summary>
        /// Syncs the given game
        /// </summary>
        /// <param name="game">The game to sync</param>
        protected abstract void Sync(Game game);

        /// <summary>
        /// Submits the turn for the given game
        /// </summary>
        /// <param name="game">The game to submit</param>
        /// <returns>An awaitable task</returns>
        public abstract ValueTask SubmitTurnAsync(Game game);

        /// <summary>
        /// Passes the turn to the next player, without uploading the save game
        /// </summary>
        /// <param name="game">The game for which to pass a turn</param>
        /// <returns>An awaitable task</returns>
        public abstract ValueTask PassTurnAsync(Game game);

        /// <summary>
        /// Creates a queryable source of games.
        /// </summary>
        /// <remarks>Lazy loading is preferred</remarks>
        /// <returns>Queryable enumeration of games</returns>
        protected abstract IQueryable<Game> CreateGameSource();

        /// <summary>
        /// Creates a queryable source of players
        /// </summary>
        /// <remarks>Lazy loading is preferred</remarks>
        /// <returns>Queryable enumeration of players</returns>
        protected virtual IQueryable<Player> CreatePlayerSource()
        {
            return CreateGameSource().SelectMany(g => g.Players).Distinct();
        }

        /// <summary>
        /// Retrieve the last modification time as shown by the server.
        /// </summary>
        /// <param name="game">Which game's modification time to check</param>
        /// <returns>A DateTimeOffset for the last modified time</returns>
        protected abstract DateTimeOffset GetGameLastModifiedTime(Game game);

        /// <summary>
        /// Gets the latest modification time for the remote save game.
        /// </summary>
        /// <param name="game">Which game's save file to check</param>
        /// <returns>A DateTimeOffset for the last modified time</returns>
        protected abstract DateTimeOffset GetRemoteSavefileLastModifiedTime(Game game);

        /// <summary>
        /// Gets the latest modification time for the local save game.
        /// </summary>
        /// <param name="game">Which game's save file to check</param>
        /// <returns>A DateTimeOffset for the last modified time</returns>
        protected abstract DateTimeOffset GetLocalSavefileLastModifiedTime(Game game);
    }
}
