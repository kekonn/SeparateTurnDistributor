
namespace ChessClock.UI
{
    partial class GameSyncControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.gameNameTextBox = new System.Windows.Forms.TextBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.checkUpdateButton = new System.Windows.Forms.Button();
            this.nextTurnButton = new System.Windows.Forms.Button();
            this.playersListBox = new System.Windows.Forms.ListBox();
            this.gameTabControl = new System.Windows.Forms.TabControl();
            this.syncTabPage = new System.Windows.Forms.TabPage();
            this.syncTabTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lastSyncTimeLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lastFileModifiedLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.currentPlayerLabel = new System.Windows.Forms.Label();
            this.saveButtonsPanel = new System.Windows.Forms.Panel();
            this.autoSyncToggle = new System.Windows.Forms.Button();
            this.selectSaveFileButton = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainLayoutPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.gameTabControl.SuspendLayout();
            this.syncTabPage.SuspendLayout();
            this.syncTabTableLayout.SuspendLayout();
            this.saveButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            this.mainLayoutPanel.ColumnCount = 2;
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.mainLayoutPanel.Controls.Add(this.gameNameTextBox, 1, 0);
            this.mainLayoutPanel.Controls.Add(this.buttonPanel, 0, 1);
            this.mainLayoutPanel.Controls.Add(this.gameTabControl, 1, 1);
            this.mainLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutPanel.Name = "mainLayoutPanel";
            this.mainLayoutPanel.RowCount = 2;
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.277405F));
            this.mainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.7226F));
            this.mainLayoutPanel.Size = new System.Drawing.Size(805, 447);
            this.mainLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gameNameTextBox
            // 
            this.gameNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameNameTextBox.Location = new System.Drawing.Point(209, 3);
            this.gameNameTextBox.Name = "gameNameTextBox";
            this.gameNameTextBox.Size = new System.Drawing.Size(593, 23);
            this.gameNameTextBox.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.checkUpdateButton);
            this.buttonPanel.Controls.Add(this.nextTurnButton);
            this.buttonPanel.Controls.Add(this.playersListBox);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(3, 39);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(200, 405);
            this.buttonPanel.TabIndex = 2;
            // 
            // checkUpdateButton
            // 
            this.checkUpdateButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkUpdateButton.Location = new System.Drawing.Point(0, 359);
            this.checkUpdateButton.Name = "checkUpdateButton";
            this.checkUpdateButton.Size = new System.Drawing.Size(200, 23);
            this.checkUpdateButton.TabIndex = 2;
            this.checkUpdateButton.Text = "Check for Update";
            this.checkUpdateButton.UseVisualStyleBackColor = true;
            this.checkUpdateButton.Click += new System.EventHandler(this.checkUpdateButton_Click);
            // 
            // nextTurnButton
            // 
            this.nextTurnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nextTurnButton.Location = new System.Drawing.Point(0, 382);
            this.nextTurnButton.Name = "nextTurnButton";
            this.nextTurnButton.Size = new System.Drawing.Size(200, 23);
            this.nextTurnButton.TabIndex = 1;
            this.nextTurnButton.Text = "Next Turn";
            this.nextTurnButton.UseVisualStyleBackColor = true;
            this.nextTurnButton.Click += new System.EventHandler(this.nextTurnButton_Click);
            // 
            // playersListBox
            // 
            this.playersListBox.DisplayMember = "Name";
            this.playersListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playersListBox.FormattingEnabled = true;
            this.playersListBox.ItemHeight = 15;
            this.playersListBox.Location = new System.Drawing.Point(0, 0);
            this.playersListBox.Name = "playersListBox";
            this.playersListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.playersListBox.Size = new System.Drawing.Size(200, 405);
            this.playersListBox.TabIndex = 0;
            this.playersListBox.ValueMember = "Id";
            // 
            // gameTabControl
            // 
            this.gameTabControl.Controls.Add(this.syncTabPage);
            this.gameTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameTabControl.Location = new System.Drawing.Point(209, 39);
            this.gameTabControl.Name = "gameTabControl";
            this.gameTabControl.SelectedIndex = 0;
            this.gameTabControl.Size = new System.Drawing.Size(593, 405);
            this.gameTabControl.TabIndex = 3;
            // 
            // syncTabPage
            // 
            this.syncTabPage.Controls.Add(this.syncTabTableLayout);
            this.syncTabPage.Location = new System.Drawing.Point(4, 24);
            this.syncTabPage.Name = "syncTabPage";
            this.syncTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.syncTabPage.Size = new System.Drawing.Size(585, 377);
            this.syncTabPage.TabIndex = 0;
            this.syncTabPage.Text = "Sync";
            this.syncTabPage.UseVisualStyleBackColor = true;
            // 
            // syncTabTableLayout
            // 
            this.syncTabTableLayout.ColumnCount = 2;
            this.syncTabTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.58031F));
            this.syncTabTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.41969F));
            this.syncTabTableLayout.Controls.Add(this.label2, 0, 0);
            this.syncTabTableLayout.Controls.Add(this.lastSyncTimeLabel, 1, 0);
            this.syncTabTableLayout.Controls.Add(this.label3, 0, 1);
            this.syncTabTableLayout.Controls.Add(this.lastFileModifiedLabel, 1, 1);
            this.syncTabTableLayout.Controls.Add(this.label4, 0, 2);
            this.syncTabTableLayout.Controls.Add(this.currentPlayerLabel, 1, 2);
            this.syncTabTableLayout.Controls.Add(this.saveButtonsPanel, 0, 3);
            this.syncTabTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syncTabTableLayout.Location = new System.Drawing.Point(3, 3);
            this.syncTabTableLayout.Name = "syncTabTableLayout";
            this.syncTabTableLayout.RowCount = 4;
            this.syncTabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.syncTabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.syncTabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.syncTabTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.syncTabTableLayout.Size = new System.Drawing.Size(579, 371);
            this.syncTabTableLayout.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Last Sync";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lastSyncTimeLabel
            // 
            this.lastSyncTimeLabel.AutoSize = true;
            this.lastSyncTimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastSyncTimeLabel.Location = new System.Drawing.Point(99, 0);
            this.lastSyncTimeLabel.Name = "lastSyncTimeLabel";
            this.lastSyncTimeLabel.Size = new System.Drawing.Size(477, 15);
            this.lastSyncTimeLabel.TabIndex = 1;
            this.lastSyncTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Save file time modified";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lastFileModifiedLabel
            // 
            this.lastFileModifiedLabel.AutoSize = true;
            this.lastFileModifiedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastFileModifiedLabel.Location = new System.Drawing.Point(99, 15);
            this.lastFileModifiedLabel.Name = "lastFileModifiedLabel";
            this.lastFileModifiedLabel.Size = new System.Drawing.Size(477, 30);
            this.lastFileModifiedLabel.TabIndex = 3;
            this.lastFileModifiedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Current Player";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // currentPlayerLabel
            // 
            this.currentPlayerLabel.AutoSize = true;
            this.currentPlayerLabel.Location = new System.Drawing.Point(99, 45);
            this.currentPlayerLabel.Name = "currentPlayerLabel";
            this.currentPlayerLabel.Size = new System.Drawing.Size(0, 15);
            this.currentPlayerLabel.TabIndex = 5;
            this.currentPlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveButtonsPanel
            // 
            this.saveButtonsPanel.Controls.Add(this.autoSyncToggle);
            this.saveButtonsPanel.Controls.Add(this.selectSaveFileButton);
            this.saveButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButtonsPanel.Location = new System.Drawing.Point(3, 68);
            this.saveButtonsPanel.Name = "saveButtonsPanel";
            this.saveButtonsPanel.Size = new System.Drawing.Size(90, 300);
            this.saveButtonsPanel.TabIndex = 7;
            // 
            // autoSyncToggle
            // 
            this.autoSyncToggle.Dock = System.Windows.Forms.DockStyle.Top;
            this.autoSyncToggle.Location = new System.Drawing.Point(0, 23);
            this.autoSyncToggle.Name = "autoSyncToggle";
            this.autoSyncToggle.Size = new System.Drawing.Size(90, 43);
            this.autoSyncToggle.TabIndex = 7;
            this.autoSyncToggle.Tag = "{0} AutoSync";
            this.autoSyncToggle.Text = "Enable/Disable";
            this.autoSyncToggle.UseVisualStyleBackColor = true;
            this.autoSyncToggle.Click += new System.EventHandler(this.autoSyncToggle_Click);
            // 
            // selectSaveFileButton
            // 
            this.selectSaveFileButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectSaveFileButton.Location = new System.Drawing.Point(0, 0);
            this.selectSaveFileButton.Name = "selectSaveFileButton";
            this.selectSaveFileButton.Size = new System.Drawing.Size(90, 23);
            this.selectSaveFileButton.TabIndex = 6;
            this.selectSaveFileButton.Text = "Select Save File";
            this.selectSaveFileButton.UseVisualStyleBackColor = true;
            this.selectSaveFileButton.Click += new System.EventHandler(this.selectSaveFileButton_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 60000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "8.Civ6Save";
            this.saveFileDialog.Title = "Select save file";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
            // 
            // GameSyncControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainLayoutPanel);
            this.Name = "GameSyncControl";
            this.Size = new System.Drawing.Size(805, 447);
            this.mainLayoutPanel.ResumeLayout(false);
            this.mainLayoutPanel.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.gameTabControl.ResumeLayout(false);
            this.syncTabPage.ResumeLayout(false);
            this.syncTabTableLayout.ResumeLayout(false);
            this.syncTabTableLayout.PerformLayout();
            this.saveButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayoutPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gameNameTextBox;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.TabControl gameTabControl;
        private System.Windows.Forms.TabPage syncTabPage;
        private System.Windows.Forms.TableLayoutPanel syncTabTableLayout;
        private System.Windows.Forms.Button nextTurnButton;
        private System.Windows.Forms.ListBox playersListBox;
        private System.Windows.Forms.Button checkUpdateButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lastSyncTimeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lastFileModifiedLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label currentPlayerLabel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.OpenFileDialog saveFileDialog;
        private System.Windows.Forms.Button selectSaveFileButton;
        private System.Windows.Forms.Panel saveButtonsPanel;
        private System.Windows.Forms.Button autoSyncToggle;
    }
}
