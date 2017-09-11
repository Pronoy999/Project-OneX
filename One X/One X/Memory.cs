using System;
using System.IO;

namespace One_X {
    public class Memory {
        private FileStream fileStream;
        private BinaryReader br;
        private BinaryWriter bw;

        public Memory(string name) {
            fileStream = new FileStream(name, FileMode.OpenOrCreate);
            br = new BinaryReader(fileStream);
            bw = new BinaryWriter(fileStream);
        }
        public byte ReadByte(ushort loc) {
            fileStream.Seek(loc, SeekOrigin.Begin);
            return br.ReadByte();
        }
        public void WriteByte(byte data, ushort loc) {
            fileStream.Seek(loc, SeekOrigin.Begin);
            bw.Write(data);
        }
        public ushort ReadUShort(ushort loc) {
            fileStream.Seek(loc, SeekOrigin.Begin);
            return br.ReadUInt16();
        }
        public void WriteUShort(ushort data, ushort loc) {
            fileStream.Seek(loc, SeekOrigin.Begin);
            bw.Write(data);
        }
    }
}
