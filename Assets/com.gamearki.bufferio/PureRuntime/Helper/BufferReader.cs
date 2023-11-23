using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace GameArki.BufferIO {

    public static class BufferReader {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReadBool(byte[] src, ref int offset) {
            return ReadUInt8(src, ref offset) == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte ReadInt8(byte[] src, ref int offset) {
            return (sbyte)ReadUInt8(src, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReadUInt8(byte[] src, ref int offset) {
            return src[offset++];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char ReadChar(byte[] src, ref int offset) {
            char data = (char)ReadUInt16(src, ref offset);
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadInt16(byte[] src, ref int offset) {
            return (short)ReadUInt16(src, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUInt16(byte[] src, ref int offset) {
            Bit16 content = new Bit16();
            content.b0 = src[offset++];
            content.b1 = src[offset++];
            return content.ushortValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadInt32(byte[] src, ref int offset) {
            return (int)ReadUInt32(src, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUInt32(byte[] src, ref int offset) {
            Bit32 content = new Bit32();
            content.b0 = src[offset++];
            content.b1 = src[offset++];
            content.b2 = src[offset++];
            content.b3 = src[offset++];
            return content.uintValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadInt64(byte[] src, ref int offset) {
            return (long)ReadUInt64(src, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadUInt64(byte[] src, ref int offset) {
            Bit64 content = new Bit64();
            content.b0 = src[offset++];
            content.b1 = src[offset++];
            content.b2 = src[offset++];
            content.b3 = src[offset++];
            content.b4 = src[offset++];
            content.b5 = src[offset++];
            content.b6 = src[offset++];
            content.b7 = src[offset++];
            return content.ulongValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReadSingle(byte[] src, ref int offset) {
            Bit32 content = new Bit32();
            content.b0 = src[offset++];
            content.b1 = src[offset++];
            content.b2 = src[offset++];
            content.b3 = src[offset++];
            return content.floatValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReadDouble(byte[] src, ref int offset) {
            Bit64 content = new Bit64();
            content.b0 = src[offset++];
            content.b1 = src[offset++];
            content.b2 = src[offset++];
            content.b3 = src[offset++];
            content.b4 = src[offset++];
            content.b5 = src[offset++];
            content.b6 = src[offset++];
            content.b7 = src[offset++];
            return content.doubleValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool[] ReadBoolArr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            bool[] data = new bool[count];
            Buffer.BlockCopy(src, offset, data, 0, count);
            offset += count;
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<bool> ReadBoolList(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<bool> data = new List<bool>(count);
            for (int i = 0; i < count; i += 1) {
                bool d = ReadBool(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ReadUInt8Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            byte[] data = new byte[count];
            Buffer.BlockCopy(src, offset, data, 0, count);
            offset += count;
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<byte> ReadUInt8List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<byte> data = new List<byte>(count);
            for (int i = 0; i < count; i += 1) {
                byte d = ReadUInt8(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte[] ReadInt8Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            sbyte[] data = new sbyte[count];
            Buffer.BlockCopy(src, offset, data, 0, count);
            offset += count;
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<sbyte> ReadInt8List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<sbyte> data = new List<sbyte>(count);
            for (int i = 0; i < count; i += 1) {
                sbyte d = ReadInt8(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short[] ReadInt16Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            short[] data = new short[count];
            for (int i = 0; i < count; i += 1) {
                short d = ReadInt16(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<short> ReadInt16List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<short> data = new List<short>(count);
            for (int i = 0; i < count; i += 1) {
                short d = ReadInt16(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort[] ReadUInt16Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            ushort[] data = new ushort[count];
            for (int i = 0; i < count; i += 1) {
                ushort d = ReadUInt16(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<ushort> ReadUInt16List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<ushort> data = new List<ushort>(count);
            for (int i = 0; i < count; i += 1) {
                ushort d = ReadUInt16(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] ReadInt32Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            int[] data = new int[count];
            for (int i = 0; i < count; i += 1) {
                int d = ReadInt32(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<int> ReadInt32List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<int> data = new List<int>(count);
            for (int i = 0; i < count; i += 1) {
                int d = ReadInt32(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint[] ReadUInt32Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            uint[] data = new uint[count];
            for (int i = 0; i < count; i += 1) {
                uint d = ReadUInt32(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<uint> ReadUInt32List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<uint> data = new List<uint>(count);
            for (int i = 0; i < count; i += 1) {
                uint d = ReadUInt32(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long[] ReadInt64Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            long[] data = new long[count];
            for (int i = 0; i < count; i += 1) {
                long d = ReadInt64(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<long> ReadInt64List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<long> data = new List<long>(count);
            for (int i = 0; i < count; i += 1) {
                long d = ReadInt64(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong[] ReadUInt64Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            ulong[] data = new ulong[count];
            for (int i = 0; i < count; i += 1) {
                ulong d = ReadUInt64(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<ulong> ReadUInt64List(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<ulong> data = new List<ulong>(count);
            for (int i = 0; i < count; i += 1) {
                ulong d = ReadUInt64(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ReadSingleArr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            float[] data = new float[count];
            for (int i = 0; i < count; i += 1) {
                float d = ReadSingle(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<float> ReadSingleList(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<float> data = new List<float>(count);
            for (int i = 0; i < count; i += 1) {
                float d = ReadSingle(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] ReadDoubleArr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            double[] data = new double[count];
            for (int i = 0; i < count; i += 1) {
                double d = ReadDouble(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<double> ReadDoubleList(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            List<double> data = new List<double>(count);
            for (int i = 0; i < count; i += 1) {
                double d = ReadDouble(src, ref offset);
                data.Add(d);
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ReadUTF8String(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            string data = Encoding.UTF8.GetString(src, offset, count);
            offset += count;
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string[] ReadUTF8StringArr(byte[] src, ref int offset) {
            ushort totalCount = ReadUInt16(src, ref offset);
            string[] data = new string[totalCount];
            for (int i = 0; i < totalCount; i += 1) {
                ushort count = ReadUInt16(src, ref offset);
                string s = Encoding.UTF8.GetString(src, offset, count);
                data[i] = s;
                offset += count;
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<string> ReadUTF8StringList(byte[] src, ref int offset) {
            ushort totalCount = ReadUInt16(src, ref offset);
            List<string> data = new List<string>(totalCount);
            for (int i = 0; i < totalCount; i += 1) {
                ushort count = ReadUInt16(src, ref offset);
                string s = Encoding.UTF8.GetString(src, offset, count);
                data.Add(s);
                offset += count;
            }
            return data;
        }

        // ==== VARINT ====
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadVarint(byte[] src, ref int offset) {
            ulong data = 0;
            byte b = 0;
            for (int i = 0, j = 0; ; i += 1, j += 7) {
                b = src[offset++];
                if ((b & 0x80) != 0) {
                    data |= (ulong)(b & 0x7F) << j;
                } else {
                    data |= (ulong)b << j;
                    break;
                }
                if (i >= 9 && b > 0) {
                    throw new Exception("overflow");
                }
            }
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadVarintWithZigZag(byte[] src, ref int offset) {
            ulong udata = (ulong)ReadVarint(src, ref offset);
            ulong data = ReadZigZag(udata);
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ulong ReadZigZag(ulong value) {
            bool isNegative = value % 2 != 0;
            unchecked {
                if (isNegative) {
                    return (~(value >> 1) + 1ul) | 0x8000_0000_0000_0000ul;
                } else {
                    return value >> 1;
                }
            }
        }

    }

}