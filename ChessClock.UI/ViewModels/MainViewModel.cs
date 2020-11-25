using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChessClock.Model;
using ChessClock.SyncEngine;
using ChessClock.UI.Commands;

namespace ChessClock.UI.ViewModels
{
    public class MainViewModel : IViewModel
    {
        public Player SystemPlayer { get; set; } = Player.One;
        public ObservableCollection<Game> Games { get; set; }
        public string WindowTitle { get; set; } = "Separate Turn Distributor";
        public ISyncEngine SyncEngine { get; private set; }
        public MainWindow Window { get; set; }

        private Queue<ICommand> commandQueue;
        private bool initialized = false;

        public MainViewModel(ISyncEngine syncEngine)
        {
            SyncEngine = syncEngine;
            commandQueue = new Queue<ICommand>();
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

        public ICommand? NextCommand()
        {
            return commandQueue.TryDequeue(out ICommand command) ? command : null;
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
