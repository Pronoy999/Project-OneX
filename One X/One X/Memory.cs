using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace One_X {
    class Memory
    {
     //TODO:functn to change pos in file stream

        private FileStream fileStream;
        private short stackTop, stackTail;
        private BinaryReader br;
        private BinaryWriter bw;
        Memory(string name) {
            fileStream = new FileStream(name, FileMode.OpenOrCreate);
            br = new BinaryReader(fileStream);
            bw = new BinaryWriter(fileStream);
        }
        public byte ReadByte(ushort loc)
        {
            fileStream.Seek(loc, SeekOrigin.Begin);
            return br.ReadByte();
        }
        public void WriteByte(byte data,ushort loc)
        {
            fileStream.Seek(loc, SeekOrigin.Begin);
            bw.Write(data);
        }
        public ushort ReadUShort(ushort loc)
        {
            fileStream.Seek(loc, SeekOrigin.Begin);
            return br.ReadUInt16();
        }
        public void WriteUShort(ushort data,ushort loc)
        {
            fileStream.Seek(loc, SeekOrigin.Begin);
            bw.Write(data);
        }
         void PushStack(byte data)
        {//check sp and update
            
        }

        static byte PopStack()
        {
            byte data=0;
            return data;  //TODO: Check the MPU stack Condition and then return data. 
        }

        static void InitStack()
        {
            //TODO: Initialize the Stack Top and Stack Tail. 
        }
    }
}
