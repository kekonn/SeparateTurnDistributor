using System;
using System.Windows;
using ChessClock.SyncEngine;
using ChessClock.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ChessClock.UI
{
    public class Navigator
    {
        public void ShowViewModel(IViewModel viewModel)
        {
            var app = Application.Current as App;
            app?.ShowViewModel(viewModel);
        }

        public void ShowGamesView()
        {
            var app = Application.Current as App;
            var services = app?.ServiceProvider ?? throw new InvalidOperationException("Could not locate service provider.");

            var viewModel = new GamesViewModel(services.GetRequiredService<ISyncEngine>());

            ShowViewModel(viewModel);
        }
    }
}
