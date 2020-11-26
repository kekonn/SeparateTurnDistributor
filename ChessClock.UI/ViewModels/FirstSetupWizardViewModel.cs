using System.Threading.Tasks;
using System.Windows.Input;
using ChessClock.UI.Properties;
using ChessClock.UI.Views;

namespace ChessClock.UI.ViewModels
{
    public class FirstSetupWizardViewModel : BaseViewModel
    {

        public string PlayerName { get; set; }

        public string PlayerSeed { get; set; }

        public ICommand PlayerSetupDoneCommand { get; }

        private readonly Navigator navigator;

        public FirstSetupWizardViewModel(Navigator navigator)
        {
            Title = "Player Setup";
            View = new WizardHost() {DataContext = this};
            PlayerSeed = PlayerUtilities.GetSystemPlayerSeed();
            PlayerName = Settings.Default.PlayerName;
            PlayerSetupDoneCommand = new ActionCommand(p => PlayerName is {Length: > 0} && PlayerSeed is {Length: > 0},
                p => PlayerSetupDone());
            this.navigator = navigator;
        }

        public override void Initialize() {}

        public override ValueTask InitializeAsync() => ValueTask.CompletedTask;

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
