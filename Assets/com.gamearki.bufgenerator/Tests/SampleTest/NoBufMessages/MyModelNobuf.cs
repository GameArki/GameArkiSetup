using System;
using GameArki.NoBuf;
using GameArki.NativeBytes;

namespace GameArki.BufferIO.Sample
{
    public struct MyModelNobuf : INoBufMessage
    {
        public bool boolValue;
        public char charValue;
        public byte byteValue;
        public sbyte sbyteValue;
        public short shortValue;
        public ushort ushortValue;
        public int intValue;
        public uint uintValue;
        public long longValue;
        public ulong ulongValue;
        public float floatValue;
        public double doubleValue;
        public NBArray<bool> boolArr;
        public NBArray<byte> byteArr;
        public NBArray<sbyte> sbyteArr;
        public NBArray<short> shortArr;
        public NBArray<ushort> ushortArr;
        public NBArray<int> intArr;
        public NBArray<uint> uintArr;
        public NBArray<long> longArr;
        public NBArray<ulong> ulongArr;
        public NBArray<float> floatArr;
        public NBArray<double> doubleArr;
        public NBString strValue;
        public NBArray<NBString> strArr;
        public void WriteTo(byte[] dst, ref int offset)
        {
            NBWriter.W_Bool(dst, boolValue, ref offset);
            NBWriter.W_Char(dst, charValue, ref offset);
            NBWriter.W_U8(dst, byteValue, ref offset);
            NBWriter.W_I8(dst, sbyteValue, ref offset);
            NBWriter.W_I16(dst, shortValue, ref offset);
            NBWriter.W_U16(dst, ushortValue, ref offset);
            NBWriter.W_I32(dst, intValue, ref offset);
            NBWriter.W_U32(dst, uintValue, ref offset);
            NBWriter.W_I64(dst, longValue, ref offset);
            NBWriter.W_U64(dst, ulongValue, ref offset);
            NBWriter.W_F32(dst, floatValue, ref offset);
            NBWriter.W_F64(dst, doubleValue, ref offset);
            NBWriter.W_BoolArr(dst, boolArr, ref offset);
            NBWriter.W_U8Arr(dst, byteArr, ref offset);
            NBWriter.W_I8Arr(dst, sbyteArr, ref offset);
            NBWriter.W_I16Arr(dst, shortArr, ref offset);
            NBWriter.W_U16Arr(dst, ushortArr, ref offset);
            NBWriter.W_I32Arr(dst, intArr, ref offset);
            NBWriter.W_U32Arr(dst, uintArr, ref offset);
            NBWriter.W_I64Arr(dst, longArr, ref offset);
            NBWriter.W_U64Arr(dst, ulongArr, ref offset);
            NBWriter.W_F32Arr(dst, floatArr, ref offset);
            NBWriter.W_F64Arr(dst, doubleArr, ref offset);
            NBWriter.W_NBString(dst, strValue, ref offset);
            NBWriter.W_StringArr(dst, strArr, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset)
        {
            boolValue = NBReader.R_Bool(src, ref offset);
            charValue = NBReader.R_Char(src, ref offset);
            byteValue = NBReader.R_U8(src, ref offset);
            sbyteValue = NBReader.R_I8(src, ref offset);
            shortValue = NBReader.R_I16(src, ref offset);
            ushortValue = NBReader.R_U16(src, ref offset);
            intValue = NBReader.R_I32(src, ref offset);
            uintValue = NBReader.R_U32(src, ref offset);
            longValue = NBReader.R_I64(src, ref offset);
            ulongValue = NBReader.R_U64(src, ref offset);
            floatValue = NBReader.R_F32(src, ref offset);
            doubleValue = NBReader.R_F64(src, ref offset);
            boolArr = NBReader.R_BoolArr(src, ref offset);
            byteArr = NBReader.R_U8Arr(src, ref offset);
            sbyteArr = NBReader.R_I8Arr(src, ref offset);
            shortArr = NBReader.R_I16Arr(src, ref offset);
            ushortArr = NBReader.R_U16Arr(src, ref offset);
            intArr = NBReader.R_I32Arr(src, ref offset);
            uintArr = NBReader.R_U32Arr(src, ref offset);
            longArr = NBReader.R_I64Arr(src, ref offset);
            ulongArr = NBReader.R_U64Arr(src, ref offset);
            floatArr = NBReader.R_F32Arr(src, ref offset);
            doubleArr = NBReader.R_F64Arr(src, ref offset);
            strValue = NBReader.R_String(src, ref offset);
            strArr = NBReader.R_StringArr(src, ref offset);
        }

        public void Dispose()
        {
            boolArr.Dispose();
            byteArr.Dispose();
            sbyteArr.Dispose();
            shortArr.Dispose();
            ushortArr.Dispose();
            intArr.Dispose();
            uintArr.Dispose();
            longArr.Dispose();
            ulongArr.Dispose();
            floatArr.Dispose();
            doubleArr.Dispose();
            strValue.Dispose();
            strArr.Dispose();
        }
    }
}