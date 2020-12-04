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
        public SyncEngineTestImpl(Player player, ILogger<ISyncEngine> logger) : base(player, null, logger)
        {
        }

        protected override void Sync(Game game)
        {
            throw new NotImplementedException();
        }

        public override ValueTask SubmitTurnAsync(Game game)
        {
            throw new NotImplementedException();
        }

        public override ValueTask PassTurnAsync(Game game)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Game> CreateGameSource()
        {
            throw new NotImplementedException();
        }

        protected override DateTimeOffset GetGameLastModifiedTime(Game game)
        {
            throw new NotImplementedException();
        }

        protected override DateTimeOffset GetRemoteSavefileLastModifiedTime(Game game)
        {
            throw new NotImplementedException();
        }

        protected override DateTimeOffset GetLocalSavefileLastModifiedTime(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
