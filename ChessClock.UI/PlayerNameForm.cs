using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessClock.UI
{
    public partial class PlayerNameForm : Form
    {
        public string PlayerName { get; set; }

        public PlayerNameForm()
        {
            InitializeComponent();
        }

        private void SetPlayerName()
        {
            PlayerName = playerNameTextBox.Text;

            DialogResult = DialogResult.OK;
        }

        private void playerNameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            SetPlayerName();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SetPlayerName();
        }

        private void PlayerNameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(PlayerName))
            {
                e.Cancel = true;
                MessageBox.Show("You have to enter a username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
