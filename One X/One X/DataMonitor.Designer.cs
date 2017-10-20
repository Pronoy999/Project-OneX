namespace One_X {
    partial class DataMonitor {
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
            this.monitorView = new System.Windows.Forms.ListView();
            this.aCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stepCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // monitorView
            // 
            this.monitorView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.stepCol,
            this.aCol,
            this.bCol,
            this.cCol,
            this.dCol,
            this.eCol,
            this.hCol,
            this.lCol,
            this.mCol,
            this.flCol});
            this.monitorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorView.Location = new System.Drawing.Point(0, 0);
            this.monitorView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.monitorView.Name = "monitorView";
            this.monitorView.Size = new System.Drawing.Size(624, 441);
            this.monitorView.TabIndex = 0;
            this.monitorView.UseCompatibleStateImageBehavior = false;
            this.monitorView.View = System.Windows.Forms.View.Details;
            // 
            // aCol
            // 
            this.aCol.Text = "A";
            this.aCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.aCol.Width = 48;
            // 
            // bCol
            // 
            this.bCol.Text = "B";
            this.bCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bCol.Width = 48;
            // 
            // cCol
            // 
            this.cCol.Text = "C";
            this.cCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cCol.Width = 48;
            // 
            // dCol
            // 
            this.dCol.Text = "D";
            this.dCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dCol.Width = 48;
            // 
            // eCol
            // 
            this.eCol.Text = "E";
            this.eCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.eCol.Width = 48;
            // 
            // hCol
            // 
            this.hCol.Text = "H";
            this.hCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hCol.Width = 48;
            // 
            // lCol
            // 
            this.lCol.Text = "L";
            this.lCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lCol.Width = 48;
            // 
            // mCol
            // 
            this.mCol.Text = "M*";
            this.mCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mCol.Width = 48;
            // 
            // flCol
            // 
            this.flCol.Text = "S : Z : AC : P : CY";
            this.flCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.flCol.Width = 172;
            // 
            // stepCol
            // 
            this.stepCol.Text = "STEP";
            this.stepCol.Width = 64;
            // 
            // DataMonitor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.monitorView);
            this.Font = new System.Drawing.Font("Trebuchet MS", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DataMonitor";
            this.Text = "DataMonitor";
            this.Load += new System.EventHandler(this.DataMonitor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView monitorView;
        private System.Windows.Forms.ColumnHeader aCol;
        private System.Windows.Forms.ColumnHeader bCol;
        private System.Windows.Forms.ColumnHeader cCol;
        private System.Windows.Forms.ColumnHeader dCol;
        private System.Windows.Forms.ColumnHeader eCol;
        private System.Windows.Forms.ColumnHeader hCol;
        private System.Windows.Forms.ColumnHeader lCol;
        private System.Windows.Forms.ColumnHeader mCol;
        private System.Windows.Forms.ColumnHeader flCol;
        private System.Windows.Forms.ColumnHeader stepCol;
    }
}