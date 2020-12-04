using System;
using System.Linq;
using ChessClock.Model;
using ChessClock.SyncEngine;
using ChessClock.UI.ViewModels;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace ChessClock.UI.Tests
{
    public class GameViewModelTests
    {
        private readonly ISyncEngine mockSyncEngine;
        private readonly ITestOutputHelper outputHelper;
        private readonly Game[] games;
        private readonly Player[] players = { Player.One, new Player { Id = Guid.NewGuid(), Name = "Player Two"} };
        private readonly GamesViewModel viewModel;

        public GameViewModelTests(ITestOutputHelper testOutputHelper)
        {
            outputHelper = testOutputHelper;
            games = new[]
            {
                new Game("Test game", players, Player.One)
            };
            mockSyncEngine = new SyncEngineTestImpl(Player.One, CreateLogger<ISyncEngine>(), games);
            viewModel = new GamesViewModel(mockSyncEngine);
            viewModel.Initialize();
        }

        [WpfFact]
        public void VMInitialized()
        {
            Assert.NotNull(viewModel.SyncEngine);
            Assert.NotNull(viewModel.ForceSyncCommand);
            Assert.NotNull(viewModel.Games);
            Assert.True(viewModel.Games.Any());
            Assert.NotNull(viewModel.NextTurnCommand);
        }

        [WpfFact]
        public void SelectedGameDetection()
        {
            viewModel.SelectedGame = games[0];

            Assert.NotNull(viewModel.SelectedGame);
            Assert.True(games[0].Equals(viewModel.SelectedGame));
        }

        [WpfFact]
        public void CanForceSync()
        {
            viewModel.SelectedGame = viewModel.Games[0];

            Assert.NotNull(viewModel.SelectedGame);
            Assert.NotNull(viewModel.ForceSyncCommand);
            Assert.True(viewModel.ForceSyncCommand.CanExecute(null));
        }

        [WpfFact]
        public void CanNextTurn()
        {
            viewModel.SelectedGame = viewModel.Games[0];

            Assert.NotNull(viewModel.SelectedGame);
            Assert.NotNull(viewModel.NextTurnCommand);
            Assert.True(viewModel.NextTurnCommand.CanExecute(null));
        }

        [WpfFact]
        public void CantNextTurnWhenNotMyTurn()
        {
            Assert.NotNull(viewModel.NextTurnCommand);

            viewModel.SelectedGame = viewModel.Games[0];
            Assert.NotNull(viewModel.SelectedGame);
            Assert.True(viewModel.NextTurnCommand.CanExecute(null));
            
            viewModel.NextTurnCommand.Execute(null);
            Assert.False(viewModel.NextTurnCommand.CanExecute(null));
            
        }

        private ILogger<TClass> CreateLogger<TClass>()
        {
            return outputHelper.BuildLoggerFor<TClass>();
        }
    }
}
