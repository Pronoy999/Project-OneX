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

namespace One_X {
    public partial class MainForm : Form {
        private string codeFileName = string.Empty;
        private string saveFileName = string.Empty;

        private bool saved = false;
        public PrivateFontCollection pfc = new PrivateFontCollection();

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            MenuItem[] notImp = { updateMI, aboutMI, memeditMI, assemblerMI, datamoniMI, execMI, optionsMI };
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

            codeBox.AutoIndentCharsPatterns = "^\\s*[\\w]+\\s*(?<range>:)\\s*(?<range>[^\\n]+)$";

            foreach (var i in notImp) {
                i.Click += (s, ev) => MessageBox.Show("NOT IMPLEMENTED YET!");
            }
            Debug.WriteLine(Application.UserAppDataPath + "\\currentfile");
            New();
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

            e.ChangedRange.SetStyle(labelStyle, RegexHelper.rxRangeLabelOnly);
            e.ChangedRange.SetStyle(labelStyle, RegexHelper.rxRangeReference);

            e.ChangedRange.SetStyle(literalStyle, RegexHelper.rxRangeLiteralByte);
            e.ChangedRange.SetStyle(literalStyle, RegexHelper.rxRangeLiteralUShort);

            UpdateModifiedInfo();
        }

        private void codeBox_AutoIndentNeeded(object sender, AutoIndentEventArgs e) {
            int lpos = e.PrevLineText.IndexOf(":");
            if (lpos > 0) {
                e.AbsoluteIndentation = lpos + 2;
            }
            if (e.LineText.Contains(":")) {
                e.AbsoluteIndentation = 0;
            }
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
            DialogResult dr = MessageBox.Show("Warning: Unsaved changes!\nSave changes to " + (string.IsNullOrWhiteSpace(saveFileName) ? "new source file" : saveFileName) +  "?", "Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
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
            modifiedinfo.Text = "";
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
                MPU.CommitMemory();
                if (OneXFile.ExtractOneXFile(openFile.FileName, "currentfile")) {
                    saveFileName = openFile.FileName;
                    string dir = Application.UserAppDataPath + "\\currentfile";
                    codeFileName = dir + "\\code";
                    codeBox.OpenFile(codeFileName, Encoding.UTF8);
                    MPU.InitMemory(dir + "\\memory");
                    codeBox.IsChanged = false;
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
            MPU.CommitMemory();
            OneXFile.RepackOneXFile(name, "currentfile");
            MPU.InitMemory(dir + "\\memory");
            // commit changes (reset all dirty flags)
            codeBox.IsChanged = false;
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
            try { MPU.CommitMemory(); Directory.Delete(dir, true); } catch (IOException) { } catch (NullReferenceException) { }
            Directory.CreateDirectory(dir);
            MPU.InitMemory(dir + "\\memory");
            codeBox.IsChanged = false;

            codeBox.Visible = true;
        }

        private void newMI_Click(object sender, EventArgs e) {
            New();
        }

        private async void parseTimer_Tick(object sender, EventArgs e) {
            await Task.Run(() => {

            });
        }
    }
}
