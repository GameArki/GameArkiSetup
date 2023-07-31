using NUnit.Framework;
using GameArki.BufferIO.Sample;

namespace GameArki.BufferIO.Tests {

    public class BufferIOExtraTest {

        [Test]
        public void TestFixed() {

            byte[] dst = new byte[4096];
            int writeOffset = 0;
            int readOffset = 0;

            BufferWriter.WriteBool(dst, true, ref writeOffset);
            bool boolValue = BufferReader.ReadBool(dst, ref readOffset);
            Assert.That(boolValue, Is.EqualTo(true));

            BufferWriter.WriteChar(dst, 'c', ref writeOffset);
            char charValue = BufferReader.ReadChar(dst, ref readOffset);
            Assert.That(charValue, Is.EqualTo('c'));

            BufferWriter.WriteInt8(dst, -1, ref writeOffset);
            sbyte sbyteValue = BufferReader.ReadInt8(dst, ref readOffset);
            Assert.That(sbyteValue, Is.EqualTo(-1));

            BufferWriter.WriteUInt8(dst, 4, ref writeOffset);
            byte byteValue = BufferReader.ReadUInt8(dst, ref readOffset);
            Assert.That(byteValue, Is.EqualTo(4));

            BufferWriter.WriteInt16(dst, -5, ref writeOffset);
            short shortValue = BufferReader.ReadInt16(dst, ref readOffset);
            Assert.That(writeOffset == readOffset);
            Assert.That(shortValue, Is.EqualTo(-5));

            BufferWriter.WriteUInt16(dst, 6, ref writeOffset);
            ushort ushortValue = BufferReader.ReadUInt16(dst, ref readOffset);
            Assert.That(ushortValue, Is.EqualTo(6));

            BufferWriter.WriteInt32(dst, -999, ref writeOffset);
            int intValue = BufferReader.ReadInt32(dst, ref readOffset);
            Assert.That(intValue, Is.EqualTo(-999));

            BufferWriter.WriteUInt32(dst, 998, ref writeOffset);
            uint uintValue = BufferReader.ReadUInt32(dst, ref readOffset);
            Assert.That(uintValue, Is.EqualTo(998));

            BufferWriter.WriteInt64(dst, -88551, ref writeOffset);
            long longValue = BufferReader.ReadInt64(dst, ref readOffset);
            Assert.That(writeOffset == readOffset);
            Assert.That(longValue, Is.EqualTo(-88551));

            BufferWriter.WriteUInt64(dst, 9988, ref writeOffset);
            ulong ulongValue = BufferReader.ReadUInt64(dst, ref readOffset);
            Assert.That(ulongValue, Is.EqualTo(9988));

            BufferWriter.WriteUTF8String(dst, "hello", ref writeOffset);
            string strValue = BufferReader.ReadUTF8String(dst, ref readOffset);
            Assert.That(strValue, Is.EqualTo("hello"));

            BufferWriter.WriteSingle(dst, -8.22f, ref writeOffset);
            float floatValue = BufferReader.ReadSingle(dst, ref readOffset);
            Assert.That(floatValue, Is.EqualTo(-8.22f));

            BufferWriter.WriteDouble(dst, 1155.221f, ref writeOffset);
            double doubleValue = BufferReader.ReadDouble(dst, ref readOffset);
            Assert.That(doubleValue, Is.EqualTo(1155.221f));

            BufferWriter.WriteInt8Arr(dst, new sbyte[3] { -1, -2, -3 }, ref writeOffset);
            sbyte[] sbyteArr = BufferReader.ReadInt8Arr(dst, ref readOffset);
            Assert.That(sbyteArr.Length, Is.EqualTo(3));
            Assert.That(sbyteArr[0], Is.EqualTo(-1));
            Assert.That(sbyteArr[1], Is.EqualTo(-2));
            Assert.That(sbyteArr[2], Is.EqualTo(-3));

            BufferWriter.WriteBoolArr(dst, new bool[5] { false, true, true, true, false }, ref writeOffset);
            bool[] boolArr = BufferReader.ReadBoolArr(dst, ref readOffset);
            Assert.That(boolArr[0], Is.EqualTo(false));
            Assert.That(boolArr[1], Is.EqualTo(true));
            Assert.That(boolArr[2], Is.EqualTo(true));
            Assert.That(boolArr[3], Is.EqualTo(true));
            Assert.That(boolArr[4], Is.EqualTo(false));

            BufferWriter.WriteUint8Arr(dst, new byte[4] { 3, 5, 6, 111 }, ref writeOffset);
            byte[] byteArr = BufferReader.ReadUInt8Arr(dst, ref readOffset);
            Assert.That(byteArr.Length, Is.EqualTo(4));
            Assert.That(byteArr[0], Is.EqualTo(3));
            Assert.That(byteArr[1], Is.EqualTo(5));
            Assert.That(byteArr[2], Is.EqualTo(6));
            Assert.That(byteArr[3], Is.EqualTo(111));

            BufferWriter.WriteInt16Arr(dst, new short[3] { 2, -8, 9 }, ref writeOffset);
            short[] shortArr = BufferReader.ReadInt16Arr(dst, ref readOffset);
            Assert.That(shortArr.Length, Is.EqualTo(3));
            Assert.That(shortArr[0], Is.EqualTo(2));
            Assert.That(shortArr[1], Is.EqualTo(-8));
            Assert.That(shortArr[2], Is.EqualTo(9));

            BufferWriter.WriteUInt16Arr(dst, new ushort[5] { 1, 877, 9993, 12, 23 }, ref writeOffset);
            ushort[] ushortArr = BufferReader.ReadUInt16Arr(dst, ref readOffset);
            Assert.That(ushortArr.Length, Is.EqualTo(5));
            Assert.That(ushortArr[0], Is.EqualTo(1));
            Assert.That(ushortArr[1], Is.EqualTo(877));
            Assert.That(ushortArr[2], Is.EqualTo(9993));
            Assert.That(ushortArr[3], Is.EqualTo(12));
            Assert.That(ushortArr[4], Is.EqualTo(23));

            BufferWriter.WriteInt32Arr(dst, new int[2] { -888888, 999999 }, ref writeOffset);
            int[] intArr = BufferReader.ReadInt32Arr(dst, ref readOffset);
            Assert.That(intArr.Length, Is.EqualTo(2));
            Assert.That(intArr[0], Is.EqualTo(-888888));
            Assert.That(intArr[1], Is.EqualTo(999999));

            BufferWriter.WriteUInt32Arr(dst, new uint[4] { 111, 222, 333, 444 }, ref writeOffset);
            uint[] uintArr = BufferReader.ReadUInt32Arr(dst, ref readOffset);
            Assert.That(uintArr.Length, Is.EqualTo(4));
            Assert.That(uintArr[0], Is.EqualTo(111));
            Assert.That(uintArr[1], Is.EqualTo(222));
            Assert.That(uintArr[2], Is.EqualTo(333));
            Assert.That(uintArr[3], Is.EqualTo(444));

            BufferWriter.WriteSingleArr(dst, new float[5] { -11.226f, 33.22f, 13.333333f, -8.1f, 9f }, ref writeOffset);
            float[] singleArr = BufferReader.ReadSingleArr(dst, ref readOffset);
            Assert.That(singleArr.Length, Is.EqualTo(5));
            Assert.That(singleArr[0], Is.EqualTo(-11.226f));
            Assert.That(singleArr[1], Is.EqualTo(33.22f));
            Assert.That(singleArr[2], Is.EqualTo(13.333333f));
            Assert.That(singleArr[3], Is.EqualTo(-8.1f));
            Assert.That(singleArr[4], Is.EqualTo(9f));

            BufferWriter.WriteInt64Arr(dst, new long[6] { 666, -887, -996, -997, -555, 555 }, ref writeOffset);
            long[] longArr = BufferReader.ReadInt64Arr(dst, ref readOffset);
            Assert.That(longArr.Length, Is.EqualTo(6));
            Assert.That(longArr[0], Is.EqualTo(666));
            Assert.That(longArr[1], Is.EqualTo(-887));
            Assert.That(longArr[2], Is.EqualTo(-996));
            Assert.That(longArr[3], Is.EqualTo(-997));
            Assert.That(longArr[4], Is.EqualTo(-555));
            Assert.That(longArr[5], Is.EqualTo(555));

            BufferWriter.WriteUInt64Arr(dst, new ulong[4] { 2, 1, 3, 6 }, ref writeOffset);
            ulong[] ulongArr = BufferReader.ReadUInt64Arr(dst, ref readOffset);
            Assert.That(ulongArr.Length, Is.EqualTo(4));
            Assert.That(ulongArr[0], Is.EqualTo(2));
            Assert.That(ulongArr[1], Is.EqualTo(1));
            Assert.That(ulongArr[2], Is.EqualTo(3));
            Assert.That(ulongArr[3], Is.EqualTo(6));

            BufferWriter.WriteDoubleArr(dst, new double[3] { 0.5555d, 85000d, -99.331d }, ref writeOffset);
            double[] doubleArr = BufferReader.ReadDoubleArr(dst, ref readOffset);
            Assert.That(doubleArr.Length, Is.EqualTo(3));
            Assert.That(doubleArr[0], Is.EqualTo(0.5555d));
            Assert.That(doubleArr[1], Is.EqualTo(85000d));
            Assert.That(doubleArr[2], Is.EqualTo(-99.331d));

            BufferWriter.WriteUTF8StringArr(dst, new string[4] { "h", "llo", "WWWWD", "-TT" }, ref writeOffset);
            string[] strArr = BufferReader.ReadUTF8StringArr(dst, ref readOffset);
            Assert.That(strArr.Length, Is.EqualTo(4));
            Assert.That(strArr[0], Is.EqualTo("h"));
            Assert.That(strArr[1], Is.EqualTo("llo"));
            Assert.That(strArr[2], Is.EqualTo("WWWWD"));
            Assert.That(strArr[3], Is.EqualTo("-TT"));

            byte[] newDst = new byte[2000];
            writeOffset = 0;
            readOffset = 0;
            BufferWriterExtra.WriteMessage(newDst, new HerModel() { value = 1 }, ref writeOffset);
            HerModel herModel = BufferReaderExtra.ReadMessage(newDst, () => new HerModel(), ref readOffset);
            Assert.That(writeOffset, Is.EqualTo(readOffset));
            Assert.That(herModel.value, Is.EqualTo(1));

        }

