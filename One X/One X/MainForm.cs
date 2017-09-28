using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace One_X {
    public partial class MainForm : Form {
        Parser p = new Parser();
        bool doHigh = true;

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            //highTimer.Start();
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
            p.Parse(codeBox.Text);
        }

        private void highlight() {
            var highs = p.Parse(codeBox.Text);

            var ls = padLen(p.labels.Values);

            string codeBoxText = string.Empty;

            var x = new List<(Parser.StringType stype, int begin, int length)>();

            int count = 0;

            for (int i = 0; i < highs.Count; i++) {
                var word = highs[i];
                var str = word.Word;
                if (word.SType == Parser.StringType.Label) {
                    if (highs[i + 1].LineIndex == word.LineIndex) {
                        // left label
                        str = pad(str, ls) + ":    ";
                    }
                }
                
                x.Add((word.SType, count, str.Length));
                count += str.Length;
                codeBoxText += str;
                try {
                    if (highs[i + 1].LineIndex != word.LineIndex) {
                        codeBoxText += "\n";
                        count++;
                    }
                } catch {
                    codeBoxText += "\n";
                    count++;
                }
            }

            codeBox.Clear();
            codeBox.Text = codeBoxText;

            // color
            foreach (var w in x) {
                codeBox.Select(w.begin, w.length);
                codeBox.SelectionColor = Color.FromArgb((int)w.stype);
            }

            // restore selection

            codeBox.DeselectAll();
            codeBox.Select(codeBox.TextLength, 0);
        }

        private void highTimer_Tick(object sender, EventArgs e) {
            if (doHigh) highlight(); else doHigh = true;
        }

        static string pad(string s, int i) {
            string p = string.Empty;
            while (p.Length + s.Length < i) {
                p += " ";
            }
            return s + p;
        }

        static int padLen(IEnumerable<string> labels) {
            int r = 4;
            foreach (string p in labels) {
                if (p.Length >= r) {
                    r = 4 * ((p.Length / 4) + 1);
                }
            }
            return r;
        }
    }
}
