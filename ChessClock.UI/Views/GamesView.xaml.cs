using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessClock.SyncEngine;
using ChessClock.UI.ViewModels;

namespace ChessClock.UI.Views
{
    /// <summary>
    /// Interaction logic for ContentHostWindow.xaml
    /// </summary>
    public partial class GamesView : ContentControl
    {

        public GamesView()
        {
            InitializeComponent();
        }

        public GamesView(GamesViewModel viewModel) : this()
        {
            DataContext = viewModel;
            viewModel.Initialize();
        }
    }
}
