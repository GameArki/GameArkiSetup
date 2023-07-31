using System;
using System.Runtime.InteropServices;
using NUnit.Framework;
using RD = UnityEngine.Random;
using GameArki.NativeBytes;

namespace GameArki.NoBuf.Tests {

    public class Test_NoBuf_Message {

        readonly static Random random = new Random();

        // [Test]
        public void Test_Message() {
            byte[] dst = new byte[1024];
            int offset = 0;
            
            TestMessage message = new TestMessage();
            message.boolValue = true;
            message.byteValue = byte.MaxValue;
            message.sbyteValue = sbyte.MaxValue;
            message.charValue = char.MaxValue;
            message.ushortValue = ushort.MaxValue;
            message.shortValue = short.MaxValue;
            message.uintValue = uint.MaxValue;
            message.intValue = int.MaxValue;
            message.ulongValue = ulong.MaxValue;
            message.longValue = long.MaxValue;
            message.floatValue = float.MaxValue;
            message.doubleValue = double.MaxValue;
            message.decimalValue = decimal.MaxValue;

            message.boolArray = new NBArray<bool>(3);
            message.boolArray[0] = true;
            message.boolArray[1] = false;
            message.boolArray[2] = true;

            message.byteArray = new NBArray<byte>(3);
            message.byteArray[0] = 1;
            message.byteArray[1] = 2;
            message.byteArray[2] = 3;

            message.sbyteArray = new NBArray<sbyte>(3);
            message.sbyteArray[0] = 1;
            message.sbyteArray[1] = 2;
            message.sbyteArray[2] = 3;

            message.charArray = new NBArray<char>(3);
            message.charArray[0] = 'a';
            message.charArray[1] = 'b';
            message.charArray[2] = 'c';

            message.ushortArray = new NBArray<ushort>(3);
            message.ushortArray[0] = 1;
            message.ushortArray[1] = 2;
            message.ushortArray[2] = 3;

            message.shortArray = new NBArray<short>(3);
            message.shortArray[0] = 1;
            message.shortArray[1] = 2;
            message.shortArray[2] = 3;

            message.uintArray = new NBArray<uint>(3);
            message.uintArray[0] = 1;
            message.uintArray[1] = 2;
            message.uintArray[2] = 3;

            message.intArray = new NBArray<int>(3);
            message.intArray[0] = 1;
            message.intArray[1] = 2;
            message.intArray[2] = 3;

            message.ulongArray = new NBArray<ulong>(3);
            message.ulongArray[0] = 1;
            message.ulongArray[1] = 2;
            message.ulongArray[2] = 3;

            message.longArray = new NBArray<long>(3);
            message.longArray[0] = 1;
            message.longArray[1] = 2;
            message.longArray[2] = 3;

            message.floatArray = new NBArray<float>(3);
            message.floatArray[0] = 1;
            message.floatArray[1] = 2;
            message.floatArray[2] = 3;

            message.doubleArray = new NBArray<double>(3);
            message.doubleArray[0] = 1;
            message.doubleArray[1] = 2;
            message.doubleArray[2] = 3;

            message.decimalArray = new NBArray<decimal>(3);
            message.decimalArray[0] = 1;
            message.decimalArray[1] = 2;
            message.decimalArray[2] = 3;

            message.stringValue = new NBString("Hello World");

            message.WriteTo(dst, ref offset);

            byte[] src = new byte[1024];
            for (int i = 0; i < src.Length; i++) {
                src[i] = dst[i];
            }

            offset = 0;
            message.FromBytes(src, ref offset);

            // Assert
            Assert.AreEqual(true, message.boolValue);
            Assert.AreEqual(byte.MaxValue, message.byteValue);
            Assert.AreEqual(sbyte.MaxValue, message.sbyteValue);
            Assert.AreEqual(char.MaxValue, message.charValue);
            Assert.AreEqual(ushort.MaxValue, message.ushortValue);
            Assert.AreEqual(short.MaxValue, message.shortValue);
            Assert.AreEqual(uint.MaxValue, message.uintValue);
            Assert.AreEqual(int.MaxValue, message.intValue);
            Assert.AreEqual(ulong.MaxValue, message.ulongValue);
            Assert.AreEqual(long.MaxValue, message.longValue);
            Assert.AreEqual(float.MaxValue, message.floatValue);
            Assert.AreEqual(double.MaxValue, message.doubleValue);
            Assert.AreEqual(decimal.MaxValue, message.decimalValue);

            Assert.AreEqual(3, message.boolArray.Length);
            Assert.AreEqual(true, message.boolArray[0]);
            Assert.AreEqual(false, message.boolArray[1]);
            Assert.AreEqual(true, message.boolArray[2]);

            Assert.AreEqual(3, message.byteArray.Length);
            Assert.AreEqual(1, message.byteArray[0]);
            Assert.AreEqual(2, message.byteArray[1]);
            Assert.AreEqual(3, message.byteArray[2]);

            Assert.AreEqual(3, message.sbyteArray.Length);
            Assert.AreEqual(1, message.sbyteArray[0]);
            Assert.AreEqual(2, message.sbyteArray[1]);
            Assert.AreEqual(3, message.sbyteArray[2]);

            Assert.AreEqual(3, message.charArray.Length);
            Assert.AreEqual('a', message.charArray[0]);
            Assert.AreEqual('b', message.charArray[1]);
            Assert.AreEqual('c', message.charArray[2]);

            Assert.AreEqual(3, message.ushortArray.Length);
            Assert.AreEqual(1, message.ushortArray[0]);
            Assert.AreEqual(2, message.ushortArray[1]);
            Assert.AreEqual(3, message.ushortArray[2]);

            Assert.AreEqual(3, message.shortArray.Length);
            Assert.AreEqual(1, message.shortArray[0]);
            Assert.AreEqual(2, message.shortArray[1]);
            Assert.AreEqual(3, message.shortArray[2]);

            Assert.AreEqual(3, message.uintArray.Length);
            Assert.AreEqual(1, message.uintArray[0]);
            Assert.AreEqual(2, message.uintArray[1]);
            Assert.AreEqual(3, message.uintArray[2]);

            Assert.AreEqual(3, message.intArray.Length);
            Assert.AreEqual(1, message.intArray[0]);
            Assert.AreEqual(2, message.intArray[1]);
            Assert.AreEqual(3, message.intArray[2]);

            Assert.AreEqual(3, message.ulongArray.Length);
            Assert.AreEqual(1, message.ulongArray[0]);
            Assert.AreEqual(2, message.ulongArray[1]);
            Assert.AreEqual(3, message.ulongArray[2]);

            Assert.AreEqual(3, message.longArray.Length);
            Assert.AreEqual(1, message.longArray[0]);
            Assert.AreEqual(2, message.longArray[1]);
            Assert.AreEqual(3, message.longArray[2]);

            Assert.AreEqual(3, message.floatArray.Length);
            Assert.AreEqual(1, message.floatArray[0]);
            Assert.AreEqual(2, message.floatArray[1]);
            Assert.AreEqual(3, message.floatArray[2]);

            Assert.AreEqual(3, message.doubleArray.Length);
            Assert.AreEqual(1, message.doubleArray[0]);
            Assert.AreEqual(2, message.doubleArray[1]);
            Assert.AreEqual(3, message.doubleArray[2]);

            Assert.AreEqual(3, message.decimalArray.Length);
            Assert.AreEqual(1, message.decimalArray[0]);
            Assert.AreEqual(2, message.decimalArray[1]);
            Assert.AreEqual(3, message.decimalArray[2]);

            Assert.AreEqual("Hello World", message.stringValue.GetString());

            // Release
            message.Dispose();

        }

    }

}