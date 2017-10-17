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
using System.Windows.Threading;

namespace One_X {
    public partial class Assembler : Form {
        public Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        public PrivateFontCollection pfc = new PrivateFontCollection();

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        public Assembler() {
            InitializeComponent();
        }

        private void Assembler_Load(object sender, EventArgs e) {
            object[] fontObjects = { startAddressBox };

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
            }
        }
    }
}
