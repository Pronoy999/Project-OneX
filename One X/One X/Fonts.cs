using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

using One_X.Properties;

namespace One_X.Fonts {
    public class Fonts {
        private static PrivateFontCollection sFonts;
        static Fonts() {
            sFonts = new PrivateFontCollection();

            AddFont(Resources.Hack);
        }
        private static void AddFont(byte[] font) {
            var buffer = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, buffer, font.Length);
            sFonts.AddMemoryFont(buffer, font.Length);
        }
        public static Font Create(
            FontFamily family,
            float emSize,
            FontStyle style = FontStyle.Regular,
            GraphicsUnit unit = GraphicsUnit.Point) {
            var fam = sFonts.Families[(int)family];
            return new Font(fam, emSize, style, unit);
        }
    }
    public enum FontFamily {
        Hack = 0
    }
}
