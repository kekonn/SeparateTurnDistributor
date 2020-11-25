using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessClock.UI.ViewModels
{
    public class FirstSetupWizardViewModel : IViewModel
    {
        public string Title { get; set; } = "Player Setup";

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public async ValueTask InitializeAsync()
        {
            await Task.Run(Initialize);
        }

        public IEnumerable<ICommand?> Commands { get; }
        public ContentControl View { get; }
    }
}
