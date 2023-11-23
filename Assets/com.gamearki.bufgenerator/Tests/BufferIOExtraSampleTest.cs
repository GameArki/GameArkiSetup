using System.Collections.Generic;
using NUnit.Framework;
using GameArki.BufferIO.Sample;

namespace GameArki.BufferIO.Tests {

    public class BufferIOExtraSampleTest {

        [Test]
        public void TestRun() {

            // ==== Write ====
            MyModel myModel = new MyModel();

            myModel.boolValue = true;
            myModel.boolArr = new bool[2] { true, false };
            myModel.boolList = new List<bool>() { true, false, true };

            myModel.byteValue = 5;
            myModel.byteArr = new byte[3] { 1, 2, 9 };
            myModel.byteList = new List<byte>() { 1, 2, 9 };

            myModel.sbyteValue = -85;
            myModel.sbyteArr = new sbyte[4] { -1, -1, -2, -8 };
            myModel.sbyteList = new List<sbyte>() { -1, -1, -2, -8 };

            myModel.shortValue = -88;
            myModel.shortArr = new short[3] { -1, -2, -8 };
            myModel.shortList = new List<short>() { -1, -2, -8 };

            myModel.ushortValue = 442;
            myModel.ushortArr = new ushort[1] { 222 };
            myModel.ushortList = new List<ushort>() { 222 };

            myModel.intValue = -32131;
            myModel.intArr = new int[2] { -32131, 32131 };
            myModel.intList = new List<int>() { -32131, 32131 };

            myModel.uintValue = 9988888;
            myModel.uintArr = new uint[2] { 9988888, 9988888 };
            myModel.uintList = new List<uint>() { 9988888, 9988888 };

            myModel.longValue = -999991;
            myModel.longArr = new long[2] { -999991, 999991 };
            myModel.longList = new List<long>() { -999991, 999991 };

            myModel.ulongValue = 11122333;
            myModel.ulongArr = new ulong[2] { 111, 222222333 };
            myModel.ulongList = new List<ulong>() { 111, 222222333 };

            myModel.floatValue = -1.00000000001f;
            myModel.floatArr = new float[2] { -1.0000002f, 1.0003f };
            myModel.floatList = new List<float>() { -1.0000002f, 1.0003f };

            myModel.doubleValue = 4541.12321333f;
            myModel.doubleArr = new double[2] { 4541.12321333f, 4541.12321333f };
            myModel.doubleList = new List<double>() { 4541.12321333f, 4541.12321333f };

            myModel.charValue = 'D';

            myModel.strValue = "hello worlddd";
            myModel.strArr = new string[2] { "hl", "ddd" };
            myModel.strList = new List<string>() { "hl", "ddd" };

            myModel.herModel = new HerModel() { name = "yo", value = 3 };
            myModel.herModelArr = new HerModel[2] {
                new HerModel{ name ="ho112", value = 4},
                new HerModel{ value = -99}
            };
            myModel.herModelList = new List<HerModel>() {
                new HerModel{ name ="ho112", value = 4},
                new HerModel{ value = -99}
            };

            myModel.otherStr = "endall";

            byte[] data = new byte[2048];
            int offset = 0;
            myModel.WriteTo(data, ref offset);

            // ==== Read ====
            MyModel newModel = new MyModel();
            offset = 0;
            newModel.FromBytes(data, ref offset);

            // - bool
            Assert.AreEqual(myModel.boolValue, newModel.boolValue);
            Assert.AreEqual(myModel.boolArr.Length, newModel.boolArr.Length);
            for (int i = 0; i < myModel.boolArr.Length; i += 1) {
                Assert.AreEqual(myModel.boolArr[i], newModel.boolArr[i]);
            }
            Assert.AreEqual(myModel.boolList.Count, newModel.boolList.Count);
            for (int i = 0; i < myModel.boolList.Count; i += 1) {
                Assert.AreEqual(myModel.boolList[i], newModel.boolList[i]);
            }

            // - byte
            Assert.AreEqual(myModel.byteValue, newModel.byteValue);
            Assert.AreEqual(myModel.byteArr.Length, newModel.byteArr.Length);
            for (int i = 0; i < myModel.byteArr.Length; i += 1) {
                Assert.AreEqual(myModel.byteArr[i], newModel.byteArr[i]);
            }
            Assert.AreEqual(myModel.byteList.Count, newModel.byteList.Count);
            for (int i = 0; i < myModel.byteList.Count; i += 1) {
                Assert.AreEqual(myModel.byteList[i], newModel.byteList[i]);
            }

            // - sbyte
            Assert.AreEqual(myModel.sbyteValue, newModel.sbyteValue);
            Assert.AreEqual(myModel.sbyteArr.Length, newModel.sbyteArr.Length);
            for (int i = 0; i < myModel.sbyteArr.Length; i += 1) {
                Assert.AreEqual(myModel.sbyteArr[i], newModel.sbyteArr[i]);
            }
            Assert.AreEqual(myModel.sbyteList.Count, newModel.sbyteList.Count);
            for (int i = 0; i < myModel.sbyteList.Count; i += 1) {
                Assert.AreEqual(myModel.sbyteList[i], newModel.sbyteList[i]);
            }

            // - short
            Assert.AreEqual(myModel.shortValue, newModel.shortValue);
            Assert.AreEqual(myModel.shortArr.Length, newModel.shortArr.Length);
            for (int i = 0; i < myModel.shortArr.Length; i += 1) {
                Assert.AreEqual(myModel.shortArr[i], newModel.shortArr[i]);
            }
            Assert.AreEqual(myModel.shortList.Count, newModel.shortList.Count);
            for (int i = 0; i < myModel.shortList.Count; i += 1) {
                Assert.AreEqual(myModel.shortList[i], newModel.shortList[i]);
            }

            // - ushort
            Assert.AreEqual(myModel.ushortValue, newModel.ushortValue);
            Assert.AreEqual(myModel.ushortArr.Length, newModel.ushortArr.Length);
            for (int i = 0; i < myModel.ushortArr.Length; i += 1) {
                Assert.AreEqual(myModel.ushortArr[i], newModel.ushortArr[i]);
            }
            Assert.AreEqual(myModel.ushortList.Count, newModel.ushortList.Count);
            for (int i = 0; i < myModel.ushortList.Count; i += 1) {
                Assert.AreEqual(myModel.ushortList[i], newModel.ushortList[i]);
            }

            // - int
            Assert.AreEqual(myModel.intValue, newModel.intValue);
            Assert.AreEqual(myModel.intArr.Length, newModel.intArr.Length);
            for (int i = 0; i < myModel.intArr.Length; i += 1) {
                Assert.AreEqual(myModel.intArr[i], newModel.intArr[i]);
            }
            Assert.AreEqual(myModel.intList.Count, newModel.intList.Count);
            for (int i = 0; i < myModel.intList.Count; i += 1) {
                Assert.AreEqual(myModel.intList[i], newModel.intList[i]);
            }

            // - uint
            Assert.AreEqual(myModel.uintValue, newModel.uintValue);
            Assert.AreEqual(myModel.uintArr.Length, newModel.uintArr.Length);
            for (int i = 0; i < myModel.uintArr.Length; i += 1) {
                Assert.AreEqual(myModel.uintArr[i], newModel.uintArr[i]);
            }
            Assert.AreEqual(myModel.uintList.Count, newModel.uintList.Count);
            for (int i = 0; i < myModel.uintList.Count; i += 1) {
                Assert.AreEqual(myModel.uintList[i], newModel.uintList[i]);
            }

            // - long
            Assert.AreEqual(myModel.longValue, newModel.longValue);
            Assert.AreEqual(myModel.longArr.Length, newModel.longArr.Length);
            for (int i = 0; i < myModel.longArr.Length; i += 1) {
                Assert.AreEqual(myModel.longArr[i], newModel.longArr[i]);
            }
            Assert.AreEqual(myModel.longList.Count, newModel.longList.Count);
            for (int i = 0; i < myModel.longList.Count; i += 1) {
                Assert.AreEqual(myModel.longList[i], newModel.longList[i]);
            }

            // - ulong
            Assert.AreEqual(myModel.ulongValue, newModel.ulongValue);
            Assert.AreEqual(myModel.ulongArr.Length, newModel.ulongArr.Length);
            for (int i = 0; i < myModel.ulongArr.Length; i += 1) {
                Assert.AreEqual(myModel.ulongArr[i], newModel.ulongArr[i]);
            }
            Assert.AreEqual(myModel.ulongList.Count, newModel.ulongList.Count);
            for (int i = 0; i < myModel.ulongList.Count; i += 1) {
                Assert.AreEqual(myModel.ulongList[i], newModel.ulongList[i]);
            }

            // - float
            Assert.AreEqual(myModel.floatValue, newModel.floatValue);
            Assert.AreEqual(myModel.floatArr.Length, newModel.floatArr.Length);
            for (int i = 0; i < myModel.floatArr.Length; i += 1) {
                Assert.AreEqual(myModel.floatArr[i], newModel.floatArr[i]);
            }
            Assert.AreEqual(myModel.floatList.Count, newModel.floatList.Count);
            for (int i = 0; i < myModel.floatList.Count; i += 1) {
                Assert.AreEqual(myModel.floatList[i], newModel.floatList[i]);
            }

            // - double
            Assert.AreEqual(myModel.doubleValue, newModel.doubleValue);
            Assert.AreEqual(myModel.doubleArr.Length, newModel.doubleArr.Length);
            for (int i = 0; i < myModel.doubleArr.Length; i += 1) {
                Assert.AreEqual(myModel.doubleArr[i], newModel.doubleArr[i]);
            }
            Assert.AreEqual(myModel.doubleList.Count, newModel.doubleList.Count);
            for (int i = 0; i < myModel.doubleList.Count; i += 1) {
                Assert.AreEqual(myModel.doubleList[i], newModel.doubleList[i]);
            }

            // - char
            Assert.AreEqual(myModel.charValue, newModel.charValue);

            // - string
            AssertString(myModel.strValue, newModel.strValue);
            Assert.AreEqual(myModel.strArr.Length, newModel.strArr.Length);
            for (int i = 0; i < myModel.strArr.Length; i += 1) {
                AssertString(myModel.strArr[i], newModel.strArr[i]);
                Assert.AreEqual(myModel.strArr[i], newModel.strArr[i]);
            }
            Assert.AreEqual(myModel.strList.Count, newModel.strList.Count);
            for (int i = 0; i < myModel.strList.Count; i += 1) {
                Assert.AreEqual(myModel.strList[i], newModel.strList[i]);
            }

            // - HerModel
            AssertString(myModel.herModel.name, newModel.herModel.name);
            Assert.AreEqual(myModel.herModel.value, newModel.herModel.value);
            Assert.AreEqual(myModel.herModelArr.Length, newModel.herModelArr.Length);
            for (int i = 0; i < myModel.herModelArr.Length; i += 1) {
                AssertString(myModel.herModelArr[i].name, newModel.herModelArr[i].name);
                Assert.AreEqual(myModel.herModelArr[i].value, newModel.herModelArr[i].value);
            }
            Assert.AreEqual(myModel.herModelList.Count, newModel.herModelList.Count);
            for (int i = 0; i < myModel.herModelList.Count; i += 1) {
                AssertString(myModel.herModelList[i].name, newModel.herModelList[i].name);
                Assert.AreEqual(myModel.herModelList[i].value, newModel.herModelList[i].value);
            }

            // - other
            Assert.AreEqual(myModel.otherStr, newModel.otherStr);

        }

        void AssertString(string left, string right) {
            bool leftEmpty = string.IsNullOrEmpty(left);
            bool rightEmpty = string.IsNullOrEmpty(right);
            UnityEngine.Debug.Log("AssertString: " + left + " " + right);
            if (!leftEmpty && !rightEmpty) {
                Assert.AreEqual(left, right);
            }
            Assert.AreEqual(leftEmpty, rightEmpty);
        }

    }
}