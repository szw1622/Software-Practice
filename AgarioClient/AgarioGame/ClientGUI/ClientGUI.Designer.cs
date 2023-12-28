namespace ClientGUI
{
    /// <summary> 
    /// Author:    Jiawen Wang
    /// Partner:   Zhuowen Song
    /// Date:      4/14/2022
    /// Course:    CS 3500, University of Utah, School of Computing 
    /// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
    /// 
    /// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
    /// another source.  All references used in the completion of the assignment are cited in my README file. 
    /// 
    /// File Contents 
    /// This is the designer of the clientGUI.
    /// </summary>
    partial class ClientGUI
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
            this.PlayerNameLabel = new System.Windows.Forms.Label();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.PlayerNameBox = new System.Windows.Forms.TextBox();
            this.ErrorBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ServerBox = new System.Windows.Forms.TextBox();
            this.PositionTag = new System.Windows.Forms.Label();
            this.PositionBox = new System.Windows.Forms.TextBox();
            this.FoodTag = new System.Windows.Forms.Label();
            this.FoodBox = new System.Windows.Forms.TextBox();
            this.MassTag = new System.Windows.Forms.Label();
            this.MassBox = new System.Windows.Forms.TextBox();
            this.PlayerTag = new System.Windows.Forms.Label();
            this.HeartBeatTag = new System.Windows.Forms.Label();
            this.HeartBeatBox = new System.Windows.Forms.TextBox();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.ReplayBox = new System.Windows.Forms.TextBox();
            this.ReplayButton = new System.Windows.Forms.Button();
            this.GameOverLabel = new System.Windows.Forms.Label();
            this.ReplayNameLabel = new System.Windows.Forms.Label();
            this.PlayersBox = new System.Windows.Forms.TextBox();
            this.MouseLabel = new System.Windows.Forms.Label();
            this.MouseBox = new System.Windows.Forms.TextBox();
            this.DirectionBox = new System.Windows.Forms.TextBox();
            this.DirectionLabel = new System.Windows.Forms.Label();
            this.DeathLabel = new System.Windows.Forms.Label();
            this.DieBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PlayerNameLabel
            // 
            this.PlayerNameLabel.AutoSize = true;
            this.PlayerNameLabel.Location = new System.Drawing.Point(122, 284);
            this.PlayerNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlayerNameLabel.Name = "PlayerNameLabel";
            this.PlayerNameLabel.Size = new System.Drawing.Size(93, 20);
            this.PlayerNameLabel.TabIndex = 0;
            this.PlayerNameLabel.Text = "Player Name";
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(164, 338);
            this.ServerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(50, 20);
            this.ServerLabel.TabIndex = 1;
            this.ServerLabel.Text = "Server";
            // 
            // PlayerNameBox
            // 
            this.PlayerNameBox.Location = new System.Drawing.Point(229, 284);
            this.PlayerNameBox.Margin = new System.Windows.Forms.Padding(2);
            this.PlayerNameBox.Name = "PlayerNameBox";
            this.PlayerNameBox.Size = new System.Drawing.Size(236, 27);
            this.PlayerNameBox.TabIndex = 2;
            // 
            // ErrorBox
            // 
            this.ErrorBox.Enabled = false;
            this.ErrorBox.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ErrorBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ErrorBox.Location = new System.Drawing.Point(50, 1051);
            this.ErrorBox.Margin = new System.Windows.Forms.Padding(2);
            this.ErrorBox.Name = "ErrorBox";
            this.ErrorBox.ReadOnly = true;
            this.ErrorBox.Size = new System.Drawing.Size(1103, 43);
            this.ErrorBox.TabIndex = 5;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(481, 338);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(2);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(94, 29);
            this.ConnectButton.TabIndex = 6;
            this.ConnectButton.Text = "Play";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ServerBox
            // 
            this.ServerBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ServerBox.Location = new System.Drawing.Point(229, 340);
            this.ServerBox.Margin = new System.Windows.Forms.Padding(2);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(236, 27);
            this.ServerBox.TabIndex = 7;
            // 
            // PositionTag
            // 
            this.PositionTag.AutoSize = true;
            this.PositionTag.Location = new System.Drawing.Point(1291, 94);
            this.PositionTag.Name = "PositionTag";
            this.PositionTag.Size = new System.Drawing.Size(105, 20);
            this.PositionTag.TabIndex = 8;
            this.PositionTag.Text = "Player Position";
            // 
            // PositionBox
            // 
            this.PositionBox.Enabled = false;
            this.PositionBox.Location = new System.Drawing.Point(1417, 94);
            this.PositionBox.Name = "PositionBox";
            this.PositionBox.Size = new System.Drawing.Size(225, 27);
            this.PositionBox.TabIndex = 9;
            // 
            // FoodTag
            // 
            this.FoodTag.AutoSize = true;
            this.FoodTag.Location = new System.Drawing.Point(1354, 188);
            this.FoodTag.Name = "FoodTag";
            this.FoodTag.Size = new System.Drawing.Size(43, 20);
            this.FoodTag.TabIndex = 10;
            this.FoodTag.Text = "Food";
            // 
            // FoodBox
            // 
            this.FoodBox.Enabled = false;
            this.FoodBox.Location = new System.Drawing.Point(1417, 188);
            this.FoodBox.Name = "FoodBox";
            this.FoodBox.Size = new System.Drawing.Size(225, 27);
            this.FoodBox.TabIndex = 11;
            // 
            // MassTag
            // 
            this.MassTag.AutoSize = true;
            this.MassTag.Location = new System.Drawing.Point(1354, 142);
            this.MassTag.Name = "MassTag";
            this.MassTag.Size = new System.Drawing.Size(42, 20);
            this.MassTag.TabIndex = 12;
            this.MassTag.Text = "Mass";
            // 
            // MassBox
            // 
            this.MassBox.Enabled = false;
            this.MassBox.Location = new System.Drawing.Point(1417, 144);
            this.MassBox.Name = "MassBox";
            this.MassBox.Size = new System.Drawing.Size(225, 27);
            this.MassBox.TabIndex = 13;
            // 
            // PlayerTag
            // 
            this.PlayerTag.AutoSize = true;
            this.PlayerTag.Location = new System.Drawing.Point(1341, 236);
            this.PlayerTag.Name = "PlayerTag";
            this.PlayerTag.Size = new System.Drawing.Size(55, 20);
            this.PlayerTag.TabIndex = 14;
            this.PlayerTag.Text = "Players";
            // 
            // HeartBeatTag
            // 
            this.HeartBeatTag.AutoSize = true;
            this.HeartBeatTag.Location = new System.Drawing.Point(1311, 281);
            this.HeartBeatTag.Name = "HeartBeatTag";
            this.HeartBeatTag.Size = new System.Drawing.Size(86, 20);
            this.HeartBeatTag.TabIndex = 16;
            this.HeartBeatTag.Text = "Heart Beats";
            // 
            // HeartBeatBox
            // 
            this.HeartBeatBox.Enabled = false;
            this.HeartBeatBox.Location = new System.Drawing.Point(1417, 281);
            this.HeartBeatBox.Name = "HeartBeatBox";
            this.HeartBeatBox.Size = new System.Drawing.Size(225, 27);
            this.HeartBeatBox.TabIndex = 17;
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WelcomeLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.WelcomeLabel.Location = new System.Drawing.Point(204, 96);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(281, 75);
            this.WelcomeLabel.TabIndex = 18;
            this.WelcomeLabel.Text = "Welcome!";
            // 
            // ReplayBox
            // 
            this.ReplayBox.Location = new System.Drawing.Point(680, 360);
            this.ReplayBox.Margin = new System.Windows.Forms.Padding(2);
            this.ReplayBox.Name = "ReplayBox";
            this.ReplayBox.Size = new System.Drawing.Size(225, 27);
            this.ReplayBox.TabIndex = 19;
            this.ReplayBox.Visible = false;
            // 
            // ReplayButton
            // 
            this.ReplayButton.Location = new System.Drawing.Point(743, 414);
            this.ReplayButton.Margin = new System.Windows.Forms.Padding(2);
            this.ReplayButton.Name = "ReplayButton";
            this.ReplayButton.Size = new System.Drawing.Size(94, 29);
            this.ReplayButton.TabIndex = 20;
            this.ReplayButton.Text = "Try Again";
            this.ReplayButton.UseVisualStyleBackColor = true;
            this.ReplayButton.Visible = false;
            this.ReplayButton.Click += new System.EventHandler(this.ReplayButton_Click_1);
            // 
            // GameOverLabel
            // 
            this.GameOverLabel.AutoSize = true;
            this.GameOverLabel.Font = new System.Drawing.Font("Impact", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameOverLabel.ForeColor = System.Drawing.Color.Red;
            this.GameOverLabel.Location = new System.Drawing.Point(646, 265);
            this.GameOverLabel.Name = "GameOverLabel";
            this.GameOverLabel.Size = new System.Drawing.Size(309, 75);
            this.GameOverLabel.TabIndex = 21;
            this.GameOverLabel.Text = "Game Over!";
            this.GameOverLabel.Visible = false;
            // 
            // ReplayNameLabel
            // 
            this.ReplayNameLabel.AutoSize = true;
            this.ReplayNameLabel.Location = new System.Drawing.Point(565, 360);
            this.ReplayNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ReplayNameLabel.Name = "ReplayNameLabel";
            this.ReplayNameLabel.Size = new System.Drawing.Size(93, 20);
            this.ReplayNameLabel.TabIndex = 26;
            this.ReplayNameLabel.Text = "Player Name";
            this.ReplayNameLabel.Visible = false;
            // 
            // PlayersBox
            // 
            this.PlayersBox.Enabled = false;
            this.PlayersBox.Location = new System.Drawing.Point(1417, 234);
            this.PlayersBox.Name = "PlayersBox";
            this.PlayersBox.Size = new System.Drawing.Size(225, 27);
            this.PlayersBox.TabIndex = 15;
            // 
            // MouseLabel
            // 
            this.MouseLabel.AutoSize = true;
            this.MouseLabel.Location = new System.Drawing.Point(1287, 331);
            this.MouseLabel.Name = "MouseLabel";
            this.MouseLabel.Size = new System.Drawing.Size(109, 20);
            this.MouseLabel.TabIndex = 27;
            this.MouseLabel.Text = "Mouse Position";
            // 
            // MouseBox
            // 
            this.MouseBox.Enabled = false;
            this.MouseBox.Location = new System.Drawing.Point(1417, 328);
            this.MouseBox.Name = "MouseBox";
            this.MouseBox.Size = new System.Drawing.Size(225, 27);
            this.MouseBox.TabIndex = 28;
            // 
            // DirectionBox
            // 
            this.DirectionBox.Enabled = false;
            this.DirectionBox.Location = new System.Drawing.Point(1417, 375);
            this.DirectionBox.Name = "DirectionBox";
            this.DirectionBox.Size = new System.Drawing.Size(225, 27);
            this.DirectionBox.TabIndex = 29;
            // 
            // DirectionLabel
            // 
            this.DirectionLabel.AutoSize = true;
            this.DirectionLabel.Location = new System.Drawing.Point(1327, 378);
            this.DirectionLabel.Name = "DirectionLabel";
            this.DirectionLabel.Size = new System.Drawing.Size(70, 20);
            this.DirectionLabel.TabIndex = 30;
            this.DirectionLabel.Text = "Direction";
            // 
            // DeathLabel
            // 
            this.DeathLabel.AutoSize = true;
            this.DeathLabel.Location = new System.Drawing.Point(1321, 423);
            this.DeathLabel.Name = "DeathLabel";
            this.DeathLabel.Size = new System.Drawing.Size(75, 20);
            this.DeathLabel.TabIndex = 31;
            this.DeathLabel.Text = "Die Times";
            // 
            // DieBox
            // 
            this.DieBox.Enabled = false;
            this.DieBox.Location = new System.Drawing.Point(1417, 420);
            this.DieBox.Name = "DieBox";
            this.DieBox.Size = new System.Drawing.Size(225, 27);
            this.DieBox.TabIndex = 32;
            // 
            // ClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1682, 1153);
            this.Controls.Add(this.DieBox);
            this.Controls.Add(this.DeathLabel);
            this.Controls.Add(this.DirectionLabel);
            this.Controls.Add(this.DirectionBox);
            this.Controls.Add(this.MouseBox);
            this.Controls.Add(this.MouseLabel);
            this.Controls.Add(this.ReplayNameLabel);
            this.Controls.Add(this.GameOverLabel);
            this.Controls.Add(this.ReplayButton);
            this.Controls.Add(this.ReplayBox);
            this.Controls.Add(this.WelcomeLabel);
            this.Controls.Add(this.HeartBeatBox);
            this.Controls.Add(this.HeartBeatTag);
            this.Controls.Add(this.PlayersBox);
            this.Controls.Add(this.PlayerTag);
            this.Controls.Add(this.MassBox);
            this.Controls.Add(this.MassTag);
            this.Controls.Add(this.FoodBox);
            this.Controls.Add(this.FoodTag);
            this.Controls.Add(this.PositionBox);
            this.Controls.Add(this.PositionTag);
            this.Controls.Add(this.ServerBox);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ErrorBox);
            this.Controls.Add(this.PlayerNameBox);
            this.Controls.Add(this.ServerLabel);
            this.Controls.Add(this.PlayerNameLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ClientGUI";
            this.Text = "Game Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label PlayerNameLabel;
        private Label ServerLabel;
        private TextBox PlayerNameBox;
        private TextBox ErrorBox;
        private Button ConnectButton;
        private TextBox ServerBox;
        private Label PositionTag;
        private TextBox PositionBox;
        private Label FoodTag;
        private TextBox FoodBox;
        private Label MassTag;
        private TextBox MassBox;
        private Label PlayerTag;
        private Label HeartBeatTag;
        private TextBox HeartBeatBox;
        private Label WelcomeLabel;
        private TextBox ReplayBox;
        private Button ReplayButton;
        private Label GameOverLabel;
        private Label ReplayNameLabel;
        private TextBox PlayersBox;
        private Label MouseLabel;
        private TextBox MouseBox;
        private TextBox DirectionBox;
        private Label DirectionLabel;
        private Label DeathLabel;
        private TextBox DieBox;
    }
}