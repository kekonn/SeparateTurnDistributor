using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChessClock.UI.ViewModels
{
    public interface IViewModel
    {
        void Initialize();
        ValueTask InitializeAsync();
        ICommand? NextCommand();
    }
}
