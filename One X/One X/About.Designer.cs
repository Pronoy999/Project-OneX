namespace One_X {
    partial class About {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.license = new System.Windows.Forms.TextBox();
            this.title = new System.Windows.Forms.Label();
            this.descTitle = new System.Windows.Forms.Label();
            this.sourceCode = new System.Windows.Forms.LinkLabel();
            this.reportBug = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // iconBox
            // 
            this.iconBox.Image = global::One_X.Properties.Resources.icon;
            this.iconBox.Location = new System.Drawing.Point(12, 12);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(460, 258);
            this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconBox.TabIndex = 0;
            this.iconBox.TabStop = false;
            // 
            // license
            // 
            this.license.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.license.Font = new System.Drawing.Font("Trebuchet MS", 10F);
            this.license.Location = new System.Drawing.Point(12, 325);
            this.license.Multiline = true;
            this.license.Name = "license";
            this.license.ReadOnly = true;
            this.license.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.license.Size = new System.Drawing.Size(460, 197);
            this.license.TabIndex = 1;
            this.license.TabStop = false;
            this.license.Text = resources.GetString("license.Text");
            // 
            // title
            // 
            this.title.Font = new System.Drawing.Font("Hack", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(15, 273);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(457, 30);
            this.title.TabIndex = 2;
            this.title.Text = "Project One X";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // descTitle
            // 
            this.descTitle.Font = new System.Drawing.Font("Hack", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descTitle.Location = new System.Drawing.Point(12, 303);
            this.descTitle.Name = "descTitle";
            this.descTitle.Size = new System.Drawing.Size(460, 19);
            this.descTitle.TabIndex = 3;
            this.descTitle.Text = "Intel 8085 Simulator";
            this.descTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sourceCode
            // 
            this.sourceCode.AutoSize = true;
            this.sourceCode.Location = new System.Drawing.Point(290, 525);
            this.sourceCode.Name = "sourceCode";
            this.sourceCode.Size = new System.Drawing.Size(182, 20);
            this.sourceCode.TabIndex = 4;
            this.sourceCode.TabStop = true;
            this.sourceCode.Text = "VIEW SOURCE ON GITHUB";
            this.sourceCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // reportBug
            // 
            this.reportBug.AutoSize = true;
            this.reportBug.Location = new System.Drawing.Point(12, 525);
            this.reportBug.Name = "reportBug";
            this.reportBug.Size = new System.Drawing.Size(106, 20);
            this.reportBug.TabIndex = 5;
            this.reportBug.TabStop = true;
            this.reportBug.Text = "REPORT A BUG";
            this.reportBug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.reportBug_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(484, 554);
            this.Controls.Add(this.reportBug);
            this.Controls.Add(this.sourceCode);
            this.Controls.Add(this.descTitle);
            this.Controls.Add(this.title);
            this.Controls.Add(this.license);
            this.Controls.Add(this.iconBox);
            this.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconBox;
        private System.Windows.Forms.TextBox license;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label descTitle;
        private System.Windows.Forms.LinkLabel sourceCode;
        private System.Windows.Forms.LinkLabel reportBug;
    }
}