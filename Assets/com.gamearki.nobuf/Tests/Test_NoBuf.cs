using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using GameArki.NativeBytes;

namespace GameArki.NoBuf.Tests {

    public unsafe class Test_NoBuf {

        readonly static Random random = new Random();

        [Test]
        public void Test_Nothing() {

            var clientBaker = new NBBakerClient();
            clientBaker.Initialize(100, 1024);
            clientBaker.Register<TestMessage>(1, 2, () => new TestMessage(), (msg) => {
                UnityEngine.Debug.Log($"TestMessage: {msg.boolValue}");
            });

            var serverBaker = new NBBakerServer();
            serverBaker.Initialize(100, 1024);
            serverBaker.Register<TestMessage>(1, 2, () => new TestMessage(), (connID, msg) => {
                UnityEngine.Debug.Log($"TestMessage: {msg.boolValue}");
            });

            TestMessage testMessage = new TestMessage();

            // 1. bake
            ArraySegment<byte> data = clientBaker.Bake_Take(1, 2, testMessage);

            // 2. send
            clientBaker.Trigger(data.Array);
            serverBaker.Trigger(0, data.Array);

            // 3. return
            clientBaker.Bake_ReturnAfterSend(data.Array);

            testMessage.Dispose();

        }

        [Test]
        public void Test_RW_U8([NUnit.Framework.Range(byte.MinValue, byte.MaxValue)] byte valueWrite) {
            Span<byte> dst = stackalloc byte[1];
            int offset = 0;
            NBWriter.W_U8(dst, valueWrite, ref offset);
            Assert.AreEqual(1, offset);

            offset = 0;
            byte valueRead = NBReader.R_U8(dst, ref offset);
            Assert.AreEqual(1, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_U16([NUnit.Framework.Random(ushort.MinValue, ushort.MaxValue, 100)] ushort valueWrite) {
            Span<byte> dst = stackalloc byte[2];
            int offset = 0;
            NBWriter.W_U16(dst, valueWrite, ref offset);
            Assert.AreEqual(2, offset);

            offset = 0;
            ushort valueRead = NBReader.R_U16(dst, ref offset);
            Assert.AreEqual(2, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_U32([NUnit.Framework.Random(uint.MinValue, uint.MaxValue, 100)] uint valueWrite) {
            Span<byte> dst = stackalloc byte[4];
            int offset = 0;
            NBWriter.W_U32(dst, valueWrite, ref offset);
            Assert.AreEqual(4, offset);

            offset = 0;
            uint valueRead = NBReader.R_U32(dst, ref offset);
            Assert.AreEqual(4, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_U64([NUnit.Framework.Random(ulong.MinValue, ulong.MaxValue, 100)] ulong valueWrite) {
            Span<byte> dst = stackalloc byte[8];
            int offset = 0;
            NBWriter.W_U64(dst, valueWrite, ref offset);
            Assert.AreEqual(8, offset);

            offset = 0;
            ulong valueRead = NBReader.R_U64(dst, ref offset);
            Assert.AreEqual(8, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_I8([NUnit.Framework.Range(sbyte.MinValue, sbyte.MaxValue)] sbyte valueWrite) {
            Span<byte> dst = stackalloc byte[1];
            int offset = 0;
            NBWriter.W_I8(dst, valueWrite, ref offset);
            Assert.AreEqual(1, offset);

            offset = 0;
            sbyte valueRead = NBReader.R_I8(dst, ref offset);
            Assert.AreEqual(1, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_I16([NUnit.Framework.Random(short.MinValue, short.MaxValue, 100)] short valueWrite) {
            Span<byte> dst = stackalloc byte[2];
            int offset = 0;
            NBWriter.W_I16(dst, valueWrite, ref offset);
            Assert.AreEqual(2, offset);

            offset = 0;
            short valueRead = NBReader.R_I16(dst, ref offset);
            Assert.AreEqual(2, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_I32([NUnit.Framework.Random(int.MinValue, int.MaxValue, 100)] int valueWrite) {
            Span<byte> dst = stackalloc byte[4];
            int offset = 0;
            NBWriter.W_I32(dst, valueWrite, ref offset);
            Assert.AreEqual(4, offset);

            offset = 0;
            int valueRead = NBReader.R_I32(dst, ref offset);
            Assert.AreEqual(4, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_I64([NUnit.Framework.Random(long.MinValue, long.MaxValue, 100)] long valueWrite) {
            Span<byte> dst = stackalloc byte[8];
            int offset = 0;
            NBWriter.W_I64(dst, valueWrite, ref offset);
            Assert.AreEqual(8, offset);

            offset = 0;
            long valueRead = NBReader.R_I64(dst, ref offset);
            Assert.AreEqual(8, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_F32([NUnit.Framework.Random(float.MinValue, float.MaxValue, 100)] float valueWrite) {
            Span<byte> dst = stackalloc byte[sizeof(float)];
            int offset = 0;
            NBWriter.W_F32(dst, valueWrite, ref offset);
            Assert.AreEqual(4, offset);

            offset = 0;
            float valueRead = NBReader.R_F32(dst, ref offset);
            Assert.AreEqual(4, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_F64([NUnit.Framework.Random(double.MinValue, double.MaxValue, 100)] double valueWrite) {
            Span<byte> dst = stackalloc byte[sizeof(double)];
            int offset = 0;
            NBWriter.W_F64(dst, valueWrite, ref offset);
            Assert.AreEqual(8, offset);

            offset = 0;
            double valueRead = NBReader.R_F64(dst, ref offset);
            Assert.AreEqual(8, offset);
            Assert.AreEqual(valueWrite, valueRead);
        }

        [Test]
        public void Test_RW_F128([NUnit.Framework.Random(ulong.MinValue, ulong.MaxValue, 100)] ulong u1) {
            Span<byte> dst = stackalloc byte[sizeof(decimal)];
            int offset = 0;
            int i1 = random.Next(int.MinValue, int.MaxValue);
            int i2 = random.Next(int.MinValue, int.MaxValue);
            ulong u2 = (uint)i1 | ((ulong)(uint)i2 << 32);
            NBDecimalContent valueWrite = new NBDecimalContent() { i1 = u1, i2 = u2 };
            NBWriter.W_F128(dst, valueWrite.f, ref offset);
            Assert.AreEqual(16, offset);

            offset = 0;
            decimal valueRead = NBReader.R_F128(dst, ref offset);
            Assert.AreEqual(16, offset);
            Assert.AreEqual(valueWrite.f, valueRead);
        }

        [Test]
        public void Test_RW_Bool([NUnit.Framework.Range(0, 1)] byte valueWrite) {
            Span<byte> dst = stackalloc byte[1];
            int offset = 0;
            NBWriter.W_Bool(dst, valueWrite == 1, ref offset);
            Assert.AreEqual(1, offset);

            offset = 0;
            bool valueRead = NBReader.R_Bool(dst, ref offset);
            Assert.AreEqual(1, offset);
            Assert.AreEqual(valueWrite, valueRead ? 1 : 0);
        }

        [Test]
        public void Test_RW_BoolArr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[1024];

            int offset = 0;
            ReadOnlySpan<bool> valueWriteSrc = stackalloc bool[length];
            NBArray<bool> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = UnityEngine.Random.Range(0, 2) == 1;
            }
            NBWriter.W_BoolArr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_BoolArr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_U8Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(byte) + 2];

            int offset = 0;
            ReadOnlySpan<byte> valueWriteSrc = stackalloc byte[length];
            NBArray<byte> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (byte)UnityEngine.Random.Range(byte.MinValue, byte.MaxValue);
            }
            NBWriter.W_U8Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_U8Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_U16Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(ushort) + 2];

            int offset = 0;
            ReadOnlySpan<ushort> valueWriteSrc = stackalloc ushort[length];
            NBArray<ushort> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (ushort)UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue);
            }
            NBWriter.W_U16Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_U16Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_U32Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(uint) + 2];

            int offset = 0;
            ReadOnlySpan<uint> valueWriteSrc = stackalloc uint[length];
            NBArray<uint> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (uint)UnityEngine.Random.Range(uint.MinValue, uint.MaxValue);
            }
            NBWriter.W_U32Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_U32Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_U64Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(ulong) + 2];

            int offset = 0;
            ReadOnlySpan<ulong> valueWriteSrc = stackalloc ulong[length];
            NBArray<ulong> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (ulong)UnityEngine.Random.Range(ulong.MinValue, ulong.MaxValue);
            }
            NBWriter.W_U64Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_U64Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_I8Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(sbyte) + 2];

            int offset = 0;
            ReadOnlySpan<sbyte> valueWriteSrc = stackalloc sbyte[length];
            NBArray<sbyte> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (sbyte)UnityEngine.Random.Range(sbyte.MinValue, sbyte.MaxValue);
            }
            NBWriter.W_I8Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_I8Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_I16Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(short) + 2];

            int offset = 0;
            ReadOnlySpan<short> valueWriteSrc = stackalloc short[length];
            NBArray<short> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (short)UnityEngine.Random.Range(short.MinValue, short.MaxValue);
            }
            NBWriter.W_I16Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_I16Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_I32Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(int) + 2];

            int offset = 0;
            ReadOnlySpan<int> valueWriteSrc = stackalloc int[length];
            NBArray<int> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (int)UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            }
            NBWriter.W_I32Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_I32Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_I64Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(long) + 2];

            int offset = 0;
            ReadOnlySpan<long> valueWriteSrc = stackalloc long[length];
            NBArray<long> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = (long)UnityEngine.Random.Range(long.MinValue, long.MaxValue);
            }
            NBWriter.W_I64Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_I64Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_F32Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(float) + 2];

