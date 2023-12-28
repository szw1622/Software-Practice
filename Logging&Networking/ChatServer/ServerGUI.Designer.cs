namespace ChatServer
{
    partial class ServerGUI
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
            this.ServerLabel = new System.Windows.Forms.Label();
            this.ServerBox = new System.Windows.Forms.TextBox();
            this.BoxOfMessages = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.ShutdownServerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ParticipantsBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ServerLabel
            // 
            this.ServerLabel.AutoSize = true;
            this.ServerLabel.Location = new System.Drawing.Point(46, 53);
            this.ServerLabel.Name = "ServerLabel";
            this.ServerLabel.Size = new System.Drawing.Size(66, 20);
            this.ServerLabel.TabIndex = 0;
            this.ServerLabel.Text = "Server IP";
            // 
            // ServerBox
            // 
            this.ServerBox.Enabled = false;
            this.ServerBox.Location = new System.Drawing.Point(119, 50);
            this.ServerBox.Name = "ServerBox";
            this.ServerBox.Size = new System.Drawing.Size(373, 27);
            this.ServerBox.TabIndex = 1;
            // 
            // BoxOfMessages
            // 
            this.BoxOfMessages.Enabled = false;
            this.BoxOfMessages.Location = new System.Drawing.Point(119, 114);
            this.BoxOfMessages.Multiline = true;
            this.BoxOfMessages.Name = "BoxOfMessages";
            this.BoxOfMessages.Size = new System.Drawing.Size(373, 398);
            this.BoxOfMessages.TabIndex = 2;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(46, 117);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(67, 20);
            this.MessageLabel.TabIndex = 3;
            this.MessageLabel.Text = "Message";
            // 
            // ShutdownServerButton
            // 
            this.ShutdownServerButton.Location = new System.Drawing.Point(620, 50);
            this.ShutdownServerButton.Name = "ShutdownServerButton";
            this.ShutdownServerButton.Size = new System.Drawing.Size(150, 29);
            this.ShutdownServerButton.TabIndex = 5;
            this.ShutdownServerButton.Text = "Shutdown Server";
            this.ShutdownServerButton.UseVisualStyleBackColor = true;
            this.ShutdownServerButton.Click += new System.EventHandler(this.ShutdownServerButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(517, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Participants";
            // 
            // ParticipantsBox
            // 
            this.ParticipantsBox.Enabled = false;
            this.ParticipantsBox.Location = new System.Drawing.Point(620, 117);
            this.ParticipantsBox.Multiline = true;
            this.ParticipantsBox.Name = "ParticipantsBox";
            this.ParticipantsBox.Size = new System.Drawing.Size(239, 395);
            this.ParticipantsBox.TabIndex = 8;
            // 
            // ServerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 597);
            this.Controls.Add(this.ParticipantsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShutdownServerButton);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.BoxOfMessages);
            this.Controls.Add(this.ServerBox);
            this.Controls.Add(this.ServerLabel);
            this.Name = "ServerGUI";
            this.Text = "Chat Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerGUI_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label ServerLabel;
        private TextBox ServerBox;
        private TextBox BoxOfMessages;
        private Label MessageLabel;
        private Button ShutdownServerButton;
        private Label label1;
        private TextBox ParticipantsBox;
    }
}