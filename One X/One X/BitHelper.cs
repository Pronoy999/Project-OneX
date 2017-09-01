﻿namespace One_X {
    public static class BitHelper {
        internal static ushort ToShort(this (byte HO, byte LO) data) => (ushort) (data.HO << 8 + data.LO);
        internal static (byte HO, byte LO) ToBytes(this ushort data) => ((byte)((data & 0xFF00) >> 8), (byte)(data & 0x00FF));

        internal static int ToBitInt(this bool bit) => bit ? 1 : 0;
        internal static bool ToBitBool(this int bit) => bit == 1;

        internal static bool IsNegative(this byte data) => data >> 7 == 1; // according to 8085 MCP
    }
}
