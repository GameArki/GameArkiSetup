using System.Collections.Generic;
using UnityEngine;

namespace GameArki.BufferIO {

    public static class UnityBufferReader {

        public static Vector2 ReadVector2(byte[] src, ref int offset) {
            return new Vector2(BufferReader.ReadSingle(src, ref offset),
                               BufferReader.ReadSingle(src, ref offset));
        }

        public static Vector2[] ReadVector2Array(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Vector2[] data = new Vector2[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Vector2(BufferReader.ReadSingle(src, ref offset),
                                      BufferReader.ReadSingle(src, ref offset));
            }
            return data;
        }

        public static List<Vector2> ReadVector2List(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Vector2> data = new List<Vector2>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Vector2(BufferReader.ReadSingle(src, ref offset),
                                     BufferReader.ReadSingle(src, ref offset)));
            }
            return data;
        }

        public static Vector2Int ReadVector2Int(byte[] src, ref int offset) {
            return new Vector2Int(BufferReader.ReadInt32(src, ref offset),
                                  BufferReader.ReadInt32(src, ref offset));
        }

        public static Vector2Int[] ReadVector2IntArray(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Vector2Int[] data = new Vector2Int[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Vector2Int(BufferReader.ReadInt32(src, ref offset),
                                         BufferReader.ReadInt32(src, ref offset));
            }
            return data;
        }

        public static List<Vector2Int> ReadVector2IntList(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Vector2Int> data = new List<Vector2Int>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Vector2Int(BufferReader.ReadInt32(src, ref offset),
                                        BufferReader.ReadInt32(src, ref offset)));
            }
            return data;
        }

        public static Vector3 ReadVector3(byte[] src, ref int offset) {
            return new Vector3(BufferReader.ReadSingle(src, ref offset),
                               BufferReader.ReadSingle(src, ref offset),
                               BufferReader.ReadSingle(src, ref offset));
        }

        public static Vector3[] ReadVector3Array(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Vector3[] data = new Vector3[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Vector3(BufferReader.ReadSingle(src, ref offset),
                                      BufferReader.ReadSingle(src, ref offset),
                                      BufferReader.ReadSingle(src, ref offset));
            }
            return data;
        }

        public static List<Vector3> ReadVector3List(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Vector3> data = new List<Vector3>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Vector3(BufferReader.ReadSingle(src, ref offset),
                                     BufferReader.ReadSingle(src, ref offset),
                                     BufferReader.ReadSingle(src, ref offset)));
            }
            return data;
        }

        public static Vector3Int ReadVector3Int(byte[] src, ref int offset) {
            return new Vector3Int(BufferReader.ReadInt32(src, ref offset),
                                  BufferReader.ReadInt32(src, ref offset),
                                  BufferReader.ReadInt32(src, ref offset));
        }

        public static Vector3Int[] ReadVector3IntArray(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Vector3Int[] data = new Vector3Int[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Vector3Int(BufferReader.ReadInt32(src, ref offset),
                                         BufferReader.ReadInt32(src, ref offset),
                                         BufferReader.ReadInt32(src, ref offset));
            }
            return data;
        }

        public static List<Vector3Int> ReadVector3IntList(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Vector3Int> data = new List<Vector3Int>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Vector3Int(BufferReader.ReadInt32(src, ref offset),
                                        BufferReader.ReadInt32(src, ref offset),
                                        BufferReader.ReadInt32(src, ref offset)));
            }
            return data;
        }

        public static Vector4 ReadVector4(byte[] src, ref int offset) {
            return new Vector4(BufferReader.ReadSingle(src, ref offset),
                               BufferReader.ReadSingle(src, ref offset),
                               BufferReader.ReadSingle(src, ref offset),
                               BufferReader.ReadSingle(src, ref offset));
        }

        public static Vector4[] ReadVector4Array(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Vector4[] data = new Vector4[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Vector4(BufferReader.ReadSingle(src, ref offset),
                                      BufferReader.ReadSingle(src, ref offset),
                                      BufferReader.ReadSingle(src, ref offset),
                                      BufferReader.ReadSingle(src, ref offset));
            }
            return data;
        }

        public static List<Vector4> ReadVector4List(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Vector4> data = new List<Vector4>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Vector4(BufferReader.ReadSingle(src, ref offset),
                                     BufferReader.ReadSingle(src, ref offset),
                                     BufferReader.ReadSingle(src, ref offset),
                                     BufferReader.ReadSingle(src, ref offset)));
            }
            return data;
        }

        public static Color ReadColor(byte[] src, ref int offset) {
            return new Color(BufferReader.ReadSingle(src, ref offset),
                             BufferReader.ReadSingle(src, ref offset),
                             BufferReader.ReadSingle(src, ref offset),
                             BufferReader.ReadSingle(src, ref offset));
        }

        public static Color[] ReadColorArray(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Color[] data = new Color[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Color(BufferReader.ReadSingle(src, ref offset),
                                    BufferReader.ReadSingle(src, ref offset),
                                    BufferReader.ReadSingle(src, ref offset),
                                    BufferReader.ReadSingle(src, ref offset));
            }
            return data;
        }

        public static List<Color> ReadColorList(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Color> data = new List<Color>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Color(BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset)));
            }
            return data;
        }

        public static Color32 ReadColor32(byte[] src, ref int offset) {
            return new Color32(BufferReader.ReadUInt8(src, ref offset),
                               BufferReader.ReadUInt8(src, ref offset),
                               BufferReader.ReadUInt8(src, ref offset),
                               BufferReader.ReadUInt8(src, ref offset));
        }

        public static Color32[] ReadColor32Array(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Color32[] data = new Color32[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Color32(BufferReader.ReadUInt8(src, ref offset),
                                      BufferReader.ReadUInt8(src, ref offset),
                                      BufferReader.ReadUInt8(src, ref offset),
                                      BufferReader.ReadUInt8(src, ref offset));
            }
            return data;
        }

        public static List<Color32> ReadColor32List(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Color32> data = new List<Color32>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Color32(BufferReader.ReadUInt8(src, ref offset),
                                     BufferReader.ReadUInt8(src, ref offset),
                                     BufferReader.ReadUInt8(src, ref offset),
                                     BufferReader.ReadUInt8(src, ref offset)));
            }
            return data;
        }

        public static Quaternion ReadQuaternion(byte[] src, ref int offset) {
            return new Quaternion(BufferReader.ReadSingle(src, ref offset),
                                  BufferReader.ReadSingle(src, ref offset),
                                  BufferReader.ReadSingle(src, ref offset),
                                  BufferReader.ReadSingle(src, ref offset));
        }

        public static Quaternion[] ReadQuaternionArray(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Quaternion[] data = new Quaternion[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Quaternion(BufferReader.ReadSingle(src, ref offset),
                                         BufferReader.ReadSingle(src, ref offset),
                                         BufferReader.ReadSingle(src, ref offset),
                                         BufferReader.ReadSingle(src, ref offset));
            }
            return data;
        }

        public static List<Quaternion> ReadQuaternionList(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Quaternion> data = new List<Quaternion>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Quaternion(BufferReader.ReadSingle(src, ref offset),
                                        BufferReader.ReadSingle(src, ref offset),
                                        BufferReader.ReadSingle(src, ref offset),
                                        BufferReader.ReadSingle(src, ref offset)));
            }
            return data;
        }

        public static Rect ReadRect(byte[] src, ref int offset) {
            return new Rect(BufferReader.ReadSingle(src, ref offset),
                            BufferReader.ReadSingle(src, ref offset),
                            BufferReader.ReadSingle(src, ref offset),
                            BufferReader.ReadSingle(src, ref offset));
        }

        public static Rect[] ReadRectArray(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            Rect[] data = new Rect[length];
            for (int i = 0; i < length; i++) {
                data[i] = new Rect(BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset));
            }
            return data;
        }

        public static List<Rect> ReadRectList(byte[] src, ref int offset) {
            int length = BufferReader.ReadUInt16(src, ref offset);
            List<Rect> data = new List<Rect>(length);
            for (int i = 0; i < length; i++) {
                data.Add(new Rect(BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset),
                                   BufferReader.ReadSingle(src, ref offset)));
            }
            return data;
        }
    }
}