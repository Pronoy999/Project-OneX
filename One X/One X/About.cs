using System.Windows.Forms;

namespace One_X {
    public partial class About : Form {
        public About() {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            sourceCode.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/Pronoy999/Project-OneX");
        }

        private void reportBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            reportBug.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/Pronoy999/Project-OneX/issues");
        }
    }
}
