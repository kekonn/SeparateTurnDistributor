using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessClock.Model;
using ChessClock.SyncEngine;

namespace ChessClock.UI.ViewModels
{
    public class MainViewModel
    {
        public Player SystemPlayer { get; set; }
        public ObservableCollection<Game> Games { get; set; }
        public string WindowTitle { get; set; } = "Separate Turn Distributor";

        public ISyncEngine SyncEngine { get; private set; }

        public MainViewModel(ISyncEngine syncEngine)
        {
            SyncEngine = syncEngine;
        }

    }
}
