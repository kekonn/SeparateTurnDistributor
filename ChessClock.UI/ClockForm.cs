using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessClock.Data;
using ChessClock.Model;
using ChessClock.UI.Properties;
using NLog;

namespace ChessClock.UI
{
    public partial class ClockForm : Form
    {
        public IGameRepository GameRepository { get; set; }
        public Player SystemPlayer { get; set; } = Player.One;
        public ILogger Logger { get; set; } = LogManager.GetCurrentClassLogger();
        
        private NewGameWindow newGameWindow;

        public ClockForm()
        {
            InitializeComponent();
            newGameMenuItem.Click += NewGameMenuItem_Click;
        }

        private void NewGameMenuItem_Click(object sender, EventArgs e)
        {
            ShowNewGameWindow();
        }

        private void ShowNewGameWindow()
        {
            var gameName = gameNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(gameName))
            {
                MessageBox.Show("This is not a valid game name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            newGameWindow = BuildGameWindow(gameName);
            newGameWindow.FormClosed += NewGameWindow_FormClosed;
            newGameWindow.ShowDialog(this);
        }

        private async void NewGameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (newGameWindow.DialogResult != DialogResult.OK)
                return;

            var players = newGameWindow.SelectedPlayers;
            var gameName = newGameWindow.GameName;

            await CreateGame(gameName, players);
        }

        private async Task CreateGame(string gameName, List<Player> players)
        {
            var game = new Game(gameName, players);

            Logger.Debug($"Added game {game}");

            GameRepository.Add(game);

            await UpdateGamesList();
        }

        private NewGameWindow BuildGameWindow(string newGameName)
        {
            return new NewGameWindow()
            {
                GameName = newGameName,
                GameRepository = GameRepository
            };
        }

        private async void ClockForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SystemPlayer.Name))
            {
                using var playerNameDialog = new PlayerNameForm();

                playerNameDialog.ShowDialog(this);

                SystemPlayer.Name = playerNameDialog.PlayerName;

                PlayerUtilities.SaveSystemPlayer(SystemPlayer);
            }

            if (string.IsNullOrEmpty(Settings.Default.HotSeatFolder))
            {
                var result = hotSeatFolderBrowser.ShowDialog();
                while (result != DialogResult.OK)
                {
                    MessageBox.Show("You have to select a save file folder in order to proceed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = hotSeatFolderBrowser.ShowDialog();
                }

                Settings.Default.HotSeatFolder = hotSeatFolderBrowser.SelectedPath;
                Settings.Default.Save();
            }

            usernameStatusLabel.Text = string.Format(usernameStatusLabel.Tag.ToString(), SystemPlayer.Name);
            playerSeedStatusLabel.Text = string.Format(playerSeedStatusLabel.Tag.ToString(), PlayerUtilities.GetSystemPlayerSeed());

            await UpdateGamesList();
        }

        private async Task UpdateGamesList()
        {
            refreshProgressBar.Visible = true;

            var games = await Task.Run(() => GameRepository.AllForPlayer(SystemPlayer).ToList());

            if (games.Count > 0)
            {
                var gameNames = games.Select(g => g.Name).Aggregate((workingSentence, next) => workingSentence += ", " + next);
                Logger.Debug($"Found the following games for player {SystemPlayer}: {gameNames}");
            }

            gamesListBox.DataSource = games;

            refreshProgressBar.Visible = false;
        }

        private void gameNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.Handled = true;
            ShowNewGameWindow();
        }

        private void playerSeedStatusLabel_Click(object sender, EventArgs e)
        {
            var playerSeed = PlayerUtilities.GetSystemPlayerSeed();
            Clipboard.SetText(playerSeed);
        }

        private void gamesListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var viewModel = new GameViewModel(gamesListBox.SelectedItem as Game, GameRepository);
            gameSyncControl.ViewModel = viewModel;
        }
    }
}
