using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace One_X {
    public partial class MemoryViewer : Form {
        public MemoryViewer() {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        public PrivateFontCollection pfc = new PrivateFontCollection();

        private void MemoryViewer_Load(object sender, EventArgs e) {
            int fontLength = Properties.Resources.Hack.Length;
            byte[] fontdata = Properties.Resources.Hack;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            object[] fontObjects = { memBox, offset };

            foreach (var f in fontObjects) {
                if (f is TextBox) {
                    TextBox fd = f as TextBox;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
                if (f is Label) {
                    Label fd = f as Label;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
                if (f is HexBox) {
                    HexBox fd = f as HexBox;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
            }

            memBox.Font = Fonts.Fonts.Create(Fonts.FontFamily.Hack, 12);
            memBox.ByteProvider = MPU.memory.provider;

            offset.Font = Fonts.Fonts.Create(Fonts.FontFamily.Hack, 12);

            offset.GotFocus += (sndr, args) => {
                offset.Select(offset.TextLength, 0);
                HideCaret(offset.Handle);
            };
        }

        private void MemoryViewer_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }

        private void offset_KeyPress(object sender, KeyPressEventArgs e) {
            char c = e.KeyChar;
            if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'))) {
                e.Handled = true;
                if (c == (char)Keys.Return) {
                    gotoBtn.PerformClick();
                }
                return;
            }
            var x = sender as TextBox;
            if (x.TextLength == 4) {
                x.Text = x.Text.Substring(1);
                x.SelectionStart = x.TextLength;
            }
        }

        private void memBox_SelectionStartChanged(object sender, EventArgs e) {
            offset.Text = memBox.SelectionStart.ToString("X4");
        }

        private void gotoBtn_Click(object sender, EventArgs e) {
            memBox.Select(long.Parse(offset.Text, System.Globalization.NumberStyles.HexNumber), 0);
            memBox.Focus();
        }
    }
}
