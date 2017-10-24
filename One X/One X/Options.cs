using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace One_X {
    public partial class Options : Form {
        public Options() {
            InitializeComponent();
        }
        private bool RunElevated() {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.FileName = Application.ExecutablePath;
            processInfo.Arguments = " -e";
            try {
                Process.Start(processInfo);
                return true;
            } catch (Win32Exception) {
                return false;
            }
        }
        private void assoicateBtn_Click(object sender, EventArgs e) {
            DialogResult dr = MessageBox.Show("The Application needs to start with Administrator rights in order to associate the extension!\n\nMake sure to place the application in a accessible location (e.g. C:\\OneX\\One X.exe) before proceeding since the location needs to be parmanent.\n\nWarning: ALL UNSAVED CHANGES WILL BE LOST!", "Run as Administrator", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            
            if (dr == DialogResult.OK) {
                if (RunElevated()) {
                    Environment.Exit(0);
                }
            }
            
            // AssociateFileExtension(".onex", "ONEX File", "Intel 8085 assembly code with ONEX memory implementation.", Application.ExecutablePath);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private void Options_Load(object sender, EventArgs e) {
            assoicateBtn.FlatStyle = FlatStyle.System;
            SendMessage(assoicateBtn.Handle, 0x0000160C, IntPtr.Zero, (IntPtr)1);
        }
    }
}
