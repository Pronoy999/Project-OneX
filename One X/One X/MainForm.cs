using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace One_X {
    public partial class MainForm : Form {
        Parser p = new Parser();
        MemoryViewer memView = new MemoryViewer();

        public MainForm() {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private void MainForm_Load(object sender, EventArgs e) {
            // todo
            MPU.memory = new Memory(Application.StartupPath + "\\test.bin");

            codeBox.Font = Fonts.Fonts.Create(Fonts.FontFamily.Hack, 12);
            codeBox.NumberFont = Fonts.Fonts.Create(Fonts.FontFamily.Hack, 10);
            codeBox.ShowLineNumbers = true;
            startAddressBox.Font = Fonts.Fonts.Create(Fonts.FontFamily.Hack, 17);

            startAddressBox.GotFocus += (sndr, args) => {
                startAddressBox.Select(startAddressBox.TextLength, 0);
                HideCaret(startAddressBox.Handle);
            };
        }

        private void codeBox_KeyUp(object sender, KeyEventArgs e) {
            if(e.KeyCode == Keys.Enter) {
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
            p = new Parser(ushort.Parse(startAddressBox.Text, System.Globalization.NumberStyles.HexNumber));
            highlight();
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
                switch(word.SType) {
                    case Parser.StringType.Mnemonic:
                        try {
                            if (highs[i - 1].LineIndex != word.LineIndex) {
                                var stre = pad("", ls) + "     ";
                                count += stre.Length;
                                codeBoxText += stre;
                            }
                        } catch {
                            var stre = pad("", ls) + "     ";
                            count += stre.Length;
                            codeBoxText += stre;
                        }
                        break;
                    case Parser.StringType.Label:
                        try {
                            if (highs[i - 1].LineIndex != word.LineIndex) {
                                // left label
                                str = pad(str, ls) + ":    ";
                            }
                        } catch {
                            str = pad(str, ls) + ":    ";
                        }
                        break;
                    default:
                        break;
                }
                
                x.Add((word.SType, count, word.Word.Length));
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
                codeBox.SetSelectionColor(w.begin, w.begin + w.length, Color.FromArgb((int)w.stype));
            }

            // restore selection
            codeBox.DeselectAll();
            codeBox.Select(codeBox.TextLength, 0);

            try {
                ushort start = ushort.Parse(insts.Items[0].SubItems[1].Text);
                ushort end = ushort.Parse(insts.Items[insts.Items.Count - 1].SubItems[1].Text);
                MPU.memory.Clear(start, end);
            } catch (ArgumentOutOfRangeException ex) { }
            
            insts.Items.Clear();
            foreach (var ins in p.instructions) {
                ListViewItem litem = new ListViewItem(new string[] {
                    string.Empty,
                    ins.Key.ToString("X4"),
                    ins.Value.Name + (ins.Value.Bytes > 1 ? 
                    (ins.Value.Name.Contains(" ") ? "," : " ") + 
                    ins.Value.Arguments.ToUShort().ToString(ins.Value.Bytes > 2 ? "X4" : "X2") + "H" : string.Empty),
                    ((byte)ins.Value.GetOPCODE()).ToString("X2"),
                    ins.Value.Bytes.ToString(),
                    ins.Value.MCycles.ToString(),
                    ins.Value.TStates.ToString()
                });
                insts.Items.Add(litem);
                if (ins.Value.Bytes > 1) {
                    ListViewItem litemLO = new ListViewItem(new string[] {
                        string.Empty,
                        ((ushort)(ins.Key + 1)).ToString("X4"),
                        string.Empty,
                        ins.Value.Arguments.LO.ToString("X2")
                    });
                    insts.Items.Add(litemLO);
                    if (ins.Value.Bytes > 2) {
                        ListViewItem litemHO = new ListViewItem(new string[] {
                            string.Empty,
                            ((ushort)(ins.Key + 2)).ToString("X4"),
                            string.Empty,
                            ins.Value.Arguments.HO.ToString("X2")
                        });
                        insts.Items.Add(litemHO);
                    }
                }
                ins.Value.WriteToMemory(MPU.memory, ins.Key);
            }
            memView.memBox.Invalidate(); ;
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

        private void memBtn_Click(object sender, EventArgs e) {
            if (memView.Visible) {
                memView.Hide();
            } else {
                memView.Show();
            }
            memView.Location = new Point(Location.X + Width, Location.Y);
        }

        private void MainForm_Move(object sender, EventArgs e) {
            memView.Location = new Point(Location.X + Width, Location.Y);
        }
    }
}
