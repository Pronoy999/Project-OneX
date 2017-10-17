namespace One_X {
    partial class Assembler {
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
            this.insts = new System.Windows.Forms.ListView();
            this.stubColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addressLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnemonicLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hexcodeLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bytesLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mcycleLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tstateLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rightPanel = new System.Windows.Forms.Panel();
            this.startAddressGB = new System.Windows.Forms.GroupBox();
            this.startAddressBox = new System.Windows.Forms.TextBox();
            this.setAddressButton = new System.Windows.Forms.Button();
            this.rightPanel.SuspendLayout();
            this.startAddressGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // insts
            // 
            this.insts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.stubColumn,
            this.addressLC,
            this.mnemonicLC,
            this.hexcodeLC,
            this.bytesLC,
            this.mcycleLC,
            this.tstateLC});
            this.insts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.insts.FullRowSelect = true;
            this.insts.Location = new System.Drawing.Point(0, 0);
            this.insts.MultiSelect = false;
            this.insts.Name = "insts";
            this.insts.Size = new System.Drawing.Size(532, 366);
            this.insts.TabIndex = 4;
            this.insts.UseCompatibleStateImageBehavior = false;
            this.insts.View = System.Windows.Forms.View.Details;
            // 
            // stubColumn
            // 
            this.stubColumn.Text = "";
            this.stubColumn.Width = 32;
            // 
            // addressLC
            // 
            this.addressLC.Text = "Address";
            this.addressLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.addressLC.Width = 70;
            // 
            // mnemonicLC
            // 
            this.mnemonicLC.Text = "Mnemonic";
            this.mnemonicLC.Width = 125;
            // 
            // hexcodeLC
            // 
            this.hexcodeLC.Text = "Hex";
            this.hexcodeLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bytesLC
            // 
            this.bytesLC.Text = "Bytes";
            this.bytesLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bytesLC.Width = 50;
            // 
            // mcycleLC
            // 
            this.mcycleLC.Text = "M-Cycles";
            this.mcycleLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mcycleLC.Width = 75;
            // 
            // tstateLC
            // 
            this.tstateLC.Text = "T-States";
            this.tstateLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tstateLC.Width = 75;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.startAddressGB);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(532, 0);
            this.rightPanel.MinimumSize = new System.Drawing.Size(172, 281);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(172, 366);
            this.rightPanel.TabIndex = 6;
            // 
            // startAddressGB
            // 
            this.startAddressGB.Controls.Add(this.startAddressBox);
            this.startAddressGB.Controls.Add(this.setAddressButton);
            this.startAddressGB.Location = new System.Drawing.Point(13, 12);
            this.startAddressGB.Name = "startAddressGB";
            this.startAddressGB.Size = new System.Drawing.Size(146, 66);
            this.startAddressGB.TabIndex = 5;
            this.startAddressGB.TabStop = false;
            this.startAddressGB.Text = "Set Start Address";
            // 
            // startAddressBox
            // 
            this.startAddressBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.startAddressBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startAddressBox.Location = new System.Drawing.Point(6, 25);
            this.startAddressBox.Name = "startAddressBox";
            this.startAddressBox.Size = new System.Drawing.Size(62, 33);
            this.startAddressBox.TabIndex = 1;
            this.startAddressBox.TabStop = false;
            this.startAddressBox.Text = "0000";
            this.startAddressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startAddressBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.startAddressBox_KeyPress);
            // 
            // setAddressButton
            // 
            this.setAddressButton.Location = new System.Drawing.Point(78, 25);
            this.setAddressButton.Name = "setAddressButton";
            this.setAddressButton.Size = new System.Drawing.Size(62, 34);
            this.setAddressButton.TabIndex = 0;
            this.setAddressButton.Text = "SET";
            this.setAddressButton.UseVisualStyleBackColor = true;
            // 
            // Assembler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 366);
            this.Controls.Add(this.insts);
            this.Controls.Add(this.rightPanel);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 320);
            this.Name = "Assembler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assembler";
            this.Load += new System.EventHandler(this.Assembler_Load);
            this.rightPanel.ResumeLayout(false);
            this.startAddressGB.ResumeLayout(false);
            this.startAddressGB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView insts;
        private System.Windows.Forms.ColumnHeader stubColumn;
        private System.Windows.Forms.ColumnHeader addressLC;
        private System.Windows.Forms.ColumnHeader mnemonicLC;
        private System.Windows.Forms.ColumnHeader hexcodeLC;
        private System.Windows.Forms.ColumnHeader bytesLC;
        private System.Windows.Forms.ColumnHeader mcycleLC;
        private System.Windows.Forms.ColumnHeader tstateLC;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.GroupBox startAddressGB;
        public System.Windows.Forms.TextBox startAddressBox;
        private System.Windows.Forms.Button setAddressButton;
    }
}