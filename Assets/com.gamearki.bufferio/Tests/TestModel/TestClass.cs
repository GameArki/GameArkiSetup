using System;
using System.Linq;

namespace GameArki.BufferIO.Tests {

    public class TestClass {
        // All Base Types && String && Array
        public bool boolValue;
        public bool[] boolArray;
        public byte byteValue;
        public byte[] byteArray;
        public char charValue;
        public double doubleValue;
        public double[] doubleArray;
        public float floatValue;
        public float[] floatArray;
        public int intValue;
        public int[] intArray;
        public long longValue;
        public long[] longArray;
        public sbyte sbyteValue;
        public sbyte[] sbyteArray;
        public short shortValue;
        public short[] shortArray;
        public string stringValue;
        public string[] stringArray;
        public uint uintValue;
        public uint[] uintArray;
        public ulong ulongValue;
        public ulong[] ulongArray;
        public ushort ushortValue;
        public ushort[] ushortArray;

        public static TestClass GenRandom() {

            TestClass obj = new TestClass();

            obj.boolValue = UnityEngine.Random.Range(0, 2) == 1;
            obj.boolArray = Enumerable.Range(0, 10).Select(x => UnityEngine.Random.Range(0, 2) == 1).ToArray();

            obj.byteValue = (byte)UnityEngine.Random.Range(byte.MinValue, byte.MaxValue);
            obj.byteArray = Enumerable.Range(0, 10).Select(x => (byte)UnityEngine.Random.Range(byte.MinValue, byte.MaxValue)).ToArray();

            obj.charValue = (char)UnityEngine.Random.Range(char.MinValue, char.MaxValue);

            obj.doubleValue = (double)UnityEngine.Random.Range(float.MinValue, float.MaxValue);
            obj.doubleArray = Enumerable.Range(0, 10).Select(x => (double)UnityEngine.Random.Range(float.MinValue, float.MaxValue)).ToArray();

            obj.floatValue = (float)UnityEngine.Random.Range(float.MinValue, float.MaxValue);
            obj.floatArray = Enumerable.Range(0, 10).Select(x => (float)UnityEngine.Random.Range(float.MinValue, float.MaxValue)).ToArray();

            obj.intValue = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            obj.intArray = Enumerable.Range(0, 10).Select(x => UnityEngine.Random.Range(int.MinValue, int.MaxValue)).ToArray();

            obj.longValue = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            obj.longArray = Enumerable.Range(0, 10).Select(x => (long)UnityEngine.Random.Range(int.MinValue, int.MaxValue)).ToArray();

            obj.sbyteValue = (sbyte)UnityEngine.Random.Range(sbyte.MinValue, sbyte.MaxValue);
            obj.sbyteArray = Enumerable.Range(0, 10).Select(x => (sbyte)UnityEngine.Random.Range(sbyte.MinValue, sbyte.MaxValue)).ToArray();

            obj.shortValue = (short)UnityEngine.Random.Range(short.MinValue, short.MaxValue);
            obj.shortArray = Enumerable.Range(0, 10).Select(x => (short)UnityEngine.Random.Range(short.MinValue, short.MaxValue)).ToArray();

            obj.stringValue = Guid.NewGuid().ToString();
            obj.stringArray = Enumerable.Range(0, 10).Select(x => Guid.NewGuid().ToString()).ToArray();

            obj.uintValue = (uint)UnityEngine.Random.Range(uint.MinValue, uint.MaxValue);
            obj.uintArray = Enumerable.Range(0, 10).Select(x => (uint)UnityEngine.Random.Range(uint.MinValue, uint.MaxValue)).ToArray();

            obj.ulongValue = (ulong)UnityEngine.Random.Range(ulong.MinValue, ulong.MaxValue);
            obj.ulongArray = Enumerable.Range(0, 10).Select(x => (ulong)UnityEngine.Random.Range(ulong.MinValue, ulong.MaxValue)).ToArray();

            obj.ushortValue = (ushort)UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue);
            obj.ushortArray = Enumerable.Range(0, 10).Select(x => (ushort)UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue)).ToArray();

