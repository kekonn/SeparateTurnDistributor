using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        public ISyncEngine SyncEngine { get; }

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

        private async ValueTask InitGamesList()
        {
            var gamesList = await SyncEngine.GamesForAsync(SystemPlayer);
            Games = new ObservableCollection<Game>(gamesList);
        }

        public override async ValueTask InitializeAsync()
        {
            if (initialized) return;

            await InitGamesList();

            initialized = true;
        }
    }
}
