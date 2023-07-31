using System;
using GameArki.NativeBytes;

namespace GameArki.NoBuf {

    public static class NBWriter {

        public static void W_Length(Span<byte> dst, int byteLen, ref int offset) {
            W_U16(dst, (ushort)(uint)byteLen, ref offset);
        }

        public static void W_Bool(Span<byte> dst, bool data, ref int offset) {
            W_U8(dst, (byte)(data ? 1 : 0), ref offset);
        }

        public static void W_U8(Span<byte> dst, byte data, ref int offset) {
            dst[offset++] = data;
        }

        public static void W_Char(Span<byte> dst, char data, ref int offset) {
            W_U16(dst, (ushort)data, ref offset);
        }

        public static void W_U16(Span<byte> dst, ushort data, ref int offset) {
            dst[offset++] = (byte)(data >> 8);
            dst[offset++] = (byte)(data);
        }

        public static void W_U32(Span<byte> dst, uint data, ref int offset) {
            dst[offset++] = (byte)(data >> 24);
            dst[offset++] = (byte)(data >> 16);
            dst[offset++] = (byte)(data >> 8);
            dst[offset++] = (byte)(data);
        }

        public static void W_U64(Span<byte> dst, ulong data, ref int offset) {
            dst[offset++] = (byte)(data >> 56);
            dst[offset++] = (byte)(data >> 48);
            dst[offset++] = (byte)(data >> 40);
            dst[offset++] = (byte)(data >> 32);
            dst[offset++] = (byte)(data >> 24);
            dst[offset++] = (byte)(data >> 16);
            dst[offset++] = (byte)(data >> 8);
            dst[offset++] = (byte)(data);
        }

        public static void W_I8(Span<byte> dst, sbyte data, ref int offset) {
            W_U8(dst, (byte)data, ref offset);
        }

        public static void W_I16(Span<byte> dst, short data, ref int offset) {
            W_U16(dst, (ushort)data, ref offset);
        }

        public static void W_I32(Span<byte> dst, int data, ref int offset) {
            W_U32(dst, (uint)data, ref offset);
        }

        public static void W_I64(Span<byte> dst, long data, ref int offset) {
            W_U64(dst, (ulong)data, ref offset);
        }

        public static void W_F32(Span<byte> dst, float data, ref int offset) {
            NBFloatContent content = new NBFloatContent { f = data };
            W_U32(dst, content.i, ref offset);
        }

        public static void W_F64(Span<byte> dst, double data, ref int offset) {
            NBDoubleContent content = new NBDoubleContent { f = data };
            W_U64(dst, content.i, ref offset);
        }

        public static void W_F128(Span<byte> dst, decimal data, ref int offset) {
            NBDecimalContent content = new NBDecimalContent { f = data };
            W_U64(dst, content.i1, ref offset);
            W_U64(dst, content.i2, ref offset);
        }

        public static void W_BoolArr(Span<byte> dst, NBArray<bool> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_Bool(dst, data[i], ref offset);
            }
        }

        public static void W_U8Arr(Span<byte> dst, NBArray<byte> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                dst[offset++] = data[i];
            }
        }

        public static void W_U16Arr(Span<byte> dst, NBArray<ushort> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_U16(dst, data[i], ref offset);
            }
        }

        public static void W_U32Arr(Span<byte> dst, NBArray<uint> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_U32(dst, data[i], ref offset);
            }
        }

        public static void W_U64Arr(Span<byte> dst, NBArray<ulong> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_U64(dst, data[i], ref offset);
            }
        }

        public static void W_I8Arr(Span<byte> dst, NBArray<sbyte> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_I8(dst, data[i], ref offset);
            }
        }

        public static void W_I16Arr(Span<byte> dst, NBArray<short> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_I16(dst, data[i], ref offset);
            }
        }

        public static void W_I32Arr(Span<byte> dst, NBArray<int> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_I32(dst, data[i], ref offset);
            }
        }

        public static void W_I64Arr(Span<byte> dst, NBArray<long> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_I64(dst, data[i], ref offset);
            }
        }

        public static void W_F32Arr(Span<byte> dst, NBArray<float> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_F32(dst, data[i], ref offset);
            }
        }

        public static void W_F64Arr(Span<byte> dst, NBArray<double> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_F64(dst, data[i], ref offset);
            }
        }

        public static void W_F128Arr(Span<byte> dst, NBArray<decimal> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_F128(dst, data[i], ref offset);
            }
        }

        public static void W_CharArr(Span<byte> dst, NBArray<char> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < arrCount; i++) {
                W_Char(dst, data[i], ref offset);
            }
        }

        public static void W_String(Span<byte> dst, string data, ref int offset) {
            NBString txt = new NBString(data);
            W_NBString(dst, txt, ref offset);
        }

        public static void W_NBString(Span<byte> dst, in NBString data, ref int offset) {
            int count = data.Length;
            W_Length(dst, count, ref offset);
            for (int i = 0; i < count; i++) {
                dst[offset++] = data[i];
            }
        }

        public static void W_StringArr(Span<byte> dst, NBArray<NBString> data, ref int offset) {
            int arrCount = data.Length;
            W_Length(dst, arrCount, ref offset);
            for (int i = 0; i < data.Length; i++) {
                W_NBString(dst, data[i], ref offset);
            }
        }

    }

}
