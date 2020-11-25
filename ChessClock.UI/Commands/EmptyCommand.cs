using System;
using System.Windows.Input;

namespace ChessClock.UI.Commands
{
    public class EmptyCommand : ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            return;
        }

        public event EventHandler? CanExecuteChanged;
    }
}
