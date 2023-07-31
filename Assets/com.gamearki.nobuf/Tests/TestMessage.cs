using System;
using GameArki.NativeBytes;

namespace GameArki.NoBuf.Tests {

    public struct TestMessage : INoBufMessage {

        public bool boolValue;
        public byte byteValue;
        public sbyte sbyteValue;
        public char charValue;
        public ushort ushortValue;
        public short shortValue;
        public uint uintValue;
        public int intValue;
        public ulong ulongValue;
        public long longValue;
        public float floatValue;
        public double doubleValue;
        public decimal decimalValue;
        public NBArray<bool> boolArray;
        public NBArray<byte> byteArray;
        public NBArray<sbyte> sbyteArray;
        public NBArray<char> charArray;
        public NBArray<ushort> ushortArray;
        public NBArray<short> shortArray;
        public NBArray<uint> uintArray;
        public NBArray<int> intArray;
        public NBArray<ulong> ulongArray;
        public NBArray<long> longArray;
        public NBArray<float> floatArray;
        public NBArray<double> doubleArray;
        public NBArray<decimal> decimalArray;
        public NBString stringValue;

        public void FromBytes(byte[] src, ref int offset) {

            int len = NBReader.R_Length(src, ref offset);

            boolValue = NBReader.R_Bool(src, ref offset);
            byteValue = NBReader.R_U8(src, ref offset);
            sbyteValue = NBReader.R_I8(src, ref offset);
            charValue = NBReader.R_Char(src, ref offset);
            ushortValue = NBReader.R_U16(src, ref offset);
            shortValue = NBReader.R_I16(src, ref offset);
            uintValue = NBReader.R_U32(src, ref offset);
            intValue = NBReader.R_I32(src, ref offset);
            ulongValue = NBReader.R_U64(src, ref offset);
            longValue = NBReader.R_I64(src, ref offset);
            floatValue = NBReader.R_F32(src, ref offset);
            doubleValue = NBReader.R_F64(src, ref offset);
            decimalValue = NBReader.R_F128(src, ref offset);
            boolArray = NBReader.R_BoolArr(src, ref offset);
            byteArray = NBReader.R_U8Arr(src, ref offset);
            sbyteArray = NBReader.R_I8Arr(src, ref offset);
            charArray = NBReader.R_CharArr(src, ref offset);
            ushortArray = NBReader.R_U16Arr(src, ref offset);
            shortArray = NBReader.R_I16Arr(src, ref offset);
            uintArray = NBReader.R_U32Arr(src, ref offset);
            intArray = NBReader.R_I32Arr(src, ref offset);
            ulongArray = NBReader.R_U64Arr(src, ref offset);
            longArray = NBReader.R_I64Arr(src, ref offset);
            floatArray = NBReader.R_F32Arr(src, ref offset);
            doubleArray = NBReader.R_F64Arr(src, ref offset);
            decimalArray = NBReader.R_F128Arr(src, ref offset);
            stringValue = NBReader.R_String(src, ref offset);
        }

        public void WriteTo(byte[] dst, ref int offset) {
            int lenOffset = offset;
            offset += 2;

            NBWriter.W_Bool(dst, boolValue, ref offset);
            NBWriter.W_U8(dst, byteValue, ref offset);
            NBWriter.W_I8(dst, sbyteValue, ref offset);
            NBWriter.W_Char(dst, charValue, ref offset);
            NBWriter.W_U16(dst, ushortValue, ref offset);
            NBWriter.W_I16(dst, shortValue, ref offset);
            NBWriter.W_U32(dst, uintValue, ref offset);
            NBWriter.W_I32(dst, intValue, ref offset);
            NBWriter.W_U64(dst, ulongValue, ref offset);
            NBWriter.W_I64(dst, longValue, ref offset);
            NBWriter.W_F32(dst, floatValue, ref offset);
            NBWriter.W_F64(dst, doubleValue, ref offset);
            NBWriter.W_F128(dst, decimalValue, ref offset);
            NBWriter.W_BoolArr(dst, boolArray, ref offset);
            NBWriter.W_U8Arr(dst, byteArray, ref offset);
            NBWriter.W_I8Arr(dst, sbyteArray, ref offset);
            NBWriter.W_CharArr(dst, charArray, ref offset);
            NBWriter.W_U16Arr(dst, ushortArray, ref offset);
            NBWriter.W_I16Arr(dst, shortArray, ref offset);
            NBWriter.W_U32Arr(dst, uintArray, ref offset);
            NBWriter.W_I32Arr(dst, intArray, ref offset);
            NBWriter.W_U64Arr(dst, ulongArray, ref offset);
            NBWriter.W_I64Arr(dst, longArray, ref offset);
            NBWriter.W_F32Arr(dst, floatArray, ref offset);
            NBWriter.W_F64Arr(dst, doubleArray, ref offset);
            NBWriter.W_F128Arr(dst, decimalArray, ref offset);
            NBWriter.W_NBString(dst, stringValue, ref offset);

            NBWriter.W_Length(dst, offset - lenOffset, ref lenOffset);
        }

        public void Dispose() {
            boolArray.Dispose();
            byteArray.Dispose();
            sbyteArray.Dispose();
            charArray.Dispose();
            ushortArray.Dispose();
            shortArray.Dispose();
            uintArray.Dispose();
            intArray.Dispose();
            ulongArray.Dispose();
            longArray.Dispose();
            floatArray.Dispose();
            doubleArray.Dispose();
            decimalArray.Dispose();
            stringValue.Dispose();
        }

    }

}