using System;
using System.Collections.Generic;
using System.Linq;
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
            this.game = game;
        }

        public override void Initialize()
        {
            if (initialized) return;

            InitializeAsync().Await();
        }

        public async override ValueTask InitializeAsync()
        {
            if (initialized) return;

            View = new GameView() {DataContext = this};

            initialized = true;
        }
    }
}
