using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace One_X {
    public static class OneXFile {
        public static bool ExtractOneXFile(string filename, string localdirname) {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
                using (BufferedStream bfs = new BufferedStream(fs, 4 * 1024)) {
                    try {
                        using (ZipArchive archive = new ZipArchive(bfs, ZipArchiveMode.Read, false, Encoding.UTF8)) {
                            if (archive.Entries.Any(x => x.Name == "code") && archive.Entries.Any(x => x.Name == "memory")) {
                                string dir = Application.UserAppDataPath + "\\" + localdirname;
                                if (Directory.Exists(dir)) Directory.Delete(dir, true);
                                Directory.CreateDirectory(dir);
                                archive.ExtractToDirectory(dir);
                                return true;
                            }
                            return false;
                        }
                    } catch (Exception) {
                        return false;
                    }
                }
            }
        }

        public static void RepackOneXFile(string filename, string localdirname) {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                using (BufferedStream bfs = new BufferedStream(fs, 4 * 1024)) {
                    using (ZipArchive archive = new ZipArchive(bfs, ZipArchiveMode.Create, false, Encoding.UTF8)) {
                        string dir = Application.UserAppDataPath + "\\" + localdirname;
                        archive.CreateEntryFromFile(dir + "\\code", "code", CompressionLevel.Optimal);
                        archive.CreateEntryFromFile(dir + "\\memory", "memory", CompressionLevel.Optimal);
                    }
                }
            }
        }
    }
}
