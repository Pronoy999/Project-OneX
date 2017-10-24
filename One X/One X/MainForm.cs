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
using System.IO;
using Blue.Windows;

namespace One_X {
    public partial class MainForm : Form {
        private MemoryViewer memView = new MemoryViewer();
        private Assembler assembler = new Assembler();
        private Executer executer = new Executer();
        private DataMonitor monitor = new DataMonitor();

        private string codeFileName = string.Empty;
        private string saveFileName = string.Empty;

        private Parser parser;

        private bool saved = false;
        public PrivateFontCollection pfc = new PrivateFontCollection();

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public MainForm() {
            InitializeComponent();
            StickyWindow.RegisterExternalReferenceForm(this);
        }

        private void MainForm_Load(object sender, EventArgs e) {
            Size = MinimumSize;

            MenuItem[] notImp = { updateMI, aboutMI, optionsMI };
            Control[] fontObjects = { codeBox };

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

            // codeBox.AutoIndentCharsPatterns = "^\\s*[\\w]+\\s*(?<range>:)\\s*(?<range>[^\\n]+)$";

            foreach (var i in notImp) {
                i.Click += (s, ev) => MessageBox.Show("NOT IMPLEMENTED YET!");
            }
            Debug.WriteLine(Application.UserAppDataPath + "\\currentfile");
            New();

            parser = new Parser(0);
            MPU.ValueChanged += ValueChanged;
            MPU.Step += Step;

            //memeditMI.PerformClick();
            execMI.PerformClick();
            assemblerMI.PerformClick();
            datamoniMI.PerformClick();
        }

        // todo define global static / settings
        // todo define comment style
        Style mnemonicStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        Style labelStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        Style literalStyle = new TextStyle(Brushes.Orange, null, FontStyle.Regular);

        private void codeBox_TextChanged(object sender, TextChangedEventArgs e) {
            e.ChangedRange.ClearStyle(mnemonicStyle, labelStyle, literalStyle);

            e.ChangedRange.SetStyle(mnemonicStyle, RegexHelper.rxRangeOneByte);
            e.ChangedRange.SetStyle(mnemonicStyle, RegexHelper.rxRangeTwoByte);
            e.ChangedRange.SetStyle(mnemonicStyle, RegexHelper.rxRangeThreeByte);

            e.ChangedRange.SetStyle(literalStyle, RegexHelper.rxRangeLiteralByte);
            e.ChangedRange.SetStyle(literalStyle, RegexHelper.rxRangeLiteralUShort);

            e.ChangedRange.SetStyle(labelStyle, RegexHelper.rxRangeLabelOnly);
            e.ChangedRange.SetStyle(labelStyle, RegexHelper.rxRangeReference);


            UpdateModifiedInfo();
        }

        private void codeBox_AutoIndentNeeded(object sender, AutoIndentEventArgs e) {
            //int lpos = e.PrevLineText.IndexOf(":");
            //if (lpos > 0) {
            //    e.AbsoluteIndentation = lpos + 2;
            //}
            //if (e.LineText.Contains(":")) {
            //    e.AbsoluteIndentation = 0;
            //}
        }

        private void cutMI_Click(object sender, EventArgs e) {
            codeBox.Cut();
            UpdateModifiedInfo();
        }

        private void copyMI_Click(object sender, EventArgs e) => codeBox.Copy();

        private void pasteMI_Click(object sender, EventArgs e) {
            codeBox.Paste();
            UpdateModifiedInfo();
        }

        private void undoMI_Click(object sender, EventArgs e) {
            codeBox.Undo();
            UpdateModifiedInfo();
        }

        private void redoMI_Click(object sender, EventArgs e) {
            codeBox.Redo();
            UpdateModifiedInfo();
        }

        private void selectallMI_Click(object sender, EventArgs e) => codeBox.SelectAll();

        private void zoominMI_Click(object sender, EventArgs e) => codeBox.Zoom += 10;

        private void zoomoutMI_Click(object sender, EventArgs e) => codeBox.Zoom -= 10;

