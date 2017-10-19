using System;
using System.Collections;

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
            if (bitArray.Length > 8)
                throw new ArgumentException();

            byte[] array = new byte[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }

        internal static BitArray ToBitArray(this byte data) => new BitArray(new byte[] { data });

        internal static (byte HON, byte LON) ToNibbles(this byte data) {
            return ((byte)((data & 0xf0) >> 4), (byte)(data & 0x0f));
        }
    }
}
