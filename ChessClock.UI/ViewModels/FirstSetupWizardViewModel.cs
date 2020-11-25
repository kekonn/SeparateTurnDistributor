using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ChessClock.UI.Properties;
using ChessClock.UI.Views;

namespace ChessClock.UI.ViewModels
{
    public class FirstSetupWizardViewModel : IViewModel
    {
        public string Title { get; set; } = "Player Setup";

        public string PlayerName { get; set; }

        public string PlayerSeed { get; set; }

        public ICommand PlayerSetupDoneCommand { get; }

        private readonly Navigator navigator;

        public FirstSetupWizardViewModel(Navigator navigator)
        {
            View = new WizardHost() {DataContext = this};
            PlayerSeed = PlayerUtilities.GetSystemPlayerSeed();
            PlayerName = Settings.Default.PlayerName;
            PlayerSetupDoneCommand = new ActionCommand(p => PlayerName is {Length: > 0} && PlayerSeed is {Length: > 0},
                p => PlayerSetupDone());
            this.navigator = navigator;
        }

        public void Initialize() {}

        public ValueTask InitializeAsync() => ValueTask.CompletedTask;

        public ContentControl View { get; }

        private void PlayerSetupDone()
        {
            var player = PlayerUtilities.FromSeed(PlayerName, PlayerSeed);
            PlayerUtilities.SaveSystemPlayer(player);
            
            Settings.Default.FirstRunSetupFinished = true;
            Settings.Default.Save();

            navigator.ShowGamesView();
        }
    }
}
