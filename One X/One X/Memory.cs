using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace One_X {
    static class Memory
    {//TODO:define constructor initialise filestream,binary writer reader in it
        //TODO:functn to change pos in file stream
        static private FileStream fileStream;
        static private String fileName = "Memory.dat";
        static private short stackTop, stackTail;
        static byte ReadByte(short offset)
        {
            fileStream=new FileStream(fileName,FileMode.Append);
            fileStream.Seek(offset, SeekOrigin.Begin);
            BinaryReader binaryReader=new BinaryReader(fileStream);
            return binaryReader.ReadByte();
        }

        static void WriteByte(byte data,short offset)
        {
            fileStream = new FileStream(fileName, FileMode.Append);
            fileStream.Seek(offset,SeekOrigin.Begin);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(data);
        }

        static void PushStack(byte data)
        {
            fileStream = new FileStream(fileName, FileMode.Append);
            fileStream.Seek(stackTop, SeekOrigin.Begin);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(data);
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
