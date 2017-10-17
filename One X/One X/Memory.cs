using Be.Windows.Forms;
using System;
using System.IO;

namespace One_X {
    public class Memory {
        internal DynamicFileByteProvider provider;
        private FileStream fileStream;

        public Memory(string name) {
            fileStream = new FileStream(name, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fileStream.SetLength(0x10000);

            provider = new DynamicFileByteProvider(fileStream);
        }
        public byte ReadByte(ushort loc) => provider.ReadByte(loc);
        public ushort ReadUShort(ushort loc) => (provider.ReadByte(loc + 1), provider.ReadByte(loc)).ToUShort();

        public void WriteByte(byte data, ushort loc) {
            provider.WriteByte(loc, data);
            provider.ApplyChanges();
        }

        public void WriteUShort(ushort data, ushort loc) {
            provider.WriteByte(loc, data.ToBytes().LO);
            provider.WriteByte(loc + 1, data.ToBytes().HO);
            provider.ApplyChanges();
        }

        public void Clear(ushort locStart, ushort locEnd) {
            for (int i = locStart; i <= locEnd; i++) {
                provider.WriteByte(i, 0x00);
            }
            provider.ApplyChanges();
        }

        public void Commit(string name) {
            fileStream.Close();
            provider.Dispose();
        }
    }
}
