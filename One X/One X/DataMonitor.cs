using Blue.Windows;
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
    public partial class DataMonitor : Form {
        private StickyWindow window;

        public Dispatcher dispatcher = Dispatcher.CurrentDispatcher;

        public PrivateFontCollection pfc = new PrivateFontCollection();

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        
        public DataMonitor() {
            InitializeComponent();
            window = new StickyWindow(this);
        }

        private void DataMonitor_Load(object sender, EventArgs e) {
            Control[] fontObjects = { monitorView };

            int fontLength = Properties.Resources.Hack.Length;
            byte[] fontdata = Properties.Resources.Hack;

            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontLength, IntPtr.Zero, ref cFonts);

            pfc.AddMemoryFont(data, fontLength);

            Marshal.FreeCoTaskMem(data);

            foreach (var f in fontObjects) {
                f.Font = new Font(pfc.Families[0], f.Font.Size);
            }
        }
    }
}
