using System.Collections.Generic;
using UnityEngine;
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

            myModel.vector2 = new Vector2(1.1f, 2.2f);
            myModel.vector2Arr = new Vector2[2] { new Vector2(1.1f, 2.2f), new Vector2(3.3f, 4.4f) };
            myModel.vector2List = new List<Vector2>() { new Vector2(1.1f, 2.2f), new Vector2(3.3f, 4.4f) };

            myModel.vector2Int = new Vector2Int(1, 2);
            myModel.vector2IntArr = new Vector2Int[2] { new Vector2Int(1, 2), new Vector2Int(3, 4) };
            myModel.vector2IntList = new List<Vector2Int>() { new Vector2Int(1, 2), new Vector2Int(3, 4) };

            myModel.vector3 = new Vector3(1.1f, 2.2f, 3.3f);
            myModel.vector3Arr = new Vector3[2] { new Vector3(1.1f, 2.2f, 3.3f), new Vector3(4.4f, 5.5f, 6.6f) };
            myModel.vector3List = new List<Vector3>() { new Vector3(1.1f, 2.2f, 3.3f), new Vector3(4.4f, 5.5f, 6.6f) };

            myModel.vector3Int = new Vector3Int(1, 2, 3);
            myModel.vector3IntArr = new Vector3Int[2] { new Vector3Int(1, 2, 3), new Vector3Int(4, 5, 6) };
            myModel.vector3IntList = new List<Vector3Int>() { new Vector3Int(1, 2, 3), new Vector3Int(4, 5, 6) };

            myModel.vector4 = new Vector4(1.1f, 2.2f, 3.3f, 4.4f);
            myModel.vector4Arr = new Vector4[2] { new Vector4(1.1f, 2.2f, 3.3f, 4.4f), new Vector4(5.5f, 6.6f, 7.7f, 8.8f) };
            myModel.vector4List = new List<Vector4>() { new Vector4(1.1f, 2.2f, 3.3f, 4.4f), new Vector4(5.5f, 6.6f, 7.7f, 8.8f) };

            myModel.quaternion = new Quaternion(1.1f, 2.2f, 3.3f, 4.4f);
            myModel.quaternionArr = new Quaternion[2] { new Quaternion(1.1f, 2.2f, 3.3f, 4.4f), new Quaternion(5.5f, 6.6f, 7.7f, 8.8f) };
            myModel.quaternionList = new List<Quaternion>() { new Quaternion(1.1f, 2.2f, 3.3f, 4.4f), new Quaternion(5.5f, 6.6f, 7.7f, 8.8f) };

            myModel.color = new Color(0.5f, 0.2f, 0.3f, 0.4f);  
            myModel.colorArr = new Color[2] { new Color(0.5f, 0.2f, 0.3f, 0.4f), new Color(0.5f, 0.2f, 0.3f, 0.4f) };
            myModel.colorList = new List<Color>() { new Color(0.5f, 0.2f, 0.3f, 0.4f), new Color(0.5f, 0.2f, 0.3f, 0.4f) };

            myModel.color32 = new Color32(1, 2, 3, 4);
            myModel.color32Arr = new Color32[2] { new Color32(1, 2, 3, 4), new Color32(5, 6, 7, 8) };
            myModel.color32List = new List<Color32>() { new Color32(1, 2, 3, 4), new Color32(5, 6, 7, 8) };

            myModel.rect = new Rect(1.1f, 2.2f, 3.3f, 4.4f);
            myModel.rectArr = new Rect[2] { new Rect(1.1f, 2.2f, 3.3f, 4.4f), new Rect(5.5f, 6.6f, 7.7f, 8.8f) };
            myModel.rectList = new List<Rect>() { new Rect(1.1f, 2.2f, 3.3f, 4.4f), new Rect(5.5f, 6.6f, 7.7f, 8.8f) };

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

            // - vector2
            Assert.AreEqual(myModel.vector2, newModel.vector2);
            Assert.AreEqual(myModel.vector2Arr.Length, newModel.vector2Arr.Length);
            for (int i = 0; i < myModel.vector2Arr.Length; i += 1) {
                Assert.AreEqual(myModel.vector2Arr[i], newModel.vector2Arr[i]);
            }
            Assert.AreEqual(myModel.vector2List.Count, newModel.vector2List.Count);
            for (int i = 0; i < myModel.vector2List.Count; i += 1) {
                Assert.AreEqual(myModel.vector2List[i], newModel.vector2List[i]);
            }

            // - vector2Int
            Assert.AreEqual(myModel.vector2Int, newModel.vector2Int);
            Assert.AreEqual(myModel.vector2IntArr.Length, newModel.vector2IntArr.Length);
            for (int i = 0; i < myModel.vector2IntArr.Length; i += 1) {
                Assert.AreEqual(myModel.vector2IntArr[i], newModel.vector2IntArr[i]);
            }
            Assert.AreEqual(myModel.vector2IntList.Count, newModel.vector2IntList.Count);
            for (int i = 0; i < myModel.vector2IntList.Count; i += 1) {
                Assert.AreEqual(myModel.vector2IntList[i], newModel.vector2IntList[i]);
            }

            // - vector3
            Assert.AreEqual(myModel.vector3, newModel.vector3);
            Assert.AreEqual(myModel.vector3Arr.Length, newModel.vector3Arr.Length);
            for (int i = 0; i < myModel.vector3Arr.Length; i += 1) {
                Assert.AreEqual(myModel.vector3Arr[i], newModel.vector3Arr[i]);
            }
            Assert.AreEqual(myModel.vector3List.Count, newModel.vector3List.Count);
            for (int i = 0; i < myModel.vector3List.Count; i += 1) {
                Assert.AreEqual(myModel.vector3List[i], newModel.vector3List[i]);
            }

            // - vector3Int
            Assert.AreEqual(myModel.vector3Int, newModel.vector3Int);
            Assert.AreEqual(myModel.vector3IntArr.Length, newModel.vector3IntArr.Length);
            for (int i = 0; i < myModel.vector3IntArr.Length; i += 1) {
                Assert.AreEqual(myModel.vector3IntArr[i], newModel.vector3IntArr[i]);
            }
            Assert.AreEqual(myModel.vector3IntList.Count, newModel.vector3IntList.Count);
            for (int i = 0; i < myModel.vector3IntList.Count; i += 1) {
                Assert.AreEqual(myModel.vector3IntList[i], newModel.vector3IntList[i]);
            }

            // - vector4
            Assert.AreEqual(myModel.vector4, newModel.vector4);
            Assert.AreEqual(myModel.vector4Arr.Length, newModel.vector4Arr.Length);
            for (int i = 0; i < myModel.vector4Arr.Length; i += 1) {
                Assert.AreEqual(myModel.vector4Arr[i], newModel.vector4Arr[i]);
            }
            Assert.AreEqual(myModel.vector4List.Count, newModel.vector4List.Count);
            for (int i = 0; i < myModel.vector4List.Count; i += 1) {
                Assert.AreEqual(myModel.vector4List[i], newModel.vector4List[i]);
            }

            // - quaternion
            Assert.AreEqual(myModel.quaternion, newModel.quaternion);
            Assert.AreEqual(myModel.quaternionArr.Length, newModel.quaternionArr.Length);
            for (int i = 0; i < myModel.quaternionArr.Length; i += 1) {
                Assert.AreEqual(myModel.quaternionArr[i], newModel.quaternionArr[i]);
            }
            Assert.AreEqual(myModel.quaternionList.Count, newModel.quaternionList.Count);
            for (int i = 0; i < myModel.quaternionList.Count; i += 1) {
                Assert.AreEqual(myModel.quaternionList[i], newModel.quaternionList[i]);
            }

            // - color
            Assert.AreEqual(myModel.color, newModel.color);
            Assert.AreEqual(myModel.colorArr.Length, newModel.colorArr.Length);
            for (int i = 0; i < myModel.colorArr.Length; i += 1) {
                Assert.AreEqual(myModel.colorArr[i], newModel.colorArr[i]);
            }
            Assert.AreEqual(myModel.colorList.Count, newModel.colorList.Count);
            for (int i = 0; i < myModel.colorList.Count; i += 1) {
                Assert.AreEqual(myModel.colorList[i], newModel.colorList[i]);
            }

            // - color32
            Assert.AreEqual(myModel.color32, newModel.color32);
            Assert.AreEqual(myModel.color32Arr.Length, newModel.color32Arr.Length);
            for (int i = 0; i < myModel.color32Arr.Length; i += 1) {
                Assert.AreEqual(myModel.color32Arr[i], newModel.color32Arr[i]);
            }
            Assert.AreEqual(myModel.color32List.Count, newModel.color32List.Count);
            for (int i = 0; i < myModel.color32List.Count; i += 1) {
                Assert.AreEqual(myModel.color32List[i], newModel.color32List[i]);
            }

            // - rect
            Assert.AreEqual(myModel.rect, newModel.rect);
            Assert.AreEqual(myModel.rectArr.Length, newModel.rectArr.Length);
            for (int i = 0; i < myModel.rectArr.Length; i += 1) {
                Assert.AreEqual(myModel.rectArr[i], newModel.rectArr[i]);
            }
            Assert.AreEqual(myModel.rectList.Count, newModel.rectList.Count);
            for (int i = 0; i < myModel.rectList.Count; i += 1) {
                Assert.AreEqual(myModel.rectList[i], newModel.rectList[i]);
            }

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