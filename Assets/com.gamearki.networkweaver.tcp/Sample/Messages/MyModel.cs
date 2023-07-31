using System;
using GameArki.BufferIO;

namespace GameArki.Network.Sample
{
    [BufferIOMessageObject]
    public struct MyModel : IBufferIOMessage<MyModel>
    {
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

        public int GetEvaluatedSize(out bool isCertain)
        {
            int count = 74;
            isCertain = false;
            if (byteArr != null)
            {
                count += byteArr.Length;
            }

            if (sbyteArr != null)
            {
                count += sbyteArr.Length;
            }

            if (shortArr != null)
            {
                count += shortArr.Length * 2;
            }

            if (ushortArr != null)
            {
                count += ushortArr.Length * 2;
            }

            if (intArr != null)
            {
                count += intArr.Length * 4;
            }

            if (uintArr != null)
            {
                count += uintArr.Length * 4;
            }

            if (longArr != null)
            {
                count += longArr.Length * 8;
            }

            if (ulongArr != null)
            {
                count += ulongArr.Length * 8;
            }

            if (floatArr != null)
            {
                count += floatArr.Length * 4;
            }

            if (doubleArr != null)
            {
                count += doubleArr.Length * 8;
            }

            if (strValue != null)
            {
                count += strValue.Length * 4;
            }

            if (strArr != null)
            {
                for (int i = 0; i < strArr.Length; i += 1)
                {
                    count += strArr[i].Length * 4;
                }
            }

            if (otherStr != null)
            {
                count += otherStr.Length * 4;
            }

            if (herModel != null)
            {
                count += herModel.GetEvaluatedSize(out bool _bherModel);
                isCertain &= _bherModel;
            }

            if (herModelArr != null)
            {
                for (int i = 0; i < herModelArr.Length; i += 1)
                {
                    var __child = herModelArr[i];
                    if (__child != null)
                    {
                        count += __child.GetEvaluatedSize(out bool _cb_herModelArr);
                        isCertain &= _cb_herModelArr;
                    }
                }
            }

            return count;
        }

        public byte[] ToBytes()
        {
            int count = GetEvaluatedSize(out bool isCertain);
            int offset = 0;
            byte[] src = new byte[count];
            WriteTo(src, ref offset);
            if (isCertain)
            {
                return src;
            }
            else
            {
                byte[] dst = new byte[offset];
                Buffer.BlockCopy(src, 0, dst, 0, offset);
                return dst;
            }
        }
    }
}