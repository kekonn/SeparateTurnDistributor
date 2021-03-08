using System;
using System.Threading.Tasks;
using ChessClock.Model;
using ChessClock.UI.Extensions;
using ChessClock.UI.Views;

namespace ChessClock.UI.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private bool initialized = false;
        private readonly Game game;

        public override string Title
        {
            get => game.Name;
            set
            {
                if (game.Name == value) return;

                game.Name = value;
                OnPropertyChanged();
            }
        }

        public Game Game => game;

        public Player CurrentPlayer => game.CurrentPlayer;

        public GameViewModel(Game game)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public override void Initialize()
        {
            if (initialized) return;

            InitializeAsync().Await();
        }

        public override ValueTask InitializeAsync()
        {
            if (initialized) return ValueTask.CompletedTask;

            View = new GameView() {DataContext = this};

            initialized = true;

            return ValueTask.CompletedTask;
        }

        public bool Equals(Game otherGame)
        {
            return game.Equals(otherGame);
        }
    }
}