        [Test]
        public void TestVarint() {

            byte[] dst = new byte[1024];
            int writeOffset = 0;
            int readOffset = 0;

            sbyte[] sbyteArr = new sbyte[5] { 1, -1, -8, -125, 126 };
            for (int i = 0; i < sbyteArr.Length; i += 1) {
                sbyte w = sbyteArr[i];
                BufferWriter.WriteVarint(dst, (byte)w, ref writeOffset);
                sbyte r = (sbyte)BufferReader.ReadVarint(dst, ref readOffset);
                Assert.That(r, Is.EqualTo(w));
            }

            byte[] byteArr = new byte[5] { 1, 100, 127, 128, 254 };
            for (int i = 0; i < byteArr.Length; i += 1) {
                byte w = byteArr[i];
                BufferWriter.WriteVarint(dst, w, ref writeOffset);
                byte r = (byte)BufferReader.ReadVarint(dst, ref readOffset);
                Assert.That(r, Is.EqualTo(w));
            }

            int before = writeOffset;
            ulong maxw = 1 << 28 - 1;
            BufferWriter.WriteVarint(dst, maxw, ref writeOffset);
            ulong maxr = BufferReader.ReadVarint(dst, ref readOffset);
            int after = writeOffset;
            Assert.That(maxr, Is.EqualTo(maxw));
            Assert.That(after - before, Is.EqualTo(4));

            before = writeOffset;
            maxw = 1 << 28;
            BufferWriter.WriteVarint(dst, maxw, ref writeOffset);
            maxr = BufferReader.ReadVarint(dst, ref readOffset);
            after = writeOffset;
            Assert.That(maxr, Is.EqualTo(maxw));
            Assert.That(after - before, Is.EqualTo(5));

        }

    }

    public static class RandomExtention {

        public static long NextLong(this System.Random rd, long a, long b) {
            long myResult = a;
            long max = b, min = a;
            if (a > b) {
                max = a;
                min = b;
            }
            double Key = rd.NextDouble();
            myResult = min + (long)((max - min) * Key);
            return myResult;
        }

    }
}