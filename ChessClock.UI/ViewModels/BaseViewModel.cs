using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessClock.UI.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        private ContentControl view;
        private string title;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ContentControl View
        {
            get => view;
            set
            {
                if (view == value) return;

                view = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (title == value) return;

                title = value;
                OnPropertyChanged();
            }
        }

        protected BaseViewModel()
        {
            view = new ContentControl();
            title = string.Empty;
        }

        public abstract void Initialize();
        public abstract ValueTask InitializeAsync();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
