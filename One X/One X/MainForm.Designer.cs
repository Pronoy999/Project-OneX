namespace One_X {
    partial class MainForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.codeBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.MenuBar = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.newMI = new System.Windows.Forms.MenuItem();
            this.openMI = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.saveMI = new System.Windows.Forms.MenuItem();
            this.saveasMI = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.closeMI = new System.Windows.Forms.MenuItem();
            this.exitMI = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.cutMI = new System.Windows.Forms.MenuItem();
            this.copyMI = new System.Windows.Forms.MenuItem();
            this.pasteMI = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.undoMI = new System.Windows.Forms.MenuItem();
            this.redoMI = new System.Windows.Forms.MenuItem();
            this.menuItem22 = new System.Windows.Forms.MenuItem();
            this.selectallMI = new System.Windows.Forms.MenuItem();
            this.viewMI = new System.Windows.Forms.MenuItem();
            this.memeditMI = new System.Windows.Forms.MenuItem();
            this.assemblerMI = new System.Windows.Forms.MenuItem();
            this.datamoniMI = new System.Windows.Forms.MenuItem();
            this.execMI = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.zoomMI = new System.Windows.Forms.MenuItem();
            this.zoominMI = new System.Windows.Forms.MenuItem();
            this.zoomoutMI = new System.Windows.Forms.MenuItem();
            this.resetzoomMI = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.findMI = new System.Windows.Forms.MenuItem();
            this.replaceMI = new System.Windows.Forms.MenuItem();
            this.findnextMI = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.gotoMI = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.ucaseMI = new System.Windows.Forms.MenuItem();
            this.lcaseMI = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.lineMI = new System.Windows.Forms.MenuItem();
            this.cmntMI = new System.Windows.Forms.MenuItem();
            this.dellineMI = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.optionsMI = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.updateMI = new System.Windows.Forms.MenuItem();
            this.aboutMI = new System.Windows.Forms.MenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.lineinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.columninfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.modifiedinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.locinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.insrepinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.parseTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.codeBox)).BeginInit();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeBox
            // 
            this.codeBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.codeBox.AutoScrollMinSize = new System.Drawing.Size(47, 24);
            this.codeBox.BackBrush = null;
            this.codeBox.CharHeight = 16;
            this.codeBox.CharWidth = 9;
            this.codeBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.codeBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBox.Font = new System.Drawing.Font("Courier New", 11F);
            this.codeBox.Hotkeys = resources.GetString("codeBox.Hotkeys");
            this.codeBox.IsReplaceMode = false;
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Name = "codeBox";
            this.codeBox.Paddings = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.codeBox.ReservedCountOfLineNumberChars = 3;
            this.codeBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.codeBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("codeBox.ServiceColors")));
            this.codeBox.Size = new System.Drawing.Size(704, 417);
            this.codeBox.TabIndex = 0;
            this.codeBox.Zoom = 100;
            this.codeBox.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.codeBox_TextChanged);
            this.codeBox.SelectionChanged += new System.EventHandler(this.codeBox_SelectionChanged);
            this.codeBox.AutoIndentNeeded += new System.EventHandler<FastColoredTextBoxNS.AutoIndentEventArgs>(this.codeBox_AutoIndentNeeded);
            this.codeBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.codeBox_KeyUp);
            // 
            // MenuBar
            // 
            this.MenuBar.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.viewMI,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newMI,
            this.openMI,
            this.menuItem12,
            this.saveMI,
            this.saveasMI,
            this.menuItem13,
            this.closeMI,
            this.exitMI});
            this.menuItem1.Text = "File";
            // 
            // newMI
            // 
            this.newMI.Index = 0;
            this.newMI.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.newMI.Text = "New";
            this.newMI.Click += new System.EventHandler(this.newMI_Click);
            // 
            // openMI
            // 
            this.openMI.Index = 1;
            this.openMI.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openMI.Text = "Open";
            this.openMI.Click += new System.EventHandler(this.openMI_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 2;
            this.menuItem12.Text = "-";
            // 
            // saveMI
            // 
            this.saveMI.Index = 3;
            this.saveMI.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.saveMI.Text = "Save";
            this.saveMI.Click += new System.EventHandler(this.saveMI_Click);
            // 
            // saveasMI
            // 
            this.saveasMI.Index = 4;
            this.saveasMI.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
            this.saveasMI.Text = "Save As";
            this.saveasMI.Click += new System.EventHandler(this.saveasMI_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 5;
            this.menuItem13.Text = "-";
            // 
            // closeMI
            // 
            this.closeMI.Index = 6;
            this.closeMI.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.closeMI.Text = "Close";
            this.closeMI.Click += new System.EventHandler(this.closeMI_Click);
            // 
            // exitMI
            // 
            this.exitMI.Index = 7;
            this.exitMI.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.exitMI.Text = "Exit";
            this.exitMI.Click += new System.EventHandler(this.exitMI_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.cutMI,
            this.copyMI,
            this.pasteMI,
            this.menuItem18,
            this.undoMI,
            this.redoMI,
            this.menuItem22,
            this.selectallMI});
            this.menuItem2.Text = "Edit";
            // 
            // cutMI
            // 
            this.cutMI.Index = 0;
            this.cutMI.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.cutMI.Text = "Cut";
            this.cutMI.Click += new System.EventHandler(this.cutMI_Click);
            // 
            // copyMI
            // 
            this.copyMI.Index = 1;
            this.copyMI.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.copyMI.Text = "Copy";
            this.copyMI.Click += new System.EventHandler(this.copyMI_Click);
            // 
            // pasteMI
            // 
            this.pasteMI.Index = 2;
            this.pasteMI.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.pasteMI.Text = "Paste";
            this.pasteMI.Click += new System.EventHandler(this.pasteMI_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 3;
            this.menuItem18.Text = "-";
            // 
            // undoMI
            // 
            this.undoMI.Index = 4;
            this.undoMI.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.undoMI.Text = "Undo";
            this.undoMI.Click += new System.EventHandler(this.undoMI_Click);
            // 
            // redoMI
            // 
            this.redoMI.Index = 5;
            this.redoMI.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
            this.redoMI.Text = "Redo";
            this.redoMI.Click += new System.EventHandler(this.redoMI_Click);
            // 
            // menuItem22
            // 
            this.menuItem22.Index = 6;
            this.menuItem22.Text = "-";
            // 
            // selectallMI
            // 
            this.selectallMI.Index = 7;
            this.selectallMI.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
            this.selectallMI.Text = "Select All";
            this.selectallMI.Click += new System.EventHandler(this.selectallMI_Click);
            // 
            // viewMI
            // 
            this.viewMI.Index = 2;
            this.viewMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.memeditMI,
            this.assemblerMI,
            this.datamoniMI,
            this.execMI,
            this.menuItem19,
            this.zoomMI});
            this.viewMI.Text = "View";
            // 
            // memeditMI
            // 
            this.memeditMI.Index = 0;
            this.memeditMI.Text = "Memory Editor";
            this.memeditMI.Click += new System.EventHandler(this.memeditMI_Click);
            // 
            // assemblerMI
            // 
            this.assemblerMI.Index = 1;
            this.assemblerMI.Text = "Assembler";
            this.assemblerMI.Click += new System.EventHandler(this.assemblerMI_Click);
            // 
            // datamoniMI
            // 
            this.datamoniMI.Index = 2;
            this.datamoniMI.Text = "Data Monitor";
            this.datamoniMI.Click += new System.EventHandler(this.datamoniMI_Click);
            // 
            // execMI
            // 
            this.execMI.Index = 3;
            this.execMI.Text = "Executer";
            this.execMI.Click += new System.EventHandler(this.execMI_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 4;
            this.menuItem19.Text = "-";
            // 
            // zoomMI
            // 
            this.zoomMI.Index = 5;
            this.zoomMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.zoominMI,
            this.zoomoutMI,
            this.resetzoomMI});
            this.zoomMI.Text = "Zoom";
            // 
            // zoominMI
            // 
            this.zoominMI.Index = 0;
            this.zoominMI.Text = "Zoom In";
            this.zoominMI.Click += new System.EventHandler(this.zoominMI_Click);
            // 
            // zoomoutMI
            // 
            this.zoomoutMI.Index = 1;
            this.zoomoutMI.Text = "Zoom Out";
            this.zoomoutMI.Click += new System.EventHandler(this.zoomoutMI_Click);
            // 
            // resetzoomMI
            // 
            this.resetzoomMI.Index = 2;
            this.resetzoomMI.Shortcut = System.Windows.Forms.Shortcut.Ctrl0;
            this.resetzoomMI.Text = "Reset Zoom";
            this.resetzoomMI.Click += new System.EventHandler(this.resetzoomMI_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.findMI,
            this.replaceMI,
            this.findnextMI,
            this.menuItem3,
            this.gotoMI,
            this.menuItem10,
            this.ucaseMI,
            this.lcaseMI,
            this.menuItem15,
            this.lineMI});
            this.menuItem4.Text = "Code";
            // 
            // findMI
            // 
            this.findMI.Index = 0;
            this.findMI.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.findMI.Text = "Find";
            this.findMI.Click += new System.EventHandler(this.findMI_Click);
            // 
            // replaceMI
            // 
            this.replaceMI.Index = 1;
            this.replaceMI.Shortcut = System.Windows.Forms.Shortcut.CtrlH;
            this.replaceMI.Text = "Replace";
            this.replaceMI.Click += new System.EventHandler(this.replaceMI_Click);
            // 
            // findnextMI
            // 
            this.findnextMI.Index = 2;
            this.findnextMI.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.findnextMI.Text = "Find Next";
            this.findnextMI.Click += new System.EventHandler(this.findnextMI_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "-";
            // 
            // gotoMI
            // 
            this.gotoMI.Index = 4;
            this.gotoMI.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
            this.gotoMI.Text = "Goto";
            this.gotoMI.Click += new System.EventHandler(this.gotoMI_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 5;
            this.menuItem10.Text = "-";
            // 
            // ucaseMI
            // 
            this.ucaseMI.Index = 6;
            this.ucaseMI.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
            this.ucaseMI.Text = "Uppercase";
            this.ucaseMI.Click += new System.EventHandler(this.ucaseMI_Click);
            // 
            // lcaseMI
            // 
            this.lcaseMI.Index = 7;
            this.lcaseMI.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftU;
            this.lcaseMI.Text = "Lowercase";
            this.lcaseMI.Click += new System.EventHandler(this.lcaseMI_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 8;
            this.menuItem15.Text = "-";
            // 
            // lineMI
            // 
            this.lineMI.Index = 9;
            this.lineMI.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.cmntMI,
            this.dellineMI});
            this.lineMI.Text = "Line";
            // 
            // cmntMI
            // 
            this.cmntMI.Index = 0;
            this.cmntMI.Text = "Comment/Uncomment";
            this.cmntMI.Click += new System.EventHandler(this.cmntMI_Click);
            // 
            // dellineMI
            // 
            this.dellineMI.Index = 1;
            this.dellineMI.Shortcut = System.Windows.Forms.Shortcut.ShiftDel;
            this.dellineMI.Text = "Delete Line";
            this.dellineMI.Click += new System.EventHandler(this.dellineMI_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.optionsMI});
            this.menuItem5.Text = "Tools";
            // 
            // optionsMI
            // 
            this.optionsMI.Index = 0;
            this.optionsMI.Text = "Options";
            this.optionsMI.Click += new System.EventHandler(this.optionsMI_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.updateMI,
            this.aboutMI});
            this.menuItem6.Text = "Help";
            // 
            // updateMI
            // 
            this.updateMI.Enabled = false;
            this.updateMI.Index = 0;
            this.updateMI.Text = "Update";
            // 
            // aboutMI
            // 
            this.aboutMI.Index = 1;
            this.aboutMI.Text = "About";
            this.aboutMI.Click += new System.EventHandler(this.aboutMI_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineinfo,
            this.columninfo,
            this.modifiedinfo,
            this.locinfo,
            this.insrepinfo});
            this.StatusBar.Location = new System.Drawing.Point(0, 417);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(704, 24);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 1;
            // 
            // lineinfo
            // 
            this.lineinfo.AutoSize = false;
            this.lineinfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lineinfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lineinfo.Name = "lineinfo";
            this.lineinfo.Size = new System.Drawing.Size(100, 19);
            this.lineinfo.Text = "Line: 0";
            this.lineinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // columninfo
            // 
            this.columninfo.AutoSize = false;
            this.columninfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.columninfo.Name = "columninfo";
            this.columninfo.Size = new System.Drawing.Size(150, 19);
            this.columninfo.Text = "Column: 0";
            this.columninfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modifiedinfo
            // 
            this.modifiedinfo.AutoSize = false;
            this.modifiedinfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.modifiedinfo.Name = "modifiedinfo";
            this.modifiedinfo.Size = new System.Drawing.Size(189, 19);
            this.modifiedinfo.Spring = true;
            // 
            // locinfo
            // 
            this.locinfo.AutoSize = false;
            this.locinfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.locinfo.Name = "locinfo";
            this.locinfo.Size = new System.Drawing.Size(150, 19);
            this.locinfo.Text = "Location: 0";
            this.locinfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // insrepinfo
            // 
            this.insrepinfo.AutoSize = false;
            this.insrepinfo.Name = "insrepinfo";
            this.insrepinfo.Size = new System.Drawing.Size(100, 19);
            this.insrepinfo.Text = "INSERT";
            this.insrepinfo.Click += new System.EventHandler(this.insrepinfo_Click);
            // 
            // openFile
            // 
            this.openFile.Filter = "OneX Files|*.onex";
            this.openFile.RestoreDirectory = true;
            // 
            // saveFile
            // 
            this.saveFile.Filter = "OneX Files|*.onex";
            this.saveFile.RestoreDirectory = true;
            // 
            // parseTimer
            // 
            this.parseTimer.Enabled = true;
            this.parseTimer.Interval = 500;
            this.parseTimer.Tick += new System.EventHandler(this.parseTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.StatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(50, 50);
            this.Menu = this.MenuBar;
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "One X Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.codeBox)).EndInit();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox codeBox;
        private System.Windows.Forms.MainMenu MenuBar;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem newMI;
        private System.Windows.Forms.MenuItem openMI;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem saveMI;
        private System.Windows.Forms.MenuItem saveasMI;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem closeMI;
        private System.Windows.Forms.MenuItem exitMI;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem cutMI;
        private System.Windows.Forms.MenuItem copyMI;
        private System.Windows.Forms.MenuItem pasteMI;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem undoMI;
        private System.Windows.Forms.MenuItem redoMI;
        private System.Windows.Forms.MenuItem menuItem22;
        private System.Windows.Forms.MenuItem selectallMI;
        private System.Windows.Forms.MenuItem viewMI;
        private System.Windows.Forms.MenuItem memeditMI;
        private System.Windows.Forms.MenuItem assemblerMI;
        private System.Windows.Forms.MenuItem datamoniMI;
        private System.Windows.Forms.MenuItem execMI;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem optionsMI;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem updateMI;
        private System.Windows.Forms.MenuItem aboutMI;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem zoomMI;
        private System.Windows.Forms.MenuItem zoominMI;
        private System.Windows.Forms.MenuItem zoomoutMI;
        private System.Windows.Forms.MenuItem resetzoomMI;
        private System.Windows.Forms.MenuItem findMI;
        private System.Windows.Forms.MenuItem findnextMI;
        private System.Windows.Forms.MenuItem replaceMI;
        private System.Windows.Forms.MenuItem gotoMI;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem ucaseMI;
        private System.Windows.Forms.MenuItem lcaseMI;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem cmntMI;
        private System.Windows.Forms.MenuItem dellineMI;
        private System.Windows.Forms.MenuItem lineMI;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel lineinfo;
        private System.Windows.Forms.ToolStripStatusLabel columninfo;
        private System.Windows.Forms.ToolStripStatusLabel modifiedinfo;
        private System.Windows.Forms.ToolStripStatusLabel locinfo;
        private System.Windows.Forms.ToolStripStatusLabel insrepinfo;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.Timer parseTimer;
    }
}