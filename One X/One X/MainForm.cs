using System;
using System.Linq;
using System.Windows.Forms;

namespace One_X {
    public partial class MainForm : Form {
        Parser p = new Parser();
        bool doHigh = true;

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            highTimer.Start();
        }

        private void codeBox_KeyUp(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
                doHigh = false;
                highlight();
            }
        }

        private void startAddressBox_KeyPress(object sender, KeyPressEventArgs e) {
            char c = e.KeyChar;
            if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'))) {
                e.Handled = true;
                return;
            }
            var x = sender as TextBox;
            if (x.TextLength == 4) {
                x.Text = x.Text.Substring(1);
                x.SelectionStart = x.TextLength;
            }
        }

        private void setAddressButton_Click(object sender, EventArgs e) {
            p = new Parser(int.Parse(startAddressBox.Text, System.Globalization.NumberStyles.HexNumber));
            MessageBox.Show(int.Parse(startAddressBox.Text, System.Globalization.NumberStyles.HexNumber) + "");
            p.Parse(codeBox.Text);
        }

        private void highlight() {
            var highs = p.Parse(codeBox.Text);
            int i = highs.First().ColIndex;
            foreach (var h in highs) {
                
            }
        }

        private void highTimer_Tick(object sender, EventArgs e) {
            if (doHigh) highlight(); else doHigh = true;
        }
    }
}
