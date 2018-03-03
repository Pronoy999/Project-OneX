namespace One_X {
    partial class MemoryInsertForm {
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
            this.addr_box = new System.Windows.Forms.TextBox();
            this.val_box = new System.Windows.Forms.TextBox();
            this.addr_label = new System.Windows.Forms.Label();
            this.val_label = new System.Windows.Forms.Label();
            this.pinMemBox = new System.Windows.Forms.CheckBox();
            this.ok_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addr_box
            // 
            this.addr_box.Location = new System.Drawing.Point(85, 12);
            this.addr_box.Name = "addr_box";
            this.addr_box.Size = new System.Drawing.Size(100, 20);
            this.addr_box.TabIndex = 0;
            // 
            // val_box
            // 
            this.val_box.Location = new System.Drawing.Point(85, 38);
            this.val_box.Name = "val_box";
            this.val_box.Size = new System.Drawing.Size(100, 20);
            this.val_box.TabIndex = 1;
            // 
            // addr_label
            // 
            this.addr_label.AutoSize = true;
            this.addr_label.Location = new System.Drawing.Point(12, 15);
            this.addr_label.Name = "addr_label";
            this.addr_label.Size = new System.Drawing.Size(51, 13);
            this.addr_label.TabIndex = 2;
            this.addr_label.Text = "Address :";
            // 
            // val_label
            // 
            this.val_label.AutoSize = true;
            this.val_label.Location = new System.Drawing.Point(12, 41);
            this.val_label.Name = "val_label";
            this.val_label.Size = new System.Drawing.Size(40, 13);
            this.val_label.TabIndex = 3;
            this.val_label.Text = "Value :";
            // 
            // pinMemBox
            // 
            this.pinMemBox.AutoSize = true;
            this.pinMemBox.Location = new System.Drawing.Point(198, 14);
            this.pinMemBox.Name = "pinMemBox";
            this.pinMemBox.Size = new System.Drawing.Size(41, 17);
            this.pinMemBox.TabIndex = 4;
            this.pinMemBox.Text = "Pin";
            this.pinMemBox.UseVisualStyleBackColor = true;
            // 
            // ok_btn
            // 
            this.ok_btn.Location = new System.Drawing.Point(255, 12);
            this.ok_btn.Name = "ok_btn";
            this.ok_btn.Size = new System.Drawing.Size(53, 46);
            this.ok_btn.TabIndex = 5;
            this.ok_btn.Text = "OK";
            this.ok_btn.UseVisualStyleBackColor = true;
            this.ok_btn.Click += new System.EventHandler(this.ok_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_btn.Location = new System.Drawing.Point(191, 38);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(58, 20);
            this.cancel_btn.TabIndex = 6;
            this.cancel_btn.Text = "CANCEL";
            this.cancel_btn.UseVisualStyleBackColor = true;
            // 
            // MemoryInsertForm
            // 
            this.AcceptButton = this.ok_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_btn;
            this.ClientSize = new System.Drawing.Size(320, 69);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.ok_btn);
            this.Controls.Add(this.pinMemBox);
            this.Controls.Add(this.val_label);
            this.Controls.Add(this.addr_label);
            this.Controls.Add(this.val_box);
            this.Controls.Add(this.addr_box);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MemoryInsertForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Insert Memory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox addr_box;
        public System.Windows.Forms.TextBox val_box;
        private System.Windows.Forms.Label addr_label;
        private System.Windows.Forms.Label val_label;
        public System.Windows.Forms.CheckBox pinMemBox;
        private System.Windows.Forms.Button ok_btn;
        private System.Windows.Forms.Button cancel_btn;
    }
}