using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessClock.Model;
using ChessClock.SyncEngine;
using ChessClock.UI.Extensions;
using ChessClock.UI.Views;

namespace ChessClock.UI.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private ObservableCollection<GameViewModel> games = new ObservableCollection<GameViewModel>();
        private bool initialized = false;
        private GameViewModel? selectedGame;

        public Player SystemPlayer => SyncEngine.SystemPlayer;

        public ObservableCollection<GameViewModel> Games
        {
            get => games;
            set
            {
                if (games == value) return;

                games = value;
                OnPropertyChanged();
            }
        }

        public GameViewModel? SelectedGame
        {
            get => selectedGame;
            set
            {
                if (selectedGame == value) return;

                selectedGame = value;
                OnPropertyChanged();
            }
        }

        public bool AutoSync
        {
            get => SyncEngine.AutoSync;
            set
            {
                if (SyncEngine.AutoSync == value) return;

                SyncEngine.AutoSync = value;
                OnPropertyChanged();
            }
        }
        public ISyncEngine SyncEngine { get; }
        public ICommand ForceSyncCommand { get; }
        public ICommand NextTurnCommand { get; }

        public GamesViewModel(ISyncEngine syncEngine)
        {
            SyncEngine = syncEngine;
            Title = "Separate Turn Distributor";
            View = new GamesView {DataContext = this};
            
            //Command setup. Extracting this to a method would require us to add setters to the properties
            ForceSyncCommand = new ActionCommand(ForceSyncCanExecute, ForceSync);
            NextTurnCommand = new ActionCommand(obj => IsMyTurn(), obj => NextTurn().Await());
        }

        public override void Initialize()
        {
            if (initialized) return;

            InitializeAsync().Await();
        }

        public override async ValueTask InitializeAsync()
        {
            if (initialized) return;

            await InitGamesList();

            initialized = true;
        }

        private async ValueTask InitGamesList()
        {
            var gamesList = await SyncEngine.GamesForAsync(SystemPlayer);
            Games = new ObservableCollection<GameViewModel>(gamesList.Select(g => new GameViewModel(g)));
        }

        private bool ForceSyncCanExecute(object? parameter)
        {
            return !selectedGame?.Equals(null) ?? false;
        }

        private async void ForceSync(object? parameter)
        {
            await SyncEngine.Sync();
            //TODO: actually update vm
        }

        private bool IsMyTurn()
        {
            return selectedGame?.CurrentPlayer == SystemPlayer;
        }

        private async Task NextTurn()
        {
            if (SelectedGame == null) return;

            await SyncEngine.SubmitTurnAsync(SelectedGame.Game);
            //TODO: Update VM
        }
    }
}