        private void resetzoomMI_Click(object sender, EventArgs e) => codeBox.Zoom = 100;

        private void findMI_Click(object sender, EventArgs e) => codeBox.ShowFindDialog();

        private void replaceMI_Click(object sender, EventArgs e) => codeBox.ShowReplaceDialog();

        private void gotoMI_Click(object sender, EventArgs e) => codeBox.ShowGoToDialog();

        private void findnextMI_Click(object sender, EventArgs e) {
            try {
                codeBox.findForm.FindNext(codeBox.findForm.tbFind.Text);
            } catch (NullReferenceException) {
                codeBox.ShowFindDialog();
            }
        }

        private void ucaseMI_Click(object sender, EventArgs e) {
            codeBox.UpperCase();
            UpdateModifiedInfo();
        }

        private void UpdateModifiedInfo() {
            codeBox.IsChanged = true;
            modifiedinfo.Text = (string.IsNullOrWhiteSpace(saveFileName) ? "Untitled" : Path.GetFileName(saveFileName)) + " - *Unsaved Changes*";
        }

        private void lcaseMI_Click(object sender, EventArgs e) {
            codeBox.LowerCase();
            UpdateModifiedInfo();
        }

        private void cmntMI_Click(object sender, EventArgs e) {
            codeBox.CommentSelected();
            UpdateModifiedInfo();
        }

        private void dellineMI_Click(object sender, EventArgs e) {
            codeBox.ClearCurrentLine();
            UpdateModifiedInfo();
        }

        private void exitMI_Click(object sender, EventArgs e) => SaveAndExit();

        private void SaveAndExit() {
            if (!codeBox.IsChanged) {
                Application.Exit();
                return;
            }
            DialogResult dr = MessageBox.Show("Warning: Unsaved changes!\nSave changes to " + (string.IsNullOrWhiteSpace(saveFileName) ? "new source file" : saveFileName) + "?", "Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            switch (dr) {
                case DialogResult.No:
                    saved = true;
                    break;
                case DialogResult.Yes:
                    if (string.IsNullOrWhiteSpace(saveFileName)) {
                        if (saveFile.ShowDialog() == DialogResult.OK) {
                            SaveFile(saveFile.FileName);
                            saved = true;
                            Application.Exit();
                            return;
                        }
                        saved = false;
                    } else {
                        SaveFile(saveFileName);
                        saved = true;
                        Application.Exit();
                    }
                    return;
                default:
                    saved = false;
                    return;
            }
            Application.Exit();
        }

        private void SaveAndClose() {
            if (!codeBox.IsChanged) { CloseFile(); return; }
            DialogResult dr = MessageBox.Show("Warning: Unsaved changes!\nSave changes to " + (string.IsNullOrWhiteSpace(saveFileName) ? "new source file" : saveFileName) + "?", "Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            switch (dr) {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    if (string.IsNullOrWhiteSpace(saveFileName)) {
                        if (saveFile.ShowDialog() == DialogResult.OK) {
                            SaveFile(saveFile.FileName);
                            CloseFile();
                        }
                    } else {
                        SaveFile(saveFileName);
                        CloseFile();
                    }
                    return;
                default:
                    return;
            }
            CloseFile();
        }

        private void CloseFile() {
            codeBox.Clear();
            string dir = Application.UserAppDataPath + "\\currentfile";
            codeFileName = dir + "\\code";
            codeBox.IsChanged = false;
            codeBox.Text = "";
            modifiedinfo.Text = "";
            try {
                memView.Close();
                memView.Dispose();
            } catch { }
            try { MPU.CommitMemory(); Directory.Delete(dir, true); } catch (IOException) { } catch (NullReferenceException) { }

            codeBox.Visible = false;
        }
        private void closeMI_Click(object sender, EventArgs e) => SaveAndClose();

        private void codeBox_SelectionChanged(object sender, EventArgs e) {
            lineinfo.Text = "Line: " + codeBox.Selection.Bounds.iStartLine;
            columninfo.Text = "Column: " + codeBox.Selection.Bounds.iStartChar;
            locinfo.Text = "Location: " + codeBox.SelectionStart;
        }

