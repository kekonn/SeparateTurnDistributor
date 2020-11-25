using System;
using System.Windows.Input;

namespace ChessClock.UI
{
    public class ActionCommand : ICommand
    {
        private Func<object?, bool> canExecute;
        private Action<object?> action;

        public ActionCommand(Func<object?, bool> canExecute, Action<object?> action)
        {
            this.canExecute = canExecute;
            this.action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            action(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