            return obj;

        }
    }

    public class TestStruct {
        // All Base Types && String && Array
        public bool boolValue;
        public bool[] boolArray;
        public byte byteValue;
        public byte[] byteArray;
        public char charValue;
        public double doubleValue;
        public double[] doubleArray;
        public float floatValue;
        public float[] floatArray;
        public int intValue;
        public int[] intArray;
        public long longValue;
        public long[] longArray;
        public sbyte sbyteValue;
        public sbyte[] sbyteArray;
        public short shortValue;
        public short[] shortArray;
        public string stringValue;
        public string[] stringArray;
        public uint uintValue;
        public uint[] uintArray;
        public ulong ulongValue;
        public ulong[] ulongArray;
        public ushort ushortValue;
        public ushort[] ushortArray;

        public static TestStruct GenRandom() {

            TestStruct obj = new TestStruct();

            obj.boolValue = UnityEngine.Random.Range(0, 2) == 1;
            obj.boolArray = Enumerable.Range(0, 10).Select(x => UnityEngine.Random.Range(0, 2) == 1).ToArray();

            obj.byteValue = (byte)UnityEngine.Random.Range(byte.MinValue, byte.MaxValue);
            obj.byteArray = Enumerable.Range(0, 10).Select(x => (byte)UnityEngine.Random.Range(byte.MinValue, byte.MaxValue)).ToArray();

            obj.charValue = (char)UnityEngine.Random.Range(char.MinValue, char.MaxValue);

            obj.doubleValue = (double)UnityEngine.Random.Range(float.MinValue, float.MaxValue);
            obj.doubleArray = Enumerable.Range(0, 10).Select(x => (double)UnityEngine.Random.Range(float.MinValue, float.MaxValue)).ToArray();

            obj.floatValue = (float)UnityEngine.Random.Range(float.MinValue, float.MaxValue);
            obj.floatArray = Enumerable.Range(0, 10).Select(x => (float)UnityEngine.Random.Range(float.MinValue, float.MaxValue)).ToArray();

            obj.intValue = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            obj.intArray = Enumerable.Range(0, 10).Select(x => UnityEngine.Random.Range(int.MinValue, int.MaxValue)).ToArray();

            obj.longValue = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            obj.longArray = Enumerable.Range(0, 10).Select(x => (long)UnityEngine.Random.Range(int.MinValue, int.MaxValue)).ToArray();

            obj.sbyteValue = (sbyte)UnityEngine.Random.Range(sbyte.MinValue, sbyte.MaxValue);
            obj.sbyteArray = Enumerable.Range(0, 10).Select(x => (sbyte)UnityEngine.Random.Range(sbyte.MinValue, sbyte.MaxValue)).ToArray();

            obj.shortValue = (short)UnityEngine.Random.Range(short.MinValue, short.MaxValue);
            obj.shortArray = Enumerable.Range(0, 10).Select(x => (short)UnityEngine.Random.Range(short.MinValue, short.MaxValue)).ToArray();

            obj.stringValue = Guid.NewGuid().ToString();
            obj.stringArray = Enumerable.Range(0, 10).Select(x => Guid.NewGuid().ToString()).ToArray();

            obj.uintValue = (uint)UnityEngine.Random.Range(uint.MinValue, uint.MaxValue);
            obj.uintArray = Enumerable.Range(0, 10).Select(x => (uint)UnityEngine.Random.Range(uint.MinValue, uint.MaxValue)).ToArray();

            obj.ulongValue = (ulong)UnityEngine.Random.Range(ulong.MinValue, ulong.MaxValue);
            obj.ulongArray = Enumerable.Range(0, 10).Select(x => (ulong)UnityEngine.Random.Range(ulong.MinValue, ulong.MaxValue)).ToArray();

            obj.ushortValue = (ushort)UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue);
            obj.ushortArray = Enumerable.Range(0, 10).Select(x => (ushort)UnityEngine.Random.Range(ushort.MinValue, ushort.MaxValue)).ToArray();

            return obj;

        }
    }


}