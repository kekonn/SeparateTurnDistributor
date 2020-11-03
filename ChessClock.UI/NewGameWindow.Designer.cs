namespace ChessClock.UI
{
    partial class NewGameWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGameWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.playerCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newPlayerNameTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.playerSeedTextBox = new System.Windows.Forms.TextBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.addPlayerButton = new System.Windows.Forms.Button();
            this.createGameButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.14354F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.85646F));
            this.tableLayoutPanel1.Controls.Add(this.playerCheckedListBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.newPlayerNameTextbox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.playerSeedTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonPanel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(418, 472);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // playerCheckedListBox
            // 
            this.playerCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerCheckedListBox.FormattingEnabled = true;
            this.playerCheckedListBox.Location = new System.Drawing.Point(128, 61);
            this.playerCheckedListBox.Name = "playerCheckedListBox";
            this.playerCheckedListBox.Size = new System.Drawing.Size(287, 408);
            this.playerCheckedListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "New Player Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // newPlayerNameTextbox
            // 
            this.newPlayerNameTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newPlayerNameTextbox.Location = new System.Drawing.Point(128, 3);
            this.newPlayerNameTextbox.Name = "newPlayerNameTextbox";
            this.newPlayerNameTextbox.Size = new System.Drawing.Size(287, 23);
            this.newPlayerNameTextbox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player seed:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // playerSeedTextBox
            // 
            this.playerSeedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerSeedTextBox.Location = new System.Drawing.Point(128, 32);
            this.playerSeedTextBox.Name = "playerSeedTextBox";
            this.playerSeedTextBox.Size = new System.Drawing.Size(287, 23);
            this.playerSeedTextBox.TabIndex = 5;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.addPlayerButton);
            this.buttonPanel.Controls.Add(this.createGameButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(3, 61);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(119, 408);
            this.buttonPanel.TabIndex = 6;
            // 
            // addPlayerButton
            // 
            this.addPlayerButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.addPlayerButton.Location = new System.Drawing.Point(0, 0);
            this.addPlayerButton.Name = "addPlayerButton";
            this.addPlayerButton.Size = new System.Drawing.Size(119, 23);
            this.addPlayerButton.TabIndex = 1;
            this.addPlayerButton.Text = "Add Player";
            this.addPlayerButton.UseVisualStyleBackColor = true;
            this.addPlayerButton.Click += new System.EventHandler(this.addPlayerButton_Click);
            // 
            // createGameButton
            // 
            this.createGameButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.createGameButton.Location = new System.Drawing.Point(0, 385);
            this.createGameButton.Name = "createGameButton";
            this.createGameButton.Size = new System.Drawing.Size(119, 23);
            this.createGameButton.TabIndex = 0;
            this.createGameButton.Text = "Create Game";
            this.createGameButton.UseVisualStyleBackColor = true;
            this.createGameButton.Click += new System.EventHandler(this.createGameButton_Click);
            // 
            // NewGameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 472);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewGameWindow";
            this.Text = "New Game";
            this.Load += new System.EventHandler(this.NewGameWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox playerCheckedListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newPlayerNameTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox playerSeedTextBox;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button createGameButton;
        private System.Windows.Forms.Button addPlayerButton;
    }
}