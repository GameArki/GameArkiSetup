using System;
using System.Text;
using System.Runtime.InteropServices;
using GameArki.NativeBytes;

namespace GameArki.NoBuf {

    public static class NBReader {

        public static ushort R_Length(ReadOnlySpan<byte> src, ref int offset) {
            return R_U16(src, ref offset);
        }

        public static bool R_Bool(ReadOnlySpan<byte> src, ref int offset) {
            return R_U8(src, ref offset) == 1;
        }

        public static byte R_U8(ReadOnlySpan<byte> src, ref int offset) {
            return src[offset++];
        }

        public static char R_Char(ReadOnlySpan<byte> src, ref int offset) {
            return (char)R_U16(src, ref offset);
        }

        public static ushort R_U16(ReadOnlySpan<byte> src, ref int offset) {
            ushort data = (ushort)(src[offset++] << 8);
            data |= src[offset++];
            return data;
        }

        public static uint R_U32(ReadOnlySpan<byte> src, ref int offset) {
            uint data = (uint)(src[offset++] << 24);
            data |= (uint)(src[offset++] << 16);
            data |= (uint)(src[offset++] << 8);
            data |= src[offset++];
            return data;
        }

        public static ulong R_U64(ReadOnlySpan<byte> src, ref int offset) {
            ulong data = (ulong)src[offset++] << 56;
            data |= (ulong)src[offset++] << 48;
            data |= (ulong)src[offset++] << 40;
            data |= (ulong)src[offset++] << 32;
            data |= (ulong)src[offset++] << 24;
            data |= (ulong)src[offset++] << 16;
            data |= (ulong)src[offset++] << 8;
            data |= src[offset++];
            return data;
        }

        public static sbyte R_I8(ReadOnlySpan<byte> src, ref int offset) {
            return (sbyte)R_U8(src, ref offset);
        }

        public static short R_I16(ReadOnlySpan<byte> src, ref int offset) {
            return (short)R_U16(src, ref offset);
        }

        public static int R_I32(ReadOnlySpan<byte> src, ref int offset) {
            return (int)R_U32(src, ref offset);
        }

        public static long R_I64(ReadOnlySpan<byte> src, ref int offset) {
            return (long)R_U64(src, ref offset);
        }

        public static float R_F32(ReadOnlySpan<byte> src, ref int offset) {
            uint data = R_U32(src, ref offset);
            NBFloatContent content = new NBFloatContent() { i = data };
            return content.f;
        }

        public static double R_F64(ReadOnlySpan<byte> src, ref int offset) {
            ulong data = R_U64(src, ref offset);
            NBDoubleContent content = new NBDoubleContent() { i = data };
            return content.f;
        }

        public static decimal R_F128(ReadOnlySpan<byte> src, ref int offset) {
            ulong data1 = R_U64(src, ref offset);
            ulong data2 = R_U64(src, ref offset);
            NBDecimalContent content = new NBDecimalContent() { i1 = data1, i2 = data2 };
            return content.f;
        }

        public static NBArray<bool> R_BoolArr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<bool>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_U8(src, ref offset) == 1;
            }
            return data;
        }

        public static NBArray<byte> R_U8Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<byte>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_U8(src, ref offset);
            }
            return data;
        }

        public static NBArray<ushort> R_U16Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<ushort>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_U16(src, ref offset);
            }
            return data;
        }

        public static NBArray<uint> R_U32Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<uint>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_U32(src, ref offset);
            }
            return data;
        }

        public static NBArray<ulong> R_U64Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<ulong>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_U64(src, ref offset);
            }
            return data;
        }

        public static NBArray<sbyte> R_I8Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<sbyte>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_I8(src, ref offset);
            }
            return data;
        }

        public static NBArray<short> R_I16Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<short>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_I16(src, ref offset);
            }
            return data;
        }

        public static NBArray<int> R_I32Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<int>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_I32(src, ref offset);
            }
            return data;
        }

        public static NBArray<long> R_I64Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<long>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_I64(src, ref offset);
            }
            return data;
        }

        public static NBArray<float> R_F32Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<float>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_F32(src, ref offset);
            }
            return data;
        }

        public static NBArray<double> R_F64Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<double>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_F64(src, ref offset);
            }
            return data;
        }

        public static NBArray<decimal> R_F128Arr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<decimal>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_F128(src, ref offset);
            }
            return data;
        }

        public static NBArray<char> R_CharArr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            var data = new NBArray<char>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_Char(src, ref offset);
            }
            return data;
        }

        public static NBString R_String(ReadOnlySpan<byte> src, ref int offset) {
            ushort byteCount = R_Length(src, ref offset);
            if (byteCount == 0) {
                return NBString.Empty;
            }
            ReadOnlySpan<byte> arr = src.Slice(offset, byteCount);
            var data = new NBString(arr);
            offset += byteCount;
            return data;
        }

        public static NBArray<NBString> R_StringArr(ReadOnlySpan<byte> src, ref int offset) {
            ushort arrCount = R_Length(src, ref offset);
            if (arrCount == 0) {
                return new NBArray<NBString>();
            }
            var data = new NBArray<NBString>(arrCount);
            for (int i = 0; i < arrCount; i++) {
                data[i] = R_String(src, ref offset);
            }
            return data;
        }

    }

}
