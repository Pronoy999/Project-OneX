﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One_X {
    public static class BitHelper {
        internal static ushort ToUShort(this (byte HO, byte LO) data) => (ushort)((data.HO << 8) + data.LO);
        internal static (byte HO, byte LO) ToBytes(this ushort data) => ((byte)((data & 0xFF00) >> 8), (byte)(data & 0x00FF));

        internal static int ToBitInt(this bool bit) => bit ? 1 : 0;
        internal static bool ToBitBool(this int bit) => bit == 1;

        internal static bool IsNegative(this byte data) => data >> 7 == 1; // according to 8085 MCP

        internal static byte TwosComplement(this byte data) => (byte)(~data + 1);

        internal static bool Parity(this byte data) {
            uint y = data;
            y = y ^ (y >> 1);
            y = y ^ (y >> 2);
            y = y ^ (y >> 4);
            return (y & 1) == 0;
        }

        internal static byte ToByte(this BitArray bitArray) {
            return bitArray.ToBytes()[0];
        }

        internal static byte[] ToBytes(this BitArray bitArray) {
            if (bitArray.Length > 8)
                throw new ArgumentException();

            byte[] array = new byte[1];
            bitArray.CopyTo(array, 0);
            return array;
        }

        internal static BitArray ToBitArray(this byte data) => new BitArray(new byte[] { data });

        internal static (byte HON, byte LON) ToNibbles(this byte data) {
            return ((byte)((data & 0xf0) >> 4), (byte)(data & 0x0f));
        }

        internal static string ToBitString(this BitArray bits) {
            var sb = new StringBuilder();
            var x = ((MPU.Flag[])Enum.GetValues(typeof(MPU.Flag)));
            for (int i = 0; i < bits.Count && x.Contains((MPU.Flag)i); i++) {
                sb.Append(bits[i] ? '1' : '0');
                sb.Append(" : ");
            }
            string str = sb.ToString();
            return str.Remove(str.Length - 3);
        }
    }
}
