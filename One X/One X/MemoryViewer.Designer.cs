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
            this.SuspendLayout();
            // 
            // memBox
            // 
            this.memBox.ColumnInfoVisible = true;
            this.memBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.memBox.GroupSeparatorVisible = true;
            this.memBox.LineInfoVisible = true;
            this.memBox.Location = new System.Drawing.Point(0, 0);
            this.memBox.Name = "memBox";
            this.memBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.memBox.ShadowSelectionVisible = false;
            this.memBox.Size = new System.Drawing.Size(600, 729);
            this.memBox.TabIndex = 0;
            this.memBox.UseFixedBytesPerLine = true;
            this.memBox.VScrollBarVisible = true;
            // 
            // MemoryViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 729);
            this.Controls.Add(this.memBox);
            this.Name = "MemoryViewer";
            this.Text = "MemoryViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MemoryViewer_FormClosing);
            this.Load += new System.EventHandler(this.MemoryViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public Be.Windows.Forms.HexBox memBox;
    }
}