using System;
using GameArki.BufferIO;

namespace GameArki.BufferIO.Tests
{
    [BufferIOMessageObject]
    public struct MyModel : IBufferIOMessage<MyModel>
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
        public bool[] boolArr;
        public byte[] byteArr;
        public sbyte[] sbyteArr;
        public short[] shortArr;
        public ushort[] ushortArr;
        public int[] intArr;
        public uint[] uintArr;
        public long[] longArr;
        public ulong[] ulongArr;
        public float[] floatArr;
        public double[] doubleArr;
        public string strValue;
        public string[] strArr;
        public HerModel herModel;
        public HerModel[] herModelArr;
        public string otherStr;
        public void WriteTo(byte[] dst, ref int offset)
        {
            BufferWriter.WriteBool(dst, boolValue, ref offset);
            BufferWriter.WriteChar(dst, charValue, ref offset);
            BufferWriter.WriteUInt8(dst, byteValue, ref offset);
            BufferWriter.WriteInt8(dst, sbyteValue, ref offset);
            BufferWriter.WriteInt16(dst, shortValue, ref offset);
            BufferWriter.WriteUInt16(dst, ushortValue, ref offset);
            BufferWriter.WriteInt32(dst, intValue, ref offset);
            BufferWriter.WriteUInt32(dst, uintValue, ref offset);
            BufferWriter.WriteInt64(dst, longValue, ref offset);
            BufferWriter.WriteUInt64(dst, ulongValue, ref offset);
            BufferWriter.WriteSingle(dst, floatValue, ref offset);
            BufferWriter.WriteDouble(dst, doubleValue, ref offset);
            BufferWriter.WriteBoolArr(dst, boolArr, ref offset);
            BufferWriter.WriteUint8Arr(dst, byteArr, ref offset);
            BufferWriter.WriteInt8Arr(dst, sbyteArr, ref offset);
            BufferWriter.WriteInt16Arr(dst, shortArr, ref offset);
            BufferWriter.WriteUInt16Arr(dst, ushortArr, ref offset);
            BufferWriter.WriteInt32Arr(dst, intArr, ref offset);
            BufferWriter.WriteUInt32Arr(dst, uintArr, ref offset);
            BufferWriter.WriteInt64Arr(dst, longArr, ref offset);
            BufferWriter.WriteUInt64Arr(dst, ulongArr, ref offset);
            BufferWriter.WriteSingleArr(dst, floatArr, ref offset);
            BufferWriter.WriteDoubleArr(dst, doubleArr, ref offset);
            BufferWriter.WriteUTF8String(dst, strValue, ref offset);
            BufferWriter.WriteUTF8StringArr(dst, strArr, ref offset);
            BufferWriterExtra.WriteMessage(dst, herModel, ref offset);
            BufferWriterExtra.WriteMessageArr(dst, herModelArr, ref offset);
            BufferWriter.WriteUTF8String(dst, otherStr, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset)
        {
            boolValue = BufferReader.ReadBool(src, ref offset);
            charValue = BufferReader.ReadChar(src, ref offset);
            byteValue = BufferReader.ReadUInt8(src, ref offset);
            sbyteValue = BufferReader.ReadInt8(src, ref offset);
            shortValue = BufferReader.ReadInt16(src, ref offset);
            ushortValue = BufferReader.ReadUInt16(src, ref offset);
            intValue = BufferReader.ReadInt32(src, ref offset);
            uintValue = BufferReader.ReadUInt32(src, ref offset);
            longValue = BufferReader.ReadInt64(src, ref offset);
            ulongValue = BufferReader.ReadUInt64(src, ref offset);
            floatValue = BufferReader.ReadSingle(src, ref offset);
            doubleValue = BufferReader.ReadDouble(src, ref offset);
            boolArr = BufferReader.ReadBoolArr(src, ref offset);
            byteArr = BufferReader.ReadUInt8Arr(src, ref offset);
            sbyteArr = BufferReader.ReadInt8Arr(src, ref offset);
            shortArr = BufferReader.ReadInt16Arr(src, ref offset);
            ushortArr = BufferReader.ReadUInt16Arr(src, ref offset);
            intArr = BufferReader.ReadInt32Arr(src, ref offset);
            uintArr = BufferReader.ReadUInt32Arr(src, ref offset);
            longArr = BufferReader.ReadInt64Arr(src, ref offset);
            ulongArr = BufferReader.ReadUInt64Arr(src, ref offset);
            floatArr = BufferReader.ReadSingleArr(src, ref offset);
            doubleArr = BufferReader.ReadDoubleArr(src, ref offset);
            strValue = BufferReader.ReadUTF8String(src, ref offset);
            strArr = BufferReader.ReadUTF8StringArr(src, ref offset);
            herModel = BufferReaderExtra.ReadMessage(src, () => new HerModel(), ref offset);
            herModelArr = BufferReaderExtra.ReadMessageArr(src, () => new HerModel(), ref offset);
            otherStr = BufferReader.ReadUTF8String(src, ref offset);
        }
    }
}