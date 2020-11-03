using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ChessClock.UI
{
    public partial class GameSyncControl : UserControl
    {
        private GameViewModel viewModel;
        public GameViewModel ViewModel
        {
            get
            {
                return viewModel;
            }
            set
            {
                viewModel = value;
                if (viewModel != null)
                {
                    WireUpViewModel();
                }
            }
        }

        public GameSyncControl()
        {
            InitializeComponent();
            UpdateToggleText();
        }

        public GameSyncControl(GameViewModel viewModel) : this()
        {
            this.viewModel = viewModel;
        }

        private void nextTurnButton_Click(object sender, EventArgs e)
        {
            viewModel.NextTurn();
        }

        private void WireUpViewModel()
        {
            playersListBox.DataSource = viewModel.Players;

            lastSyncTimeLabel.DataBindings.Clear();
            lastSyncTimeLabel.DataBindings.Add(new Binding("Text", viewModel, "LastUpdateCheck"));


            lastFileModifiedLabel.DataBindings.Clear();
            lastFileModifiedLabel.DataBindings.Add(new Binding("Text", viewModel, "SaveGameTime"));

            currentPlayerLabel.DataBindings.Clear();
            currentPlayerLabel.DataBindings.Add(new Binding("Text", viewModel, "CurrentPlayer.Name"));

            gameNameTextBox.DataBindings.Clear();
            gameNameTextBox.DataBindings.Add(new Binding("Text", viewModel, "Name"));

            nextTurnButton.DataBindings.Clear();
            nextTurnButton.DataBindings.Add(new Binding("Enabled", viewModel, "IsMyTurn"));
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ViewModel.SaveFileLocation = saveFileDialog.FileName;
        }

        private void selectSaveFileButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            viewModel.Update();
        }

        private void checkUpdateButton_Click(object sender, EventArgs e)
        {
            var lastUpdate = viewModel.LastUpdateCheck;
            if ((DateTimeOffset.Now - lastUpdate).TotalSeconds <= 60)
            {
                return;
            }

            viewModel.Update();
        }

        private void autoSyncToggle_Click(object sender, EventArgs e)
        {
            var timerEnabled = !updateTimer.Enabled;
            updateTimer.Enabled = timerEnabled;

            UpdateToggleText();
        }

        private void UpdateToggleText()
        {
            autoSyncToggle.Text = string.Format(autoSyncToggle.Tag.ToString(), updateTimer.Enabled ? "Disable" : "Enable");
        }
    }
}
