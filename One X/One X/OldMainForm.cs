using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Drawing.Text;
using System.Diagnostics;

namespace One_X {
    public partial class OldMainForm : Form {
        MemoryViewer memView = new MemoryViewer();
        Dispatcher disp = Dispatcher.CurrentDispatcher;

        public OldMainForm() {
            InitializeComponent();
        }

        public PrivateFontCollection pfc = new PrivateFontCollection();

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private void MainForm_Load(object sender, EventArgs e) {
            object[] fontObjects = { AReg, BReg, CReg, DReg, EReg, HReg, LReg, MPoint, PCVal, SPVal, NU1Flag, NU3Flag, NU5Flag, SFlag, ZFlag, ACFlag, PFlag, CYFlag, codeBox, startAddressBox };

            int fontLength = Properties.Resources.Hack.Length;
            byte[] fontdata = Properties.Resources.Hack;

            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontLength, IntPtr.Zero, ref cFonts);

            pfc.AddMemoryFont(data, fontLength);

            Marshal.FreeCoTaskMem(data);

            // todo
            MPU.memory = new Memory(Application.StartupPath + "\\test.bin");

            // codeBox.NumberFont = new Font(pfc.Families[0], 10);

            foreach(var f in fontObjects) {
                if (f is TextBox) {
                    TextBox fd = f as TextBox;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
                if (f is Label) {
                    Label fd = f as Label;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
                if (f is Ionic.WinForms.RichTextBoxEx) {
                    Ionic.WinForms.RichTextBoxEx fd = f as Ionic.WinForms.RichTextBoxEx;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
            }

            codeBox.ShowLineNumbers = true;

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
            // todo set address in parser
            highlight();
        }

        private void highlight() {

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

        private async void nextStepBtn_Click(object sender, EventArgs e) => await Task.Run(() => MPU.NextStep());

        private async void execButton_Click(object sender, EventArgs e) => await Task.Run(() => MPU.ExecuteAllSteps());

        private void stopBtn_Click(object sender, EventArgs e) {
            MPU.Stop();
        }
    }
}
