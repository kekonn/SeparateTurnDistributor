using ChessClock.Data;
using ChessClock.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ChessClock.UI
{
    public partial class NewGameWindow : Form
    {
        private const string BaseWindowTitle = "New Game: ";
        public IGameRepository GameRepository { get; set; }

        public string GameName { get; set; }

        public List<Player> SelectedPlayers => playerCheckedListBox.CheckedItems.ToPlayerList();

        public NewGameWindow()
        {
            InitializeComponent();
        }

        private void LoadPlayers()
        {
            var players = GameRepository.AllPlayers().ToList();
            var systemPlayer = PlayerUtilities.LoadSystemPlayer();

            if (!players.Contains(systemPlayer))
            {
                players.Add(systemPlayer);
            }

            var playerListBox = playerCheckedListBox as ListBox;

            var obsPlayers = new BindingList<Player>(players);

            playerListBox.DataSource = obsPlayers;
            playerListBox.DisplayMember = "Name";
            playerListBox.ValueMember = "Id";
        }

        private void SetWindowTitle()
        {
            Text = BaseWindowTitle + GameName;
        }

        private void NewGameWindow_Load(object sender, EventArgs e)
        {
            SetWindowTitle();
            LoadPlayers();
        }

        private void createGameButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void addPlayerButton_Click(object sender, EventArgs e)
        {
            var playerName = newPlayerNameTextbox.Text.Trim();
            var playerSeed = playerSeedTextBox.Text.Trim().ToLower();

            if (!PlayerInputsAreValid(playerName, playerSeed))
            {
                MessageBox.Show("Please provide proper new player inputs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var playerListbox = playerCheckedListBox as ListBox;
            var players = playerListbox.DataSource as BindingList<Player>;

            players.Add(PlayerUtilities.FromSeed(playerName, playerSeed));
        }

        private bool PlayerInputsAreValid(string playerName, string playerSeed)
        {
            var valid = !string.IsNullOrEmpty(playerName) && !string.IsNullOrEmpty(playerSeed);

            if (!valid)
                return valid;

            var id = new Guid(Convert.FromBase64String(playerSeed));

            return valid && id != Guid.Empty;
        }
    }
}
