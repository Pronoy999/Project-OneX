namespace One_X {
    partial class MemoryViewer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.memBox = new Be.Windows.Forms.HexBox();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.gotoBtn = new System.Windows.Forms.Button();
            this.offset = new System.Windows.Forms.TextBox();
            this.offsetLabel = new System.Windows.Forms.Label();
            this.MemoryTabControl = new System.Windows.Forms.TabControl();
            this.MemoryListPage = new System.Windows.Forms.TabPage();
            this.MemoryDumpPage = new System.Windows.Forms.TabPage();
            this.memList = new System.Windows.Forms.ListView();
            this.memaddrcol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.memvalcol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.memEditMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusPanel.SuspendLayout();
            this.MemoryTabControl.SuspendLayout();
            this.MemoryListPage.SuspendLayout();
            this.MemoryDumpPage.SuspendLayout();
            this.memEditMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // memBox
            // 
            this.memBox.ColumnInfoVisible = true;
            this.memBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.memBox.GroupSeparatorVisible = true;
            this.memBox.LineInfoVisible = true;
            this.memBox.Location = new System.Drawing.Point(3, 3);
            this.memBox.Name = "memBox";
            this.memBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.memBox.ShadowSelectionVisible = false;
            this.memBox.Size = new System.Drawing.Size(470, 372);
            this.memBox.TabIndex = 0;
            this.memBox.UseFixedBytesPerLine = true;
            this.memBox.VScrollBarVisible = true;
            this.memBox.SelectionStartChanged += new System.EventHandler(this.memBox_SelectionStartChanged);
            // 
            // statusPanel
            // 
            this.statusPanel.BackColor = System.Drawing.SystemColors.Control;
            this.statusPanel.Controls.Add(this.gotoBtn);
            this.statusPanel.Controls.Add(this.offset);
            this.statusPanel.Controls.Add(this.offsetLabel);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Location = new System.Drawing.Point(3, 375);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(470, 32);
            this.statusPanel.TabIndex = 1;
            // 
            // gotoBtn
            // 
            this.gotoBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.gotoBtn.Font = new System.Drawing.Font("Trebuchet MS", 10F);
            this.gotoBtn.Location = new System.Drawing.Point(407, 3);
            this.gotoBtn.Name = "gotoBtn";
            this.gotoBtn.Size = new System.Drawing.Size(60, 26);
            this.gotoBtn.TabIndex = 2;
            this.gotoBtn.Text = "GOTO";
            this.gotoBtn.UseVisualStyleBackColor = true;
            this.gotoBtn.Click += new System.EventHandler(this.gotoBtn_Click);
            // 
            // offset
            // 
            this.offset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.offset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.offset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.offset.Location = new System.Drawing.Point(358, 3);
            this.offset.Name = "offset";
            this.offset.Size = new System.Drawing.Size(46, 26);
            this.offset.TabIndex = 1;
            this.offset.Text = "0000";
            this.offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.offset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.offset_KeyPress);
            // 
            // offsetLabel
            // 
            this.offsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.offsetLabel.AutoSize = true;
            this.offsetLabel.Location = new System.Drawing.Point(301, 6);
            this.offsetLabel.Name = "offsetLabel";
            this.offsetLabel.Size = new System.Drawing.Size(46, 18);
            this.offsetLabel.TabIndex = 0;
            this.offsetLabel.Text = "Offset:";
            this.offsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MemoryTabControl
            // 
            this.MemoryTabControl.Controls.Add(this.MemoryListPage);
            this.MemoryTabControl.Controls.Add(this.MemoryDumpPage);
            this.MemoryTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MemoryTabControl.Location = new System.Drawing.Point(0, 0);
            this.MemoryTabControl.Name = "MemoryTabControl";
            this.MemoryTabControl.SelectedIndex = 0;
            this.MemoryTabControl.Size = new System.Drawing.Size(484, 441);
            this.MemoryTabControl.TabIndex = 2;
            // 
            // MemoryListPage
            // 
            this.MemoryListPage.Controls.Add(this.memList);
            this.MemoryListPage.Location = new System.Drawing.Point(4, 27);
            this.MemoryListPage.Name = "MemoryListPage";
            this.MemoryListPage.Padding = new System.Windows.Forms.Padding(3);
            this.MemoryListPage.Size = new System.Drawing.Size(476, 410);
            this.MemoryListPage.TabIndex = 0;
            this.MemoryListPage.Text = "Memory List";
            this.MemoryListPage.UseVisualStyleBackColor = true;
            // 
            // MemoryDumpPage
            // 
            this.MemoryDumpPage.Controls.Add(this.memBox);
            this.MemoryDumpPage.Controls.Add(this.statusPanel);
            this.MemoryDumpPage.Location = new System.Drawing.Point(4, 27);
            this.MemoryDumpPage.Name = "MemoryDumpPage";
            this.MemoryDumpPage.Padding = new System.Windows.Forms.Padding(3);
            this.MemoryDumpPage.Size = new System.Drawing.Size(476, 410);
            this.MemoryDumpPage.TabIndex = 1;
            this.MemoryDumpPage.Text = "Memory Dump";
            this.MemoryDumpPage.UseVisualStyleBackColor = true;
            // 
            // memList
            // 
            this.memList.CheckBoxes = true;
            this.memList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.memaddrcol,
            this.memvalcol});
            this.memList.ContextMenuStrip = this.memEditMenu;
            this.memList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memList.FullRowSelect = true;
            this.memList.GridLines = true;
            this.memList.HideSelection = false;
            this.memList.Location = new System.Drawing.Point(3, 3);
            this.memList.MultiSelect = false;
            this.memList.Name = "memList";
            this.memList.Size = new System.Drawing.Size(470, 404);
            this.memList.TabIndex = 0;
            this.memList.UseCompatibleStateImageBehavior = false;
            this.memList.View = System.Windows.Forms.View.Details;
            this.memList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.memList_KeyDown);
            this.memList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.memList_MouseDown);
            // 
            // memaddrcol
            // 
            this.memaddrcol.Text = "Address";
            this.memaddrcol.Width = 264;
            // 
            // memvalcol
            // 
            this.memvalcol.Text = "Value";
            this.memvalcol.Width = 191;
            // 
            // memEditMenu
            // 
            this.memEditMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.memEditMenu.Name = "memEditMenu";
            this.memEditMenu.Size = new System.Drawing.Size(108, 70);
            this.memEditMenu.Text = "Operations";
            this.memEditMenu.Opening += new System.ComponentModel.CancelEventHandler(this.memEditMenu_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addItem);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editItem);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Enabled = false;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteItem);
            // 
            // MemoryViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(484, 441);
            this.Controls.Add(this.MemoryTabControl);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 480);
            this.Name = "MemoryViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MemoryViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MemoryViewer_FormClosing);
            this.Load += new System.EventHandler(this.MemoryViewer_Load);
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            this.MemoryTabControl.ResumeLayout(false);
            this.MemoryListPage.ResumeLayout(false);
            this.MemoryDumpPage.ResumeLayout(false);
            this.memEditMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public Be.Windows.Forms.HexBox memBox;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label offsetLabel;
        private System.Windows.Forms.Button gotoBtn;
        private System.Windows.Forms.TextBox offset;
        private System.Windows.Forms.TabControl MemoryTabControl;
        private System.Windows.Forms.TabPage MemoryListPage;
        private System.Windows.Forms.TabPage MemoryDumpPage;
        private System.Windows.Forms.ListView memList;
        private System.Windows.Forms.ColumnHeader memaddrcol;
        private System.Windows.Forms.ColumnHeader memvalcol;
        private System.Windows.Forms.ContextMenuStrip memEditMenu;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}