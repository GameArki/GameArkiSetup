using System;
using System.Collections.Generic;
using GameArki.BufferIO;

namespace GameArki.BufferIO.Sample
{
    [BufferIOMessageObject]
    public struct MyModel : IBufferIOMessage<MyModel>
    {
        public bool boolValue;
        public bool[] boolArr;
        public List<bool> boolList;
        public byte byteValue;
        public byte[] byteArr;
        public List<byte> byteList;
        public sbyte sbyteValue;
        public sbyte[] sbyteArr;
        public List<sbyte> sbyteList;
        public short shortValue;
        public short[] shortArr;
        public List<short> shortList;
        public ushort ushortValue;
        public ushort[] ushortArr;
        public List<ushort> ushortList;
        public int intValue;
        public int[] intArr;
        public List<int> intList;
        public uint uintValue;
        public uint[] uintArr;
        public List<uint> uintList;
        public long longValue;
        public long[] longArr;
        public List<long> longList;
        public ulong ulongValue;
        public ulong[] ulongArr;
        public List<ulong> ulongList;
        public float floatValue;
        public float[] floatArr;
        public List<float> floatList;
        public double doubleValue;
        public double[] doubleArr;
        public List<double> doubleList;
        public char charValue;
        public string strValue;
        public string[] strArr;
        public List<string> strList;
        public HerModel herModel;
        public HerModel[] herModelArr;
        public List<HerModel> herModelList;
        public string otherStr;
        public void WriteTo(byte[] dst, ref int offset)
        {
            BufferWriter.WriteBool(dst, boolValue, ref offset);
            BufferWriter.WriteBoolArr(dst, boolArr, ref offset);
            BufferWriter.WriteBoolList(dst, boolList, ref offset);
            BufferWriter.WriteUInt8(dst, byteValue, ref offset);
            BufferWriter.WriteUint8Arr(dst, byteArr, ref offset);
            BufferWriter.WriteUint8List(dst, byteList, ref offset);
            BufferWriter.WriteInt8(dst, sbyteValue, ref offset);
            BufferWriter.WriteInt8Arr(dst, sbyteArr, ref offset);
            BufferWriter.WriteInt8List(dst, sbyteList, ref offset);
            BufferWriter.WriteInt16(dst, shortValue, ref offset);
            BufferWriter.WriteInt16Arr(dst, shortArr, ref offset);
            BufferWriter.WriteInt16List(dst, shortList, ref offset);
            BufferWriter.WriteUInt16(dst, ushortValue, ref offset);
            BufferWriter.WriteUInt16Arr(dst, ushortArr, ref offset);
            BufferWriter.WriteUInt16List(dst, ushortList, ref offset);
            BufferWriter.WriteInt32(dst, intValue, ref offset);
            BufferWriter.WriteInt32Arr(dst, intArr, ref offset);
            BufferWriter.WriteInt32List(dst, intList, ref offset);
            BufferWriter.WriteUInt32(dst, uintValue, ref offset);
            BufferWriter.WriteUInt32Arr(dst, uintArr, ref offset);
            BufferWriter.WriteUInt32List(dst, uintList, ref offset);
            BufferWriter.WriteInt64(dst, longValue, ref offset);
            BufferWriter.WriteInt64Arr(dst, longArr, ref offset);
            BufferWriter.WriteInt64List(dst, longList, ref offset);
            BufferWriter.WriteUInt64(dst, ulongValue, ref offset);
            BufferWriter.WriteUInt64Arr(dst, ulongArr, ref offset);
            BufferWriter.WriteUInt64List(dst, ulongList, ref offset);
            BufferWriter.WriteSingle(dst, floatValue, ref offset);
            BufferWriter.WriteSingleArr(dst, floatArr, ref offset);
            BufferWriter.WriteSingleList(dst, floatList, ref offset);
            BufferWriter.WriteDouble(dst, doubleValue, ref offset);
            BufferWriter.WriteDoubleArr(dst, doubleArr, ref offset);
            BufferWriter.WriteDoubleList(dst, doubleList, ref offset);
            BufferWriter.WriteChar(dst, charValue, ref offset);
            BufferWriter.WriteUTF8String(dst, strValue, ref offset);
            BufferWriter.WriteUTF8StringArr(dst, strArr, ref offset);
            BufferWriter.WriteUTF8StringList(dst, strList, ref offset);
            BufferWriterExtra.WriteMessage(dst, herModel, ref offset);
            BufferWriterExtra.WriteMessageArr(dst, herModelArr, ref offset);
            BufferWriterExtra.WriteMessageList(dst, herModelList, ref offset);
            BufferWriter.WriteUTF8String(dst, otherStr, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset)
        {
            boolValue = BufferReader.ReadBool(src, ref offset);
            boolArr = BufferReader.ReadBoolArr(src, ref offset);
            boolList = BufferReader.ReadBoolList(src, ref offset);
            byteValue = BufferReader.ReadUInt8(src, ref offset);
            byteArr = BufferReader.ReadUInt8Arr(src, ref offset);
            byteList = BufferReader.ReadUInt8List(src, ref offset);
            sbyteValue = BufferReader.ReadInt8(src, ref offset);
            sbyteArr = BufferReader.ReadInt8Arr(src, ref offset);
            sbyteList = BufferReader.ReadInt8List(src, ref offset);
            shortValue = BufferReader.ReadInt16(src, ref offset);
            shortArr = BufferReader.ReadInt16Arr(src, ref offset);
            shortList = BufferReader.ReadInt16List(src, ref offset);
            ushortValue = BufferReader.ReadUInt16(src, ref offset);
            ushortArr = BufferReader.ReadUInt16Arr(src, ref offset);
            ushortList = BufferReader.ReadUInt16List(src, ref offset);
            intValue = BufferReader.ReadInt32(src, ref offset);
            intArr = BufferReader.ReadInt32Arr(src, ref offset);
            intList = BufferReader.ReadInt32List(src, ref offset);
            uintValue = BufferReader.ReadUInt32(src, ref offset);
            uintArr = BufferReader.ReadUInt32Arr(src, ref offset);
            uintList = BufferReader.ReadUInt32List(src, ref offset);
            longValue = BufferReader.ReadInt64(src, ref offset);
            longArr = BufferReader.ReadInt64Arr(src, ref offset);
            longList = BufferReader.ReadInt64List(src, ref offset);
            ulongValue = BufferReader.ReadUInt64(src, ref offset);
            ulongArr = BufferReader.ReadUInt64Arr(src, ref offset);
            ulongList = BufferReader.ReadUInt64List(src, ref offset);
            floatValue = BufferReader.ReadSingle(src, ref offset);
            floatArr = BufferReader.ReadSingleArr(src, ref offset);
            floatList = BufferReader.ReadSingleList(src, ref offset);
            doubleValue = BufferReader.ReadDouble(src, ref offset);
            doubleArr = BufferReader.ReadDoubleArr(src, ref offset);
            doubleList = BufferReader.ReadDoubleList(src, ref offset);
            charValue = BufferReader.ReadChar(src, ref offset);
            strValue = BufferReader.ReadUTF8String(src, ref offset);
            strArr = BufferReader.ReadUTF8StringArr(src, ref offset);
            strList = BufferReader.ReadUTF8StringList(src, ref offset);
            herModel = BufferReaderExtra.ReadMessage(src, () => new HerModel(), ref offset);
            herModelArr = BufferReaderExtra.ReadMessageArr(src, () => new HerModel(), ref offset);
            herModelList = BufferReaderExtra.ReadMessageList(src, () => new HerModel(), ref offset);
            otherStr = BufferReader.ReadUTF8String(src, ref offset);
        }
    }
}