using System.Collections.Generic;
using UnityEngine;

namespace GameArki.BufferIO {

    public static class UnityBufferWriter {

        public static void WriteVector2(byte[] dst, Vector2 data, ref int offset) {
            BufferWriter.WriteSingle(dst, data.x, ref offset);
            BufferWriter.WriteSingle(dst, data.y, ref offset);
        }

        public static void WriteVector2Array(byte[] dst, Vector2[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
            }
        }

        public static void WriteVector2List(byte[] dst, List<Vector2> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
            }
        }

        public static void WriteVector2Int(byte[] dst, Vector2Int data, ref int offset) {
            BufferWriter.WriteInt32(dst, data.x, ref offset);
            BufferWriter.WriteInt32(dst, data.y, ref offset);
        }

        public static void WriteVector2IntArray(byte[] dst, Vector2Int[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteInt32(dst, data[i].x, ref offset);
                BufferWriter.WriteInt32(dst, data[i].y, ref offset);
            }
        }

        public static void WriteVector2IntList(byte[] dst, List<Vector2Int> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteInt32(dst, data[i].x, ref offset);
                BufferWriter.WriteInt32(dst, data[i].y, ref offset);
            }
        }

        public static void WriteVector3(byte[] dst, Vector3 data, ref int offset) {
            BufferWriter.WriteSingle(dst, data.x, ref offset);
            BufferWriter.WriteSingle(dst, data.y, ref offset);
            BufferWriter.WriteSingle(dst, data.z, ref offset);
        }

        public static void WriteVector3Array(byte[] dst, Vector3[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].z, ref offset);
            }
        }

        public static void WriteVector3List(byte[] dst, List<Vector3> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].z, ref offset);
            }
        }

        public static void WriteVector3Int(byte[] dst, Vector3Int data, ref int offset) {
            BufferWriter.WriteInt32(dst, data.x, ref offset);
            BufferWriter.WriteInt32(dst, data.y, ref offset);
            BufferWriter.WriteInt32(dst, data.z, ref offset);
        }

        public static void WriteVector3IntArray(byte[] dst, Vector3Int[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteInt32(dst, data[i].x, ref offset);
                BufferWriter.WriteInt32(dst, data[i].y, ref offset);
                BufferWriter.WriteInt32(dst, data[i].z, ref offset);
            }
        }

        public static void WriteVector3IntList(byte[] dst, List<Vector3Int> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteInt32(dst, data[i].x, ref offset);
                BufferWriter.WriteInt32(dst, data[i].y, ref offset);
                BufferWriter.WriteInt32(dst, data[i].z, ref offset);
            }
        }

        public static void WriteVector4(byte[] dst, Vector4 data, ref int offset) {
            BufferWriter.WriteSingle(dst, data.x, ref offset);
            BufferWriter.WriteSingle(dst, data.y, ref offset);
            BufferWriter.WriteSingle(dst, data.z, ref offset);
            BufferWriter.WriteSingle(dst, data.w, ref offset);
        }

        public static void WriteVector4Array(byte[] dst, Vector4[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].z, ref offset);
                BufferWriter.WriteSingle(dst, data[i].w, ref offset);
            }
        }

        public static void WriteVector4List(byte[] dst, List<Vector4> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].z, ref offset);
                BufferWriter.WriteSingle(dst, data[i].w, ref offset);
            }
        }

        public static void WriteColor(byte[] dst, Color data, ref int offset) {
            BufferWriter.WriteSingle(dst, data.r, ref offset);
            BufferWriter.WriteSingle(dst, data.g, ref offset);
            BufferWriter.WriteSingle(dst, data.b, ref offset);
            BufferWriter.WriteSingle(dst, data.a, ref offset);
        }

        public static void WriteColorArray(byte[] dst, Color[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteSingle(dst, data[i].r, ref offset);
                BufferWriter.WriteSingle(dst, data[i].g, ref offset);
                BufferWriter.WriteSingle(dst, data[i].b, ref offset);
                BufferWriter.WriteSingle(dst, data[i].a, ref offset);
            }
        }

        public static void WriteColorList(byte[] dst, List<Color> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteSingle(dst, data[i].r, ref offset);
                BufferWriter.WriteSingle(dst, data[i].g, ref offset);
                BufferWriter.WriteSingle(dst, data[i].b, ref offset);
                BufferWriter.WriteSingle(dst, data[i].a, ref offset);
            }
        }

        public static void WriteColor32(byte[] dst, Color32 data, ref int offset) {
            BufferWriter.WriteUInt8(dst, data.r, ref offset);
            BufferWriter.WriteUInt8(dst, data.g, ref offset);
            BufferWriter.WriteUInt8(dst, data.b, ref offset);
            BufferWriter.WriteUInt8(dst, data.a, ref offset);
        }

        public static void WriteColor32Array(byte[] dst, Color32[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteUInt8(dst, data[i].r, ref offset);
                BufferWriter.WriteUInt8(dst, data[i].g, ref offset);
                BufferWriter.WriteUInt8(dst, data[i].b, ref offset);
                BufferWriter.WriteUInt8(dst, data[i].a, ref offset);
            }
        }

        public static void WriteColor32List(byte[] dst, List<Color32> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteUInt8(dst, data[i].r, ref offset);
                BufferWriter.WriteUInt8(dst, data[i].g, ref offset);
                BufferWriter.WriteUInt8(dst, data[i].b, ref offset);
                BufferWriter.WriteUInt8(dst, data[i].a, ref offset);
            }
        }

        public static void WriteQuaternion(byte[] dst, Quaternion data, ref int offset) {
            BufferWriter.WriteSingle(dst, data.x, ref offset);
            BufferWriter.WriteSingle(dst, data.y, ref offset);
            BufferWriter.WriteSingle(dst, data.z, ref offset);
            BufferWriter.WriteSingle(dst, data.w, ref offset);
        }

        public static void WriteQuaternionArray(byte[] dst, Quaternion[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].z, ref offset);
                BufferWriter.WriteSingle(dst, data[i].w, ref offset);
            }
        }

        public static void WriteQuaternionList(byte[] dst, List<Quaternion> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].z, ref offset);
                BufferWriter.WriteSingle(dst, data[i].w, ref offset);
            }
        }

        public static void WriteRect(byte[] dst, Rect data, ref int offset) {
            BufferWriter.WriteSingle(dst, data.x, ref offset);
            BufferWriter.WriteSingle(dst, data.y, ref offset);
            BufferWriter.WriteSingle(dst, data.width, ref offset);
            BufferWriter.WriteSingle(dst, data.height, ref offset);
        }

        public static void WriteRectArray(byte[] dst, Rect[] data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Length, ref offset);
            for (int i = 0; i < data.Length; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].width, ref offset);
                BufferWriter.WriteSingle(dst, data[i].height, ref offset);
            }
        }

        public static void WriteRectList(byte[] dst, List<Rect> data, ref int offset) {
            BufferWriter.WriteUInt16(dst, (ushort)data.Count, ref offset);
            for (int i = 0; i < data.Count; i++) {
                BufferWriter.WriteSingle(dst, data[i].x, ref offset);
                BufferWriter.WriteSingle(dst, data[i].y, ref offset);
                BufferWriter.WriteSingle(dst, data[i].width, ref offset);
                BufferWriter.WriteSingle(dst, data[i].height, ref offset);
            }
        }

    }

}