namespace ChatClient
{
    partial class clientGUI
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
            this.ServerBox = new System.Windows.Forms.TextBox();
            this.ServerLabel = new System.Windows.Forms.Label();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.TypeBox = new System.Windows.Forms.TextBox();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ClientNameBox = new System.Windows.Forms.TextBox();
            this.ParticipantLabel = new System.Windows.Forms.Label();
            this.InstructionBox = new System.Windows.Forms.RichTextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.BoxOfMessages = new System.Windows.Forms.TextBox();
            this.ParticipantBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ServerBox
            // 
            this.ServerBox.Location = new System.Drawing.Point(149, 45);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(275, 27);
            this.ServerBox.TabIndex = 0;
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(40, 48);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(50, 20);
            this.ServerLabel.TabIndex = 1;
            this.ServerLabel.Text = "Server";
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(40, 182);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(67, 20);
            this.MessageLabel.TabIndex = 2;
            this.MessageLabel.Text = "Message";
            // 
            // TypeBox
            // 
            this.TypeBox.Location = new System.Drawing.Point(149, 495);
            this.TypeBox.Name = "TypeBox";
            this.TypeBox.Size = new System.Drawing.Size(389, 27);
            this.TypeBox.TabIndex = 5;
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Location = new System.Drawing.Point(40, 498);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(76, 20);
            this.TypeLabel.TabIndex = 6;
            this.TypeLabel.Text = "Type Here";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(40, 108);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(78, 20);
            this.NameLabel.TabIndex = 7;
            this.NameLabel.Text = "YourName";
            // 
            // ClientNameBox
            // 
            this.ClientNameBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientNameBox.Location = new System.Drawing.Point(149, 105);
            this.ClientNameBox.Name = "ClientNameBox";
            this.ClientNameBox.Size = new System.Drawing.Size(389, 27);
            this.ClientNameBox.TabIndex = 8;
            // 
            // ParticipantLabel
            // 
            this.ParticipantLabel.AutoSize = true;
            this.ParticipantLabel.Location = new System.Drawing.Point(570, 48);
            this.ParticipantLabel.Name = "ParticipantLabel";
            this.ParticipantLabel.Size = new System.Drawing.Size(85, 20);
            this.ParticipantLabel.TabIndex = 9;
            this.ParticipantLabel.Text = "Participants";
            // 
            // InstructionBox
            // 
            this.InstructionBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.InstructionBox.Location = new System.Drawing.Point(570, 378);
            this.InstructionBox.Name = "InstructionBox";
            this.InstructionBox.ReadOnly = true;
            this.InstructionBox.Size = new System.Drawing.Size(334, 188);
            this.InstructionBox.TabIndex = 11;
            this.InstructionBox.Text = "-Tpying \"Command Name [name]\" to change your name \n-Typing \"Command Participants\"" +
    " to show the participants\n";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(444, 43);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(94, 29);
            this.ConnectButton.TabIndex = 12;
            this.ConnectButton.Text = "connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(444, 537);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(94, 29);
            this.SendButton.TabIndex = 13;
            this.SendButton.Text = "send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // BoxOfMessages
            // 
            this.BoxOfMessages.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BoxOfMessages.Location = new System.Drawing.Point(149, 179);
            this.BoxOfMessages.Multiline = true;
            this.BoxOfMessages.Name = "BoxOfMessages";
            this.BoxOfMessages.ReadOnly = true;
            this.BoxOfMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.BoxOfMessages.Size = new System.Drawing.Size(389, 273);
            this.BoxOfMessages.TabIndex = 14;
            // 
            // ParticipantBox
            // 
            this.ParticipantBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ParticipantBox.Location = new System.Drawing.Point(570, 88);
            this.ParticipantBox.Multiline = true;
            this.ParticipantBox.Name = "ParticipantBox";
            this.ParticipantBox.ReadOnly = true;
            this.ParticipantBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ParticipantBox.Size = new System.Drawing.Size(334, 257);
            this.ParticipantBox.TabIndex = 15;
            // 
            // clientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 587);
            this.Controls.Add(this.ParticipantBox);
            this.Controls.Add(this.BoxOfMessages);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.InstructionBox);
            this.Controls.Add(this.ParticipantLabel);
            this.Controls.Add(this.ClientNameBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.TypeBox);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.ServerLabel);
            this.Controls.Add(this.ServerBox);
            this.Name = "clientGUI";
            this.Text = "ChatClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.clientGUI_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox ServerBox;
        private Label ServerLabel;
        private Label MessageLabel;
        private TextBox TypeBox;
        private Label TypeLabel;
        private Label NameLabel;
        private TextBox ClientNameBox;
        private Label ParticipantLabel;
        private RichTextBox InstructionBox;
        private Button ConnectButton;
        private Button SendButton;
        private TextBox BoxOfMessages;
        private TextBox ParticipantBox;
    }
}