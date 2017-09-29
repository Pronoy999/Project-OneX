namespace One_X
{
    partial class MainForm
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
            this.codeBox = new System.Windows.Forms.RichTextBox();
            this.codePanel = new System.Windows.Forms.Panel();
            this.registerPanel = new System.Windows.Forms.Panel();
            this.SFLabel = new System.Windows.Forms.Label();
            this.ZFLabel = new System.Windows.Forms.Label();
            this.ACFLabel = new System.Windows.Forms.Label();
            this.PFLabel = new System.Windows.Forms.Label();
            this.CYFLabel = new System.Windows.Forms.Label();
            this.flagIcon = new System.Windows.Forms.PictureBox();
            this.SFlag = new System.Windows.Forms.Label();
            this.NU1Flag = new System.Windows.Forms.Label();
            this.PFlag = new System.Windows.Forms.Label();
            this.NU3Flag = new System.Windows.Forms.Label();
            this.ACFlag = new System.Windows.Forms.Label();
            this.NU5Flag = new System.Windows.Forms.Label();
            this.ZFlag = new System.Windows.Forms.Label();
            this.CFlag = new System.Windows.Forms.Label();
            this.D7Label = new System.Windows.Forms.Label();
            this.D6Label = new System.Windows.Forms.Label();
            this.D5Label = new System.Windows.Forms.Label();
            this.D4Label = new System.Windows.Forms.Label();
            this.D3Label = new System.Windows.Forms.Label();
            this.D2Label = new System.Windows.Forms.Label();
            this.D1Label = new System.Windows.Forms.Label();
            this.D0Label = new System.Windows.Forms.Label();
            this.insts = new System.Windows.Forms.ListView();
            this.addressLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnemonicLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hexcodeLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bytesLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mcycleLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tstateLC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startAddressGB = new System.Windows.Forms.GroupBox();
            this.startAddressBox = new System.Windows.Forms.TextBox();
            this.setAddressButton = new System.Windows.Forms.Button();
            this.stubColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.codePanel.SuspendLayout();
            this.registerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flagIcon)).BeginInit();
            this.startAddressGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // codeBox
            // 
            this.codeBox.AcceptsTab = true;
            this.codeBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeBox.Font = new System.Drawing.Font("Hack", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeBox.HideSelection = false;
            this.codeBox.Location = new System.Drawing.Point(0, 0);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(471, 703);
            this.codeBox.TabIndex = 0;
            this.codeBox.Text = "";
            this.codeBox.WordWrap = false;
            this.codeBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.codeBox_KeyUp);
            // 
            // codePanel
            // 
            this.codePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.codePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.codePanel.Controls.Add(this.codeBox);
            this.codePanel.Location = new System.Drawing.Point(12, 12);
            this.codePanel.Name = "codePanel";
            this.codePanel.Size = new System.Drawing.Size(473, 705);
            this.codePanel.TabIndex = 1;
            // 
            // registerPanel
            // 
            this.registerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.registerPanel.Controls.Add(this.SFLabel);
            this.registerPanel.Controls.Add(this.ZFLabel);
            this.registerPanel.Controls.Add(this.ACFLabel);
            this.registerPanel.Controls.Add(this.PFLabel);
            this.registerPanel.Controls.Add(this.CYFLabel);
            this.registerPanel.Controls.Add(this.flagIcon);
            this.registerPanel.Controls.Add(this.SFlag);
            this.registerPanel.Controls.Add(this.NU1Flag);
            this.registerPanel.Controls.Add(this.PFlag);
            this.registerPanel.Controls.Add(this.NU3Flag);
            this.registerPanel.Controls.Add(this.ACFlag);
            this.registerPanel.Controls.Add(this.NU5Flag);
            this.registerPanel.Controls.Add(this.ZFlag);
            this.registerPanel.Controls.Add(this.CFlag);
            this.registerPanel.Controls.Add(this.D7Label);
            this.registerPanel.Controls.Add(this.D6Label);
            this.registerPanel.Controls.Add(this.D5Label);
            this.registerPanel.Controls.Add(this.D4Label);
            this.registerPanel.Controls.Add(this.D3Label);
            this.registerPanel.Controls.Add(this.D2Label);
            this.registerPanel.Controls.Add(this.D1Label);
            this.registerPanel.Controls.Add(this.D0Label);
            this.registerPanel.Font = new System.Drawing.Font("Trebuchet MS", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerPanel.Location = new System.Drawing.Point(756, 12);
            this.registerPanel.Name = "registerPanel";
            this.registerPanel.Size = new System.Drawing.Size(240, 303);
            this.registerPanel.TabIndex = 2;
            // 
            // SFLabel
            // 
            this.SFLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SFLabel.Location = new System.Drawing.Point(51, 1);
            this.SFLabel.Name = "SFLabel";
            this.SFLabel.Size = new System.Drawing.Size(23, 12);
            this.SFLabel.TabIndex = 46;
            this.SFLabel.Text = "S";
            this.SFLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ZFLabel
            // 
            this.ZFLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZFLabel.Location = new System.Drawing.Point(73, 1);
            this.ZFLabel.Name = "ZFLabel";
            this.ZFLabel.Size = new System.Drawing.Size(24, 12);
            this.ZFLabel.TabIndex = 45;
            this.ZFLabel.Text = "Z";
            this.ZFLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ACFLabel
            // 
            this.ACFLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ACFLabel.Location = new System.Drawing.Point(119, 1);
            this.ACFLabel.Name = "ACFLabel";
            this.ACFLabel.Size = new System.Drawing.Size(24, 12);
            this.ACFLabel.TabIndex = 44;
            this.ACFLabel.Text = "AC";
            this.ACFLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PFLabel
            // 
            this.PFLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PFLabel.Location = new System.Drawing.Point(165, 1);
            this.PFLabel.Name = "PFLabel";
            this.PFLabel.Size = new System.Drawing.Size(24, 12);
            this.PFLabel.TabIndex = 43;
            this.PFLabel.Text = "P";
            this.PFLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CYFLabel
            // 
            this.CYFLabel.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CYFLabel.Location = new System.Drawing.Point(211, 1);
            this.CYFLabel.Name = "CYFLabel";
            this.CYFLabel.Size = new System.Drawing.Size(24, 12);
            this.CYFLabel.TabIndex = 42;
            this.CYFLabel.Text = "CY";
            this.CYFLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // flagIcon
            // 
            this.flagIcon.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.flagIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flagIcon.Image = global::One_X.Properties.Resources.flag;
            this.flagIcon.ImageLocation = "";
            this.flagIcon.Location = new System.Drawing.Point(3, 3);
            this.flagIcon.Name = "flagIcon";
            this.flagIcon.Size = new System.Drawing.Size(48, 48);
            this.flagIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.flagIcon.TabIndex = 33;
            this.flagIcon.TabStop = false;
            // 
            // SFlag
            // 
            this.SFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SFlag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SFlag.Location = new System.Drawing.Point(50, 15);
            this.SFlag.Name = "SFlag";
            this.SFlag.Size = new System.Drawing.Size(24, 24);
            this.SFlag.TabIndex = 32;
            this.SFlag.Text = "0";
            this.SFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SFlag.UseCompatibleTextRendering = true;
            // 
            // NU1Flag
            // 
            this.NU1Flag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.NU1Flag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NU1Flag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.NU1Flag.Location = new System.Drawing.Point(188, 15);
            this.NU1Flag.Name = "NU1Flag";
            this.NU1Flag.Size = new System.Drawing.Size(24, 24);
            this.NU1Flag.TabIndex = 31;
            this.NU1Flag.Text = "0";
            this.NU1Flag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NU1Flag.UseCompatibleTextRendering = true;
            // 
            // PFlag
            // 
            this.PFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PFlag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PFlag.Location = new System.Drawing.Point(165, 15);
            this.PFlag.Name = "PFlag";
            this.PFlag.Size = new System.Drawing.Size(24, 24);
            this.PFlag.TabIndex = 30;
            this.PFlag.Text = "0";
            this.PFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PFlag.UseCompatibleTextRendering = true;
            // 
            // NU3Flag
            // 
            this.NU3Flag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.NU3Flag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NU3Flag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.NU3Flag.Location = new System.Drawing.Point(142, 15);
            this.NU3Flag.Name = "NU3Flag";
            this.NU3Flag.Size = new System.Drawing.Size(24, 24);
            this.NU3Flag.TabIndex = 29;
            this.NU3Flag.Text = "0";
            this.NU3Flag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NU3Flag.UseCompatibleTextRendering = true;
            // 
            // ACFlag
            // 
            this.ACFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ACFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ACFlag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ACFlag.Location = new System.Drawing.Point(119, 15);
            this.ACFlag.Name = "ACFlag";
            this.ACFlag.Size = new System.Drawing.Size(24, 24);
            this.ACFlag.TabIndex = 28;
            this.ACFlag.Text = "0";
            this.ACFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ACFlag.UseCompatibleTextRendering = true;
            // 
            // NU5Flag
            // 
            this.NU5Flag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.NU5Flag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NU5Flag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.NU5Flag.Location = new System.Drawing.Point(96, 15);
            this.NU5Flag.Name = "NU5Flag";
            this.NU5Flag.Size = new System.Drawing.Size(24, 24);
            this.NU5Flag.TabIndex = 27;
            this.NU5Flag.Text = "0";
            this.NU5Flag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.NU5Flag.UseCompatibleTextRendering = true;
            // 
            // ZFlag
            // 
            this.ZFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ZFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZFlag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ZFlag.Location = new System.Drawing.Point(73, 15);
            this.ZFlag.Name = "ZFlag";
            this.ZFlag.Size = new System.Drawing.Size(24, 24);
            this.ZFlag.TabIndex = 26;
            this.ZFlag.Text = "0";
            this.ZFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ZFlag.UseCompatibleTextRendering = true;
            // 
            // CFlag
            // 
            this.CFlag.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CFlag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CFlag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CFlag.Location = new System.Drawing.Point(211, 15);
            this.CFlag.Name = "CFlag";
            this.CFlag.Size = new System.Drawing.Size(24, 24);
            this.CFlag.TabIndex = 25;
            this.CFlag.Text = "0";
            this.CFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CFlag.UseCompatibleTextRendering = true;
            // 
            // D7Label
            // 
            this.D7Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D7Label.Location = new System.Drawing.Point(51, 37);
            this.D7Label.Name = "D7Label";
            this.D7Label.Size = new System.Drawing.Size(23, 12);
            this.D7Label.TabIndex = 41;
            this.D7Label.Text = "D₇";
            this.D7Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // D6Label
            // 
            this.D6Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D6Label.Location = new System.Drawing.Point(73, 37);
            this.D6Label.Name = "D6Label";
            this.D6Label.Size = new System.Drawing.Size(24, 12);
            this.D6Label.TabIndex = 40;
            this.D6Label.Text = "D₆";
            this.D6Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // D5Label
            // 
            this.D5Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D5Label.Location = new System.Drawing.Point(96, 37);
            this.D5Label.Name = "D5Label";
            this.D5Label.Size = new System.Drawing.Size(24, 12);
            this.D5Label.TabIndex = 39;
            this.D5Label.Text = "D₅";
            this.D5Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // D4Label
            // 
            this.D4Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D4Label.Location = new System.Drawing.Point(119, 37);
            this.D4Label.Name = "D4Label";
            this.D4Label.Size = new System.Drawing.Size(24, 12);
            this.D4Label.TabIndex = 38;
            this.D4Label.Text = "D₄";
            this.D4Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // D3Label
            // 
            this.D3Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D3Label.Location = new System.Drawing.Point(142, 37);
            this.D3Label.Name = "D3Label";
            this.D3Label.Size = new System.Drawing.Size(24, 12);
            this.D3Label.TabIndex = 37;
            this.D3Label.Text = "D₃";
            this.D3Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // D2Label
            // 
            this.D2Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D2Label.Location = new System.Drawing.Point(165, 37);
            this.D2Label.Name = "D2Label";
            this.D2Label.Size = new System.Drawing.Size(24, 12);
            this.D2Label.TabIndex = 36;
            this.D2Label.Text = "D₂";
            this.D2Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // D1Label
            // 
            this.D1Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D1Label.Location = new System.Drawing.Point(188, 37);
            this.D1Label.Name = "D1Label";
            this.D1Label.Size = new System.Drawing.Size(24, 12);
            this.D1Label.TabIndex = 35;
            this.D1Label.Text = "D₁";
            this.D1Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // D0Label
            // 
            this.D0Label.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.D0Label.Location = new System.Drawing.Point(211, 37);
            this.D0Label.Name = "D0Label";
            this.D0Label.Size = new System.Drawing.Size(24, 12);
            this.D0Label.TabIndex = 34;
            this.D0Label.Text = "D₀";
            this.D0Label.TextAlign = System.Drawing.ContentAlignment.BottomRight;
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
            this.insts.FullRowSelect = true;
            this.insts.Location = new System.Drawing.Point(491, 321);
            this.insts.MultiSelect = false;
            this.insts.Name = "insts";
            this.insts.Size = new System.Drawing.Size(505, 396);
            this.insts.TabIndex = 3;
            this.insts.UseCompatibleStateImageBehavior = false;
            this.insts.View = System.Windows.Forms.View.Details;
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
            this.mnemonicLC.Width = 100;
            // 
            // hexcodeLC
            // 
            this.hexcodeLC.Text = "Hex Code";
            this.hexcodeLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.hexcodeLC.Width = 90;
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
            // startAddressGB
            // 
            this.startAddressGB.Controls.Add(this.startAddressBox);
            this.startAddressGB.Controls.Add(this.setAddressButton);
            this.startAddressGB.Location = new System.Drawing.Point(555, 12);
            this.startAddressGB.Name = "startAddressGB";
            this.startAddressGB.Size = new System.Drawing.Size(146, 66);
            this.startAddressGB.TabIndex = 4;
            this.startAddressGB.TabStop = false;
            this.startAddressGB.Text = "Set Start Address";
            // 
            // startAddressBox
            // 
            this.startAddressBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.startAddressBox.Font = new System.Drawing.Font("Hack", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startAddressBox.Location = new System.Drawing.Point(6, 25);
            this.startAddressBox.Name = "startAddressBox";
            this.startAddressBox.Size = new System.Drawing.Size(62, 34);
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
            this.setAddressButton.Click += new System.EventHandler(this.setAddressButton_Click);
            // 
            // stubColumn
            // 
            this.stubColumn.Text = "";
            this.stubColumn.Width = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.startAddressGB);
            this.Controls.Add(this.insts);
            this.Controls.Add(this.registerPanel);
            this.Controls.Add(this.codePanel);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Assembly Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.codePanel.ResumeLayout(false);
            this.registerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flagIcon)).EndInit();
            this.startAddressGB.ResumeLayout(false);
            this.startAddressGB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox codeBox;
        private System.Windows.Forms.Panel codePanel;
        private System.Windows.Forms.Panel registerPanel;
        private System.Windows.Forms.PictureBox flagIcon;
        private System.Windows.Forms.Label SFlag;
        private System.Windows.Forms.Label NU1Flag;
        private System.Windows.Forms.Label PFlag;
        private System.Windows.Forms.Label NU3Flag;
        private System.Windows.Forms.Label ACFlag;
        private System.Windows.Forms.Label NU5Flag;
        private System.Windows.Forms.Label ZFlag;
        private System.Windows.Forms.Label CFlag;
        private System.Windows.Forms.Label D0Label;
        private System.Windows.Forms.Label D7Label;
        private System.Windows.Forms.Label D6Label;
        private System.Windows.Forms.Label D5Label;
        private System.Windows.Forms.Label D4Label;
        private System.Windows.Forms.Label D3Label;
        private System.Windows.Forms.Label D2Label;
        private System.Windows.Forms.Label D1Label;
        private System.Windows.Forms.Label SFLabel;
        private System.Windows.Forms.Label ZFLabel;
        private System.Windows.Forms.Label ACFLabel;
        private System.Windows.Forms.Label PFLabel;
        private System.Windows.Forms.Label CYFLabel;
        private System.Windows.Forms.ListView insts;
        private System.Windows.Forms.ColumnHeader addressLC;
        private System.Windows.Forms.ColumnHeader mnemonicLC;
        private System.Windows.Forms.ColumnHeader hexcodeLC;
        private System.Windows.Forms.ColumnHeader bytesLC;
        private System.Windows.Forms.ColumnHeader mcycleLC;
        private System.Windows.Forms.ColumnHeader tstateLC;
        private System.Windows.Forms.GroupBox startAddressGB;
        private System.Windows.Forms.TextBox startAddressBox;
        private System.Windows.Forms.Button setAddressButton;
        private System.Windows.Forms.ColumnHeader stubColumn;
    }
}

