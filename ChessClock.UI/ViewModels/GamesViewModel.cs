using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ChessClock.Model;
using ChessClock.SyncEngine;
using ChessClock.UI.Extensions;
using ChessClock.UI.Views;

namespace ChessClock.UI.ViewModels
{
    public class GamesViewModel : IViewModel
    {
        public Player SystemPlayer { get; set; } = Player.One;
        public ObservableCollection<Game> Games { get; set; } = new ObservableCollection<Game>();
        public string Title { get; set; } = "Separate Turn Distributor";
        public ISyncEngine SyncEngine { get; private set; }
        public ContentControl View { get; private set; }

        private Queue<ICommand> commandQueue;
        private bool initialized = false;

        public GamesViewModel(ISyncEngine syncEngine)
        {
            SyncEngine = syncEngine;
            commandQueue = new Queue<ICommand>();
            View = new GamesView {DataContext = this};
        }

        public void Initialize()
        {
            if (initialized) return;

            InitializeAsync().Await();
        }

        private async ValueTask InitGamesList()
        {
            var gamesList = await SyncEngine.GamesForAsync(SystemPlayer);
            Games = new ObservableCollection<Game>(gamesList);
        }

        public async ValueTask InitializeAsync()
        {
            if (initialized) return;

            await InitGamesList();

            initialized = true;
        }
    }
}
