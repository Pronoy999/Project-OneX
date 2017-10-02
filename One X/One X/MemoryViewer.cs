using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace One_X {
    public partial class MemoryViewer : Form {
        public MemoryViewer() {
            InitializeComponent();
        }

        private void MemoryViewer_Load(object sender, EventArgs e) {
            memBox.Font = Fonts.Fonts.Create(Fonts.FontFamily.Hack, 12);
            memBox.ByteProvider = MPU.memory.provider;
        }

        private void MemoryViewer_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            Hide();
        }
    }
}
