using SpreadsheetGrid_Core;

namespace GUI
{
    partial class SpreadsheetGUI
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
            this.spreadsheetGridWidget1 = new SpreadsheetGrid_Core.SpreadsheetGridWidget();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Help_Cell = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSaveAndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CellNameBox = new System.Windows.Forms.TextBox();
            this.CellValueBox = new System.Windows.Forms.TextBox();
            this.SetContentBox = new System.Windows.Forms.TextBox();
            this.CellNameLabel = new System.Windows.Forms.Label();
            this.CellValueLabel = new System.Windows.Forms.Label();
            this.CellContentsLabel = new System.Windows.Forms.Label();
            this.SetContentButten = new System.Windows.Forms.Button();
            this.Copy = new System.Windows.Forms.Button();
            this.Paste = new System.Windows.Forms.Button();
            this.CutButton = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetGridWidget1
            // 
            this.spreadsheetGridWidget1.AutoScroll = true;
            this.spreadsheetGridWidget1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.spreadsheetGridWidget1.Font = new System.Drawing.Font("Nirmala UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.spreadsheetGridWidget1.Location = new System.Drawing.Point(0, 141);
            this.spreadsheetGridWidget1.Name = "spreadsheetGridWidget1";
            this.spreadsheetGridWidget1.Size = new System.Drawing.Size(1019, 576);
            this.spreadsheetGridWidget1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1019, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.openToolStripMenuItem1,
            this.closeToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.helpToolStripMenuItem.Text = "File";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.viewHelpToolStripMenuItem.Text = "New";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(224, 26);
            this.closeToolStripMenuItem1.Text = "Close";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Help_Cell,
            this.aboutNewToolStripMenuItem,
            this.aboutSaveAndToolStripMenuItem,
            this.aboutOpenToolStripMenuItem,
            this.aboutCloseToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // Help_Cell
            // 
            this.Help_Cell.Name = "Help_Cell";
            this.Help_Cell.Size = new System.Drawing.Size(190, 26);
            this.Help_Cell.Text = "About The Cell";
            this.Help_Cell.Click += new System.EventHandler(this.Help_Cell_Click);
            // 
            // aboutNewToolStripMenuItem
            // 
            this.aboutNewToolStripMenuItem.Name = "aboutNewToolStripMenuItem";
            this.aboutNewToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.aboutNewToolStripMenuItem.Text = "About New";
            this.aboutNewToolStripMenuItem.Click += new System.EventHandler(this.Help_New_Click);
            // 
            // aboutSaveAndToolStripMenuItem
            // 
            this.aboutSaveAndToolStripMenuItem.Name = "aboutSaveAndToolStripMenuItem";
            this.aboutSaveAndToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.aboutSaveAndToolStripMenuItem.Text = "About Save";
            this.aboutSaveAndToolStripMenuItem.Click += new System.EventHandler(this.Help_Save_Click);
            // 
            // aboutOpenToolStripMenuItem
            // 
            this.aboutOpenToolStripMenuItem.Name = "aboutOpenToolStripMenuItem";
            this.aboutOpenToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.aboutOpenToolStripMenuItem.Text = "About Open";
            this.aboutOpenToolStripMenuItem.Click += new System.EventHandler(this.Help_Open_Click);
            // 
            // aboutCloseToolStripMenuItem
            // 
            this.aboutCloseToolStripMenuItem.Name = "aboutCloseToolStripMenuItem";
            this.aboutCloseToolStripMenuItem.Size = new System.Drawing.Size(190, 26);
            this.aboutCloseToolStripMenuItem.Text = "About  Close";
            this.aboutCloseToolStripMenuItem.Click += new System.EventHandler(this.Help_Close_Click);
            // 
            // CellNameBox
            // 
            this.CellNameBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.CellNameBox.Enabled = false;
            this.CellNameBox.Location = new System.Drawing.Point(133, 35);
            this.CellNameBox.Name = "CellNameBox";
            this.CellNameBox.Size = new System.Drawing.Size(125, 27);
            this.CellNameBox.TabIndex = 3;
            // 
            // CellValueBox
            // 
            this.CellValueBox.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.CellValueBox.Enabled = false;
            this.CellValueBox.Location = new System.Drawing.Point(133, 71);
            this.CellValueBox.Name = "CellValueBox";
            this.CellValueBox.Size = new System.Drawing.Size(242, 27);
            this.CellValueBox.TabIndex = 4;
            // 
            // SetContentBox
            // 
            this.SetContentBox.BackColor = System.Drawing.Color.White;
            this.SetContentBox.Location = new System.Drawing.Point(133, 107);
            this.SetContentBox.Name = "SetContentBox";
            this.SetContentBox.Size = new System.Drawing.Size(125, 27);
            this.SetContentBox.TabIndex = 5;
            // 
            // CellNameLabel
            // 
            this.CellNameLabel.AutoSize = true;
            this.CellNameLabel.Location = new System.Drawing.Point(12, 42);
            this.CellNameLabel.Name = "CellNameLabel";
            this.CellNameLabel.Size = new System.Drawing.Size(81, 20);
            this.CellNameLabel.TabIndex = 6;
            this.CellNameLabel.Text = "Cell Name:";
            // 
            // CellValueLabel
            // 
            this.CellValueLabel.AutoSize = true;
            this.CellValueLabel.Location = new System.Drawing.Point(12, 78);
            this.CellValueLabel.Name = "CellValueLabel";
            this.CellValueLabel.Size = new System.Drawing.Size(77, 20);
            this.CellValueLabel.TabIndex = 7;
            this.CellValueLabel.Text = "Cell Value:";
            // 
            // CellContentsLabel
            // 
            this.CellContentsLabel.AutoSize = true;
            this.CellContentsLabel.Location = new System.Drawing.Point(12, 114);
            this.CellContentsLabel.Name = "CellContentsLabel";
            this.CellContentsLabel.Size = new System.Drawing.Size(99, 20);
            this.CellContentsLabel.TabIndex = 8;
            this.CellContentsLabel.Text = "Cell Contents:";
            // 
            // SetContentButten
            // 
            this.SetContentButten.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SetContentButten.Location = new System.Drawing.Point(281, 106);
            this.SetContentButten.Name = "SetContentButten";
            this.SetContentButten.Size = new System.Drawing.Size(94, 29);
            this.SetContentButten.TabIndex = 9;
            this.SetContentButten.Text = "Set";
            this.SetContentButten.UseVisualStyleBackColor = false;
            this.SetContentButten.Click += new System.EventHandler(this.SetContentButton_Click);
            // 
            // Copy
            // 
            this.Copy.Location = new System.Drawing.Point(381, 33);
            this.Copy.Name = "Copy";
            this.Copy.Size = new System.Drawing.Size(94, 29);
            this.Copy.TabIndex = 10;
            this.Copy.Text = "Copy";
            this.Copy.UseVisualStyleBackColor = true;
            this.Copy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // Paste
            // 
            this.Paste.Location = new System.Drawing.Point(481, 33);
            this.Paste.Name = "Paste";
            this.Paste.Size = new System.Drawing.Size(94, 29);
            this.Paste.TabIndex = 11;
            this.Paste.Text = "Paste";
            this.Paste.UseVisualStyleBackColor = true;
            this.Paste.Click += new System.EventHandler(this.Paste_Click);
            // 
            // CutButton
            // 
            this.CutButton.Location = new System.Drawing.Point(281, 35);
            this.CutButton.Name = "CutButton";
            this.CutButton.Size = new System.Drawing.Size(94, 29);
            this.CutButton.TabIndex = 12;
            this.CutButton.Text = "Cut";
            this.CutButton.UseVisualStyleBackColor = true;
            this.CutButton.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(381, 105);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(94, 29);
            this.Delete.TabIndex = 13;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // SpreadsheetGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1019, 714);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.CutButton);
            this.Controls.Add(this.Paste);
            this.Controls.Add(this.Copy);
            this.Controls.Add(this.SetContentButten);
            this.Controls.Add(this.CellContentsLabel);
            this.Controls.Add(this.CellValueLabel);
            this.Controls.Add(this.CellNameLabel);
            this.Controls.Add(this.SetContentBox);
            this.Controls.Add(this.CellValueBox);
            this.Controls.Add(this.CellNameBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.spreadsheetGridWidget1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SpreadsheetGUI";
            this.Text = "SpreadsheetGUI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpreadsheetGrid_Core.SpreadsheetGridWidget spreadsheetGridWidget1;
        private MenuStrip menuStrip1;
        private TextBox CellNameBox;
        private TextBox CellValueBox;
        private TextBox SetContentBox;
        private Label CellNameLabel;
        private Label CellValueLabel;
        private Label CellContentsLabel;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem viewHelpToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem1;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripMenuItem closeToolStripMenuItem1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private Button SetContentButten;
        private Button Copy;
        private Button Paste;
        private Button CutButton;
        private Button Delete;
        private ToolStripMenuItem Help_Cell;
        private ToolStripMenuItem aboutSaveAndToolStripMenuItem;
        private ToolStripMenuItem aboutNewToolStripMenuItem;
        private ToolStripMenuItem aboutOpenToolStripMenuItem;
        private ToolStripMenuItem aboutCloseToolStripMenuItem;
    }
}