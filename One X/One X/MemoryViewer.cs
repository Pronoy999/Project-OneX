﻿using Be.Windows.Forms;
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

namespace One_X {
    public partial class MemoryViewer : Form {
        private StickyWindow window;

        public MemoryViewer() {
            InitializeComponent();
            window = new StickyWindow(this);
        }

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public PrivateFontCollection pfc = new PrivateFontCollection();

        private void MemoryViewer_Load(object sender, EventArgs e) {
            int fontLength = Properties.Resources.Hack.Length;
            byte[] fontdata = Properties.Resources.Hack;

            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);

            uint cFonts = 0;
            AddFontMemResourceEx(data, (uint)fontLength, IntPtr.Zero, ref cFonts);

            pfc.AddMemoryFont(data, fontLength);

            Marshal.FreeCoTaskMem(data);

            Control[] fontObjects = { memBox, offset };
            
            foreach (var f in fontObjects) {
                f.Font = new Font(pfc.Families[0], f.Font.Size);
            }
            
            memBox.ByteProvider = MPU.memory.provider;
            MPU.memory.ProviderChanged += (s, ev) =>  memBox.ByteProvider = MPU.memory.provider;

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

        public void InvalidateMemory() {
            if (memBox.ByteProvider != null) {
                memBox.Invalidate();
            }
        }

        private void memList_MouseDown(object sender, MouseEventArgs e) {
            if (e.Clicks == 2) {
                if (memList.SelectedIndices.Count == 1) {
                    editItem(sender, e);
                } else {
                    addItem(sender, e);
                } 
            }
        }

        private void memEditMenu_Opening(object sender, CancelEventArgs e) {
            if (memList.SelectedIndices.Count > 0) {
                editToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
            } else {
                editToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
            }
        }

        private void addItem(object sender, EventArgs e) {
            MemoryInsertForm minform = new MemoryInsertForm();

            if (minform.ShowDialog() == DialogResult.OK) {
                ListViewItem litem = new ListViewItem(new string[] { minform.addr_box.Text, minform.val_box.Text });
                litem.Checked = minform.pinMemBox.Checked;
                memList.Items.Add(litem);
            }

            // todo set value
        }

        private void editItem(object sender, EventArgs e) {
            ListViewItem litem = memList.SelectedItems[0];

            MemoryInsertForm minform = new MemoryInsertForm();
            minform.Text = "Edit Memory";
            minform.addr_box.Text = litem.Text;
            minform.val_box.Text = litem.SubItems[1].Text;
            minform.pinMemBox.Checked = litem.Checked;

            if (minform.ShowDialog() == DialogResult.OK) {
                litem.Text = minform.addr_box.Text;
                litem.SubItems[1].Text = minform.val_box.Text;
                litem.Checked = minform.pinMemBox.Checked;
            }

            // todo set value
        }

        private void deleteItem(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to delete the selected memory?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) {
                ListViewItem litem = memList.SelectedItems[0];
                memList.Items.Remove(litem);
            }

            // todo set value
        }

        private void memList_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete && memList.SelectedIndices.Count > 0) {
                deleteItem(sender, e);
            }
        }
    }
}
