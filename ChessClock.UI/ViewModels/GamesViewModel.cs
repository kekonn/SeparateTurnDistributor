using System.Collections.ObjectModel;
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
        private ObservableCollection<Game> games = new ObservableCollection<Game>();
        private bool initialized = false;
        private Game? selectedGame;

        public Player SystemPlayer => SyncEngine.SystemPlayer;

        public ObservableCollection<Game> Games
        {
            get => games;
            set
            {
                if (games == value) return;

                games = value;
                OnPropertyChanged();
            }
        }

        public Game? SelectedGame
        {
            get => selectedGame;
            set
            {
                if (selectedGame == value) return;

                selectedGame = value;
                OnPropertyChanged();
            }
        }
        public ISyncEngine SyncEngine { get; }
        public ICommand ForceSyncCommand { get; set; }

        public GamesViewModel(ISyncEngine syncEngine)
        {
            SyncEngine = syncEngine;
            Title = "Separate Turn Distributor";
            View = new GamesView {DataContext = this};
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
            Games = new ObservableCollection<Game>(gamesList);
        }
    }
}