        private void codeBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Insert) {
                insrepinfo.Text = codeBox.IsReplaceMode ? "REPLACE" : "INSERT";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!saved) {
                SaveAndExit();
            }
        }

        private void openMI_Click(object sender, EventArgs e) {
            if (openFile.ShowDialog() == DialogResult.OK) {
                SaveAndClose();
                try {
                    memView.Close();
                    memView.Dispose();
                } catch { }
                MPU.CommitMemory();
                if (OneXFile.ExtractOneXFile(openFile.FileName, "currentfile")) {
                    saveFileName = openFile.FileName;
                    string dir = Application.UserAppDataPath + "\\currentfile";
                    codeFileName = dir + "\\code";
                    codeBox.OpenFile(codeFileName, Encoding.UTF8);
                    MPU.InitMemory(dir + "\\memory");
                    memView = new MemoryViewer();
                    codeBox.IsChanged = false;
                    parse(true);
                    modifiedinfo.Text = Path.GetFileName(saveFileName) + " - *No Changes*";

                    codeBox.Visible = true;
                } else {
                    MessageBox.Show("The selected *.onex file is in invalid format!", "Invalid format!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CloseFile();
                }
            }
        }

        private void saveasMI_Click(object sender, EventArgs e) {
            if (saveFile.ShowDialog() == DialogResult.OK) {
                SaveFile(saveFile.FileName);
            }
        }

        private void SaveFile(string name) {
            if (string.IsNullOrWhiteSpace(name)) {
                saveasMI.PerformClick(); return;
            }
            saveFileName = name;
            string dir = Application.UserAppDataPath + "\\currentfile";
            codeFileName = dir + "\\code";
            codeBox.SaveToFile(codeFileName, Encoding.UTF8);
            try {
                memView.Close();
                memView.Dispose();
            } catch { }
            MPU.CommitMemory();
            OneXFile.RepackOneXFile(name, "currentfile");
            MPU.InitMemory(dir + "\\memory");
            memView = new MemoryViewer();
            // commit changes (reset all dirty flags)
            codeBox.IsChanged = false;
            parse(true);
            modifiedinfo.Text = Path.GetFileName(saveFileName) + " - *Changes Saved*";
        }

        private void saveMI_Click(object sender, EventArgs e) {
            SaveFile(saveFileName);
        }

        private void New() {
            SaveAndClose();
            codeBox.Clear();
            string dir = Application.UserAppDataPath + "\\currentfile";
            codeFileName = dir + "\\code";
            modifiedinfo.Text = "Untitled - *No Changes*";
            try {
                memView.Close();
                memView.Dispose();
            } catch { }
            try { MPU.CommitMemory(); } catch (NullReferenceException) { }
            try { Directory.Delete(dir, true); } catch (IOException) { }
            Directory.CreateDirectory(dir);
            MPU.InitMemory(dir + "\\memory");
            memView = new MemoryViewer();
            codeBox.IsChanged = false;
            parse(true);
            codeBox.Visible = true;
        }

        private void newMI_Click(object sender, EventArgs e) {
            New();
        }

        private void parseTimer_Tick(object sender, EventArgs e) {
            parse();
        }

        private void memeditMI_Click(object sender, EventArgs e) {
            if (memView.Visible) {
                memView.Hide();
            } else {
                memView.Show();
            }
            if (IsOnScreen(new Point(Location.X + Width + 20, Location.Y - 20))) {
                memView.Location = new Point(Location.X + Width, Location.Y);
            }
        }

        public bool IsOnScreen(Point pt) {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens) {
                if (screen.WorkingArea.Contains(pt)) {
                    return true;
                }
            }
            return false;
        }

        private void assemblerMI_Click(object sender, EventArgs e) {
            if (assembler.Visible) {
                assembler.Hide();
            } else {
                assembler.Show();
            }
            if (IsOnScreen(new Point(Location.X - 20, Location.Y + Height + 20))) {
                assembler.Location = new Point(Location.X, Location.Y + Height);
            }
        }

        private void execMI_Click(object sender, EventArgs e) {
            if (executer.Visible) {
                executer.Hide();
            } else {
                executer.Show();
            }
            if (IsOnScreen(new Point(Location.X + Width + 20, Location.Y + Height + 20))) {
                executer.Location = new Point(Location.X + Width, Location.Y + Height);
            }
        }

        private void insrepinfo_Click(object sender, EventArgs e) => parse();

        public static ushort startAddress = 0x0000;

        public async void parse(bool force = false) {
            if (!codeBox.IsChanged &&  !force) { return; }
            codeBox.IsChanged = false;
            await Task.Run(() => {
                assembler.dispatcher.Invoke(() => {
                    assembler.insts.BeginUpdate();
                });
                parser = new Parser(startAddress);
                parser.parse(codeBox.Text);

                assembler.dispatcher.Invoke(() => {
                    try {
                        ushort start = ushort.Parse(assembler.insts.Items[0].SubItems[1].Text, System.Globalization.NumberStyles.HexNumber);
                        ushort end = ushort.Parse(assembler.insts.Items[assembler.insts.Items.Count - 1].SubItems[1].Text, System.Globalization.NumberStyles.HexNumber);

                        MPU.memory.Clear(start, end);
                    } catch (ArgumentOutOfRangeException ex) { }
                });

                assembler.dispatcher.Invoke(() => {
                    assembler.insts.Items.Clear();
                });
                foreach (var ins in parser.instructions) {
                    var mark = string.Empty;
                    assembler.dispatcher.Invoke(() => {
                        foreach (ListViewItem lsitem in assembler.insts.Items) {
                            if (lsitem.SubItems[1].Text == executer.PCVal.Text) {
                                lsitem.Text = "->";
                            } else {
                                lsitem.Text = string.Empty;
                            }
                        }
                    });
                    ListViewItem litem = new ListViewItem(new string[] {
                        mark,
                        ins.Key.ToString("X4"),
                        ins.Value.Name + (ins.Value.Bytes > 1 ?
                        (ins.Value.Name.Contains(" ") ? "," : " ") +
                        ins.Value.Arguments.ToUShort().ToString(ins.Value.Bytes > 2 ? "X4" : "X2") + "H" : string.Empty),
                        ((byte)ins.Value.GetOPCODE()).ToString("X2"),
                        ins.Value.Bytes.ToString(),
                        ins.Value.MCycles.ToString(),
                        ins.Value.TStates.ToString()
                    });

                    assembler.dispatcher.Invoke(() => {
                        assembler.insts.Items.Add(litem);
                    });
                    if (ins.Value.Bytes > 1) {
                        ListViewItem litemLO = new ListViewItem(new string[] {
                            string.Empty,
                            ((ushort)(ins.Key + 1)).ToString("X4"),
                            string.Empty,
                            ins.Value.Arguments.LO.ToString("X2")
                        });

                        assembler.dispatcher.Invoke(() => {
                            assembler.insts.Items.Add(litemLO);
                        });
                        if (ins.Value.Bytes > 2) {
                            ListViewItem litemHO = new ListViewItem(new string[] {
                                string.Empty,
                                ((ushort)(ins.Key + 2)).ToString("X4"),
                                string.Empty,
                                ins.Value.Arguments.HO.ToString("X2")
                            });

                            assembler.dispatcher.Invoke(() => {
                                assembler.insts.Items.Add(litemHO);
                            });
                        }
                    }
                    ins.Value.WriteToMemory(MPU.memory, ins.Key);
                }
                memView.memBox.Invalidate();

                assembler.dispatcher.Invoke(() => {
                    assembler.insts.EndUpdate();
                });
            });
        }

        private async void ValueChanged(object sender, MPU.MPUEventArgs e) {
            executer.dispatcher.Invoke(delegate () {
                switch (e.VarName) {
                    case "A":
                        executer.AReg.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "B":
                        executer.BReg.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "C":
                        executer.CReg.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "D":
                        executer.DReg.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "E":
                        executer.EReg.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "H":
                        executer.HReg.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "L":
                        executer.LReg.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "M":
                        executer.MPoint.Text = ((byte)e.NewValue).ToString("X2");
                        break;
                    case "HRp":
                        var bytes = ((ushort)e.NewValue).ToBytes();
                        executer.HReg.Text = bytes.HO.ToString("X2");
                        executer.LReg.Text = bytes.LO.ToString("X2");
                        break;
                    case "BRp":
                        bytes = ((ushort)e.NewValue).ToBytes();
                        executer.BReg.Text = bytes.HO.ToString("X2");
                        executer.CReg.Text = bytes.LO.ToString("X2");
                        break;
                    case "DRp":
                        bytes = ((ushort)e.NewValue).ToBytes();
                        executer.DReg.Text = bytes.HO.ToString("X2");
                        executer.EReg.Text = bytes.LO.ToString("X2");
                        break;
                    case "PC":
                        executer.PCVal.Text = ((ushort)e.NewValue).ToString("X4");
                        assembler.dispatcher.Invoke(() => {
                            foreach (ListViewItem litem in assembler.insts.Items) {
                                if (litem.SubItems[1].Text == executer.PCVal.Text) {
                                    litem.Text = "->";
                                } else {
                                    litem.Text = string.Empty;
                                }
                            }
                        });
                        break;
                    case "SP":
                        executer.SPVal.Text = ((ushort)e.NewValue).ToString("X4");
                        break;
                    case "Sign":
                    case "S":
                        executer.SFlag.Text = ((bool)e.NewValue).ToBitInt().ToString();
                        break;
                    case "Zero":
                    case "Z":
                        executer.ZFlag.Text = ((bool)e.NewValue).ToBitInt().ToString();
                        break;
                    case "AuxiliaryCarry":
                    case "AC":
                        executer.ACFlag.Text = ((bool)e.NewValue).ToBitInt().ToString();
                        break;
                    case "Parity":
                    case "P":
                        executer.PFlag.Text = ((bool)e.NewValue).ToBitInt().ToString();
                        break;
                    case "Carry":
                    case "CY":
                        executer.CYFlag.Text = ((bool)e.NewValue).ToBitInt().ToString();
                        break;
                    default:
                        MessageBox.Show(e.VarName);
                        break;
                }
            });
        }

        private async void Step(object sender, MPU.MPUEventArgs e) {
            monitor.dispatcher.Invoke(delegate () {
                ushort step = (ushort)e.NewValue;
                string ele = "addr: ";
                switch (e.VarName) {
                    case "PC":
                        try {
                            string mnemonic = assembler.insts.Items.Cast<ListViewItem>().First(x => x.SubItems[1].Text == step.ToString("X4")).SubItems[2].Text;
                            ele += step.ToString("X4") + " ~ " + mnemonic;
                        } catch {
                            ele += step.ToString("X4");
                        }
                        break;
                    default:
                        ele += step.ToString("X4");
                        break;
                }
                string bitstr = MPU.flags.ToBitString();
                ListViewItem nItem = new ListViewItem(new string[] {
                        ele,
                        executer.AReg.Text + "H",
                        executer.BReg.Text + "H",
                        executer.CReg.Text + "H",
                        executer.DReg.Text + "H",
                        executer.EReg.Text + "H",
                        executer.HReg.Text + "H",
                        executer.LReg.Text + "H",
                        executer.MPoint.Text + "H",
                        bitstr
                    });
                monitor.monitorView.Items.Add(nItem);
            });
        }

        private void datamoniMI_Click(object sender, EventArgs e) {
            if (monitor.Visible) {
                monitor.Hide();
            } else {
                monitor.Show();
            }
            if (IsOnScreen(new Point(Location.X + Width + 20, Location.Y - 20))) {
                monitor.Location = new Point(Location.X + Width, Location.Y);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            if (WindowState != FormWindowState.Maximized) {
                monitor.WindowState = WindowState;
                assembler.WindowState = WindowState;
                memView.WindowState = WindowState;
                executer.WindowState = WindowState;
            }
        }
    }
}
