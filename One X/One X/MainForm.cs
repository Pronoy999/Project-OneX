using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace One_X {
    public partial class MainForm : Form {
        public PrivateFontCollection pfc = new PrivateFontCollection();

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            object[] fontObjects = { codeBox };

            int fontLength = Properties.Resources.Hack.Length;
            byte[] fontdata = Properties.Resources.Hack;

            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontLength, IntPtr.Zero, ref cFonts);

            pfc.AddMemoryFont(data, fontLength);

            Marshal.FreeCoTaskMem(data);

            foreach (var f in fontObjects) {
                if (f is TextBox) {
                    TextBox fd = f as TextBox;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
                if (f is Label) {
                    Label fd = f as Label;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
                if (f is FastColoredTextBox) {
                    FastColoredTextBox fd = f as FastColoredTextBox;
                    fd.Font = new Font(pfc.Families[0], fd.Font.Size);
                }
            }
        }

        // todo define global static / settings
        Style mnemonicStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        Style labelStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        Style literalStyle = new TextStyle(Brushes.Orange, null, FontStyle.Regular);

        private void codeBox_TextChanged(object sender, TextChangedEventArgs e) {
            e.ChangedRange.ClearStyle(mnemonicStyle, labelStyle, literalStyle);

            e.ChangedRange.SetStyle(mnemonicStyle, RegexHelper.rxRangeOneByte);
            e.ChangedRange.SetStyle(mnemonicStyle, RegexHelper.rxRangeTwoByte);
            e.ChangedRange.SetStyle(mnemonicStyle, RegexHelper.rxRangeThreeByte);

            e.ChangedRange.SetStyle(labelStyle, RegexHelper.rxRangeLabelOnly);
            e.ChangedRange.SetStyle(labelStyle, RegexHelper.rxRangeReference);

            e.ChangedRange.SetStyle(literalStyle, RegexHelper.rxRangeLiteralByte);
            e.ChangedRange.SetStyle(literalStyle, RegexHelper.rxRangeLiteralUShort);
        }
    }
}
