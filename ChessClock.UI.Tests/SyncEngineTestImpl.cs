using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessClock.Model;
using ChessClock.SyncEngine;
using Microsoft.Extensions.Logging;

namespace ChessClock.UI.Tests
{
    internal class SyncEngineTestImpl : BaseSyncEngine
    {
        private readonly Game[] games;

        public SyncEngineTestImpl(Player player, ILogger<ISyncEngine> logger, Game[] games) : base(player, null, logger)
        {
            this.games = games;
        }

        protected override void Sync(Game game)
        {
            Logger.LogDebug($"Syncing game {game}");
        }

        public override ValueTask SubmitTurnAsync(Game game)
        {
            return PassTurnAsync(game);
        }

        public override ValueTask PassTurnAsync(Game game)
        {
            Logger.LogTrace($"Moving turn on game {game}");
            game.NextTurn();
            return ValueTask.CompletedTask;
        }

        protected override IQueryable<Game> CreateGameSource()
        {
            return games.AsQueryable();
        }

        protected override DateTimeOffset GetGameLastModifiedTime(Game game)
        {
            return DateTimeOffset.Now.AddHours(-1);
        }

        protected override DateTimeOffset GetRemoteSavefileLastModifiedTime(Game game)
        {
            return DateTimeOffset.MinValue;
        }

        protected override DateTimeOffset GetLocalSavefileLastModifiedTime(Game game)
        {
            return DateTimeOffset.Now;
        }
    }
}
