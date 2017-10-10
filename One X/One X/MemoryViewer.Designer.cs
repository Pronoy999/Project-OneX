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
            this.memBox = new Be.Windows.Forms.HexBox();
            this.statusPanel = new System.Windows.Forms.Panel();
            this.gotoBtn = new System.Windows.Forms.Button();
            this.offset = new System.Windows.Forms.TextBox();
            this.offsetLabel = new System.Windows.Forms.Label();
            this.statusPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // memBox
            // 
            this.memBox.ColumnInfoVisible = true;
            this.memBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.memBox.GroupSeparatorVisible = true;
            this.memBox.LineInfoVisible = true;
            this.memBox.Location = new System.Drawing.Point(0, 0);
            this.memBox.Name = "memBox";
            this.memBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.memBox.ShadowSelectionVisible = false;
            this.memBox.Size = new System.Drawing.Size(600, 697);
            this.memBox.TabIndex = 0;
            this.memBox.UseFixedBytesPerLine = true;
            this.memBox.VScrollBarVisible = true;
            this.memBox.SelectionStartChanged += new System.EventHandler(this.memBox_SelectionStartChanged);
            // 
            // statusPanel
            // 
            this.statusPanel.Controls.Add(this.gotoBtn);
            this.statusPanel.Controls.Add(this.offset);
            this.statusPanel.Controls.Add(this.offsetLabel);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusPanel.Location = new System.Drawing.Point(0, 697);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(600, 32);
            this.statusPanel.TabIndex = 1;
            // 
            // gotoBtn
            // 
            this.gotoBtn.Font = new System.Drawing.Font("Trebuchet MS", 10F);
            this.gotoBtn.Location = new System.Drawing.Point(537, 3);
            this.gotoBtn.Name = "gotoBtn";
            this.gotoBtn.Size = new System.Drawing.Size(60, 26);
            this.gotoBtn.TabIndex = 2;
            this.gotoBtn.Text = "GOTO";
            this.gotoBtn.UseVisualStyleBackColor = true;
            this.gotoBtn.Click += new System.EventHandler(this.gotoBtn_Click);
            // 
            // offset
            // 
            this.offset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.offset.Font = new System.Drawing.Font("Hack", 12F);
            this.offset.Location = new System.Drawing.Point(488, 3);
            this.offset.Name = "offset";
            this.offset.Size = new System.Drawing.Size(46, 26);
            this.offset.TabIndex = 1;
            this.offset.Text = "0000";
            this.offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.offset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.offset_KeyPress);
            // 
            // offsetLabel
            // 
            this.offsetLabel.AutoSize = true;
            this.offsetLabel.Location = new System.Drawing.Point(431, 6);
            this.offsetLabel.Name = "offsetLabel";
            this.offsetLabel.Size = new System.Drawing.Size(60, 22);
            this.offsetLabel.TabIndex = 0;
            this.offsetLabel.Text = "Offset:";
            this.offsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MemoryViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(600, 729);
            this.Controls.Add(this.memBox);
            this.Controls.Add(this.statusPanel);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MemoryViewer";
            this.Text = "MemoryViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MemoryViewer_FormClosing);
            this.Load += new System.EventHandler(this.MemoryViewer_Load);
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public Be.Windows.Forms.HexBox memBox;
        private System.Windows.Forms.Panel statusPanel;
        private System.Windows.Forms.Label offsetLabel;
        private System.Windows.Forms.Button gotoBtn;
        private System.Windows.Forms.TextBox offset;
    }
}