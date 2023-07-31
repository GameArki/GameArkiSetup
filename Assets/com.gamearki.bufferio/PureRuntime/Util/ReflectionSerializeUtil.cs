using System;
using System.Linq;
using System.Reflection;

namespace GameArki.BufferIO {

    public static class ReflectionSerializeUtil {

        static byte[] writeBuffer = new byte[4096];

        public static byte[] Serialize<T>(T obj) {

            int offset = 0;

            var fields = GetFields<T>();
            for (int i = 0; i < fields.Length; i += 1) {
                var field = fields[i];
                var value = field.GetValue(obj);
                WriteField(writeBuffer, field.FieldType.Name, value, ref offset);
            }

            byte[] result = new byte[offset];
            Buffer.BlockCopy(writeBuffer, 0, result, 0, offset);
            return result;

        }

        public static T Deserialize<T>(byte[] data) {

            int offset = 0;

            var type = typeof(T);
            var fields = GetFields<T>();
            var obj = Activator.CreateInstance<T>();
            for (int i = 0; i < fields.Length; i += 1) {
                var field = fields[i];
                var value = ReadField(data, field.FieldType.Name, ref offset);
                field.SetValue(obj, value);
            }

            return obj;

        }

        static FieldInfo[] GetFields<T>() {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).OrderBy(value => value.Name).ToArray();
            return fields;
        }

        static void WriteField(byte[] dst, string fielType, object value, ref int offset) {

            // - Sign
            byte typeSign = GetTypeSign(fielType);
            BufferWriter.WriteUInt8(dst, typeSign, ref offset);

            // - Data
            switch (fielType) {
                case "Boolean":
                    BufferWriter.WriteBool(dst, (bool)value, ref offset);
                    break;
                case "Boolean[]":
                    BufferWriter.WriteBoolArr(dst, (bool[])value, ref offset);
                    break;
                case "Byte":
                    BufferWriter.WriteUInt8(dst, (byte)value, ref offset);
                    break;
                case "Byte[]":
                    BufferWriter.WriteUint8Arr(dst, (byte[])value, ref offset);
                    break;
                case "SByte":
                    BufferWriter.WriteInt8(dst, (sbyte)value, ref offset);
                    break;
                case "SByte[]":
                    BufferWriter.WriteInt8Arr(dst, (sbyte[])value, ref offset);
                    break;
                case "Int16":
                    BufferWriter.WriteInt16(dst, (short)value, ref offset);
                    break;
                case "Int16[]":
                    BufferWriter.WriteInt16Arr(dst, (short[])value, ref offset);
                    break;
                case "UInt16":
                    BufferWriter.WriteUInt16(dst, (ushort)value, ref offset);
                    break;
                case "UInt16[]":
                    BufferWriter.WriteUInt16Arr(dst, (ushort[])value, ref offset);
                    break;
                case "Int32":
                    BufferWriter.WriteInt32(dst, (int)value, ref offset);
                    break;
                case "Int32[]":
                    BufferWriter.WriteInt32Arr(dst, (int[])value, ref offset);
                    break;
                case "UInt32":
                    BufferWriter.WriteUInt32(dst, (uint)value, ref offset);
                    break;
                case "UInt32[]":
                    BufferWriter.WriteUInt32Arr(dst, (uint[])value, ref offset);
                    break;
                case "Int64":
                    BufferWriter.WriteInt64(dst, (long)value, ref offset);
                    break;
                case "Int64[]":
                    BufferWriter.WriteInt64Arr(dst, (long[])value, ref offset);
                    break;
                case "UInt64":
                    BufferWriter.WriteUInt64(dst, (ulong)value, ref offset);
                    break;
                case "UInt64[]":
                    BufferWriter.WriteUInt64Arr(dst, (ulong[])value, ref offset);
                    break;
                case "Single":
                    BufferWriter.WriteSingle(dst, (float)value, ref offset);
                    break;
                case "Single[]":
                    BufferWriter.WriteSingleArr(dst, (float[])value, ref offset);
                    break;
                case "Double":
                    BufferWriter.WriteDouble(dst, (double)value, ref offset);
                    break;
                case "Double[]":
                    BufferWriter.WriteDoubleArr(dst, (double[])value, ref offset);
                    break;
                case "Char":
                    BufferWriter.WriteChar(dst, (char)value, ref offset);
                    break;
                case "String":
                    BufferWriter.WriteUTF8String(dst, (string)value, ref offset);
                    break;
                case "String[]":
                    BufferWriter.WriteUTF8StringArr(dst, (string[])value, ref offset);
                    break;
                default:
                    System.Console.WriteLine("Unsupport type: " + fielType);
                    break;
            }

        }

        static object ReadField(byte[] data, string fielType, ref int offset) {

            // - Sign
            byte typeSign = BufferReader.ReadUInt8(data, ref offset);

            // - Data
            switch (fielType) {
                case "Boolean":
                    return BufferReader.ReadBool(data, ref offset);
                case "Boolean[]":
                    return BufferReader.ReadBoolArr(data, ref offset);
                case "Byte":
                    return BufferReader.ReadUInt8(data, ref offset);
                case "Byte[]":
                    return BufferReader.ReadUInt8Arr(data, ref offset);
                case "SByte":
                    return BufferReader.ReadInt8(data, ref offset);
                case "SByte[]":
                    return BufferReader.ReadInt8Arr(data, ref offset);
                case "Int16":
                    return BufferReader.ReadInt16(data, ref offset);
                case "Int16[]":
                    return BufferReader.ReadInt16Arr(data, ref offset);
                case "UInt16":
                    return BufferReader.ReadUInt16(data, ref offset);
                case "UInt16[]":
                    return BufferReader.ReadUInt16Arr(data, ref offset);
                case "Int32":
                    return BufferReader.ReadInt32(data, ref offset);
                case "Int32[]":
                    return BufferReader.ReadInt32Arr(data, ref offset);
                case "UInt32":
                    return BufferReader.ReadUInt32(data, ref offset);
                case "UInt32[]":
                    return BufferReader.ReadUInt32Arr(data, ref offset);
                case "Int64":
                    return BufferReader.ReadInt64(data, ref offset);
                case "Int64[]":
                    return BufferReader.ReadInt64Arr(data, ref offset);
                case "UInt64":
                    return BufferReader.ReadUInt64(data, ref offset);
                case "UInt64[]":
                    return BufferReader.ReadUInt64Arr(data, ref offset);
                case "Single":
                    return BufferReader.ReadSingle(data, ref offset);
                case "Single[]":
                    return BufferReader.ReadSingleArr(data, ref offset);
                case "Double":
                    return BufferReader.ReadDouble(data, ref offset);
                case "Double[]":
                    return BufferReader.ReadDoubleArr(data, ref offset);
                case "Char":
                    return BufferReader.ReadChar(data, ref offset);
                case "String":
                    return BufferReader.ReadUTF8String(data, ref offset);
                case "String[]":
                    return BufferReader.ReadUTF8StringArr(data, ref offset);
                default:
                    System.Console.WriteLine("Unsupport type: " + fielType);
                    return null;
            }

        }

        static byte GetTypeSign(string type) {
            switch (type) {
                case "Boolean":
                    return 1;
                case "Boolean[]":
                    return 2;
                case "Byte":
                    return 3;
                case "Byte[]":
                    return 4;
                case "SByte":
                    return 5;
                case "SByte[]":
                    return 6;
                case "Int16":
                    return 7;
                case "Int16[]":
                    return 8;
                case "UInt16":
                    return 9;
                case "UInt16[]":
                    return 10;
                case "Int32":
                    return 11;
                case "Int32[]":
                    return 12;
                case "UInt32":
                    return 13;
                case "UInt32[]":
                    return 14;
                case "Int64":
                    return 15;
                case "Int64[]":
                    return 16;
                case "UInt64":
                    return 17;
                case "UInt64[]":
                    return 18;
                case "Single":
                    return 19;
                case "Single[]":
                    return 20;
                case "Double":
                    return 21;
                case "Double[]":
                    return 22;
                case "Char":
                    return 23;
                case "Char[]":
                    return 24;
                case "String":
                    return 25;
                case "String[]":
                    return 26;
                default:
                    return 0;
            }
        }

    }

}