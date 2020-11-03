namespace ChessClock.UI
{
    partial class ClockForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClockForm));
            this.userStatusStrip = new System.Windows.Forms.StatusStrip();
            this.usernameStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.playerSeedStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.refreshProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.gamesListBox = new System.Windows.Forms.ListBox();
            this.gameSyncControl = new ChessClock.UI.GameSyncControl();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.newGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameNameTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.hotSeatFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.userStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // userStatusStrip
            // 
            this.userStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usernameStatusLabel,
            this.playerSeedStatusLabel,
            this.refreshProgressBar});
            resources.ApplyResources(this.userStatusStrip, "userStatusStrip");
            this.userStatusStrip.Name = "userStatusStrip";
            // 
            // usernameStatusLabel
            // 
            this.usernameStatusLabel.Name = "usernameStatusLabel";
            resources.ApplyResources(this.usernameStatusLabel, "usernameStatusLabel");
            this.usernameStatusLabel.Tag = "Username: {0}";
            // 
            // playerSeedStatusLabel
            // 
            this.playerSeedStatusLabel.Name = "playerSeedStatusLabel";
            resources.ApplyResources(this.playerSeedStatusLabel, "playerSeedStatusLabel");
            this.playerSeedStatusLabel.Tag = "Player Seed: {0}";
            this.playerSeedStatusLabel.Click += new System.EventHandler(this.playerSeedStatusLabel_Click);
            // 
            // refreshProgressBar
            // 
            this.refreshProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.refreshProgressBar.Name = "refreshProgressBar";
            resources.ApplyResources(this.refreshProgressBar, "refreshProgressBar");
            this.refreshProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // mainSplitContainer
            // 
            resources.ApplyResources(this.mainSplitContainer, "mainSplitContainer");
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.gamesListBox);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.gameSyncControl);
            // 
            // gamesListBox
            // 
            this.gamesListBox.DisplayMember = "Name";
            resources.ApplyResources(this.gamesListBox, "gamesListBox");
            this.gamesListBox.FormattingEnabled = true;
            this.gamesListBox.Name = "gamesListBox";
            this.gamesListBox.ValueMember = "Id";
            this.gamesListBox.SelectedValueChanged += new System.EventHandler(this.gamesListBox_SelectedValueChanged);
            // 
            // gameSyncControl
            // 
            resources.ApplyResources(this.gameSyncControl, "gameSyncControl");
            this.gameSyncControl.Name = "gameSyncControl";
            this.gameSyncControl.ViewModel = null;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameMenuItem,
            this.gameNameTextBox});
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // newGameMenuItem
            // 
            this.newGameMenuItem.Name = "newGameMenuItem";
            resources.ApplyResources(this.newGameMenuItem, "newGameMenuItem");
            // 
            // gameNameTextBox
            // 
            this.gameNameTextBox.Name = "gameNameTextBox";
            resources.ApplyResources(this.gameNameTextBox, "gameNameTextBox");
            this.gameNameTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gameNameTextBox_KeyUp);
            // 
            // hotSeatFolderBrowser
            // 
            resources.ApplyResources(this.hotSeatFolderBrowser, "hotSeatFolderBrowser");
            this.hotSeatFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            this.hotSeatFolderBrowser.ShowNewFolderButton = false;
            // 
            // ClockForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.userStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "ClockForm";
            this.Load += new System.EventHandler(this.ClockForm_Load);
            this.userStatusStrip.ResumeLayout(false);
            this.userStatusStrip.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip userStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel usernameStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar refreshProgressBar;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ListBox gamesListBox;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newGameMenuItem;
        private System.Windows.Forms.ToolStripTextBox gameNameTextBox;
        private System.Windows.Forms.ToolStripStatusLabel playerSeedStatusLabel;
        private GameSyncControl gameSyncControl;
        private System.Windows.Forms.FolderBrowserDialog hotSeatFolderBrowser;
    }
}