            int offset = 0;
            ReadOnlySpan<float> valueWriteSrc = stackalloc float[length];
            NBArray<float> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                valueWrite[i] = UnityEngine.Random.Range(float.MinValue, float.MaxValue);
            }
            NBWriter.W_F32Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_F32Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_F64Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(double) + 2];

            int offset = 0;
            ReadOnlySpan<double> valueWriteSrc = stackalloc double[length];
            NBArray<double> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                int i1 = random.Next(int.MinValue, int.MaxValue);
                int i2 = random.Next(int.MinValue, int.MaxValue);
                ulong u1 = (ulong)((uint)i1 << 32 | (uint)i2);
                NBDoubleContent content = new NBDoubleContent() { i = u1 };
                valueWrite[i] = content.f;
            }
            NBWriter.W_F64Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_F64Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_F128Arr([NUnit.Framework.Range(1, 100)] int length) {

            Span<byte> dst = stackalloc byte[length * sizeof(decimal) + 2];

            int offset = 0;
            ReadOnlySpan<decimal> valueWriteSrc = stackalloc decimal[length];
            NBArray<decimal> valueWrite = valueWriteSrc;
            for (int i = 0; i < length; i++) {
                int i1 = random.Next(int.MinValue, int.MaxValue);
                int i2 = random.Next(int.MinValue, int.MaxValue);
                int i3 = random.Next(int.MinValue, int.MaxValue);
                int i4 = random.Next(int.MinValue, int.MaxValue);
                ulong u1 = (ulong)((uint)i1 << 32 | (uint)i2);
                ulong u2 = (ulong)((uint)i3 << 32 | (uint)i4);
                NBDecimalContent content = new NBDecimalContent() { i1 = u1, i2 = u2 };
                valueWrite[i] = content.f;
            }
            NBWriter.W_F128Arr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_F128Arr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            int sameCount = 0;
            for (int i = 0; i < valueWrite.Length; i++) {
                if (valueWrite[i] == valueRead[i]) {
                    sameCount++;
                }
            }
            valueRead.Dispose();
            Assert.AreEqual(valueWrite.Length, sameCount);
        }

        [Test]
        public void Test_RW_String([NUnit.Framework.Range(0, 16)] int index) {

            Span<byte> dst = stackalloc byte[1024];
            int offset = 0;
            string valueWrite = helloArr[index];
            NBString nbStr = new NBString(valueWrite);
            NBWriter.W_NBString(dst, nbStr, ref offset);
            int lastOffset = offset;

            offset = 0;
            NBString valueRead = NBReader.R_String(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite, valueRead.GetString());
            valueRead.Dispose();
        }

        [Test]
        public void Test_RW_StringArr() {

            Span<byte> dst = stackalloc byte[1024];
            int offset = 0;
            int length = random.Next(1, helloArr.Length);
            NBArray<NBString> valueWrite = new NBArray<NBString>(length);
            for (int i = 0; i < length; i++) {
                valueWrite[i] = new NBString(RandomString());
            }
            NBWriter.W_StringArr(dst, valueWrite, ref offset);
            int lastOffset = offset;

            offset = 0;
            var valueRead = NBReader.R_StringArr(dst, ref offset);
            Assert.AreEqual(lastOffset, offset);
            Assert.AreEqual(valueWrite.Length, valueRead.Length);
            for (int i = 0; i < valueWrite.Length; i++) {
                Assert.AreEqual(valueWrite[i].GetString(), valueRead[i].GetString());
                Assert.AreEqual(valueWrite[i].BytesToHash(), valueRead[i].BytesToHash());
                valueWrite[i].Dispose();
            }
            valueRead.Dispose();
        }

        // 各国hello
        static string[] helloArr = {
            null,
            "",
            "Hello, world!",
            "こんにちは世界！",
            "你好，世界！",
            "안녕하세요 세계!",
            "Привет, мир!",
            "Bonjour le monde!",
            "Hallo Welt!",
            "Ciao mondo!",
            "Hola Mundo!",
            "Olá Mundo!",
            "Hej världen!",
            "Hei maailma!",
            "Halló heimur!",
            "Witaj świecie!",
            "Прывітанне Сусвет!",
        };
        unsafe string RandomString() {
            return helloArr[random.Next(helloArr.Length)];
        }


    }

}