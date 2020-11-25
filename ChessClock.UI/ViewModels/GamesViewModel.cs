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
using ChessClock.UI.Commands;
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

            InitSystemPlayer();
            InitGamesList();

            initialized = true;
        }

        private void InitGamesList()
        {
            if (SystemPlayer == Player.One)
            {
                return;
            }
        }

        public async ValueTask InitializeAsync()
        {
            await Task.Run(Initialize);
        }

        public IEnumerable<ICommand?> Commands
        {
            get
            {
                yield return commandQueue.TryDequeue(out ICommand command) ? command : new EmptyCommand();
            }
        }

        private void InitSystemPlayer()
        {
            var player = PlayerUtilities.GetSystemPlayer();
            if (player == Player.One)
            {
                commandQueue.Enqueue(new SystemPlayerSetupCommand());
                return;
            }

        }
    }
}
