using System;
using System.Text;

namespace GameArki.BufferIO {

    public static class BufferReader {

        public static bool ReadBool(byte[] src, ref int offset) {
            return ReadUInt8(src, ref offset) == 1;
        }

        public static sbyte ReadInt8(byte[] src, ref int offset) {
            return (sbyte)ReadUInt8(src, ref offset);
        }

        public static byte ReadUInt8(byte[] src, ref int offset) {
            byte data = src[offset];
            offset += 1;
            return data;
        }

        public static char ReadChar(byte[] src, ref int offset) {
            ushort d = ReadUInt16(src, ref offset);
            char data = (char)d;
            return data;
        }

        public static short ReadInt16(byte[] src, ref int offset) {
            return (short)ReadUInt16(src, ref offset);
        }

        public static ushort ReadUInt16(byte[] src, ref int offset) {
            ushort data = 0;
            for (byte i = 0; i < 2; i += 1) {
                data += (ushort)(src[offset] << (i * 8));
                offset += 1;
            }
            return data;
        }

        public static int ReadInt32(byte[] src, ref int offset) {
            return (int)ReadUInt32(src, ref offset);
        }

        public static uint ReadUInt32(byte[] src, ref int offset) {
            uint data = 0;
            for (byte i = 0; i < 4; i += 1) {
                data += (uint)src[offset] << (i * 8);
                offset += 1;
            }
            return data;
        }

        public static long ReadInt64(byte[] src, ref int offset) {
            return (long)ReadUInt64(src, ref offset);
        }

        public static ulong ReadUInt64(byte[] src, ref int offset) {
            ulong data = 0;
            for (byte i = 0; i < 8; i += 1) {
                data += (ulong)src[offset] << (i * 8);
                offset += 1;
            }
            return data;
        }

        public static float ReadSingle(byte[] src, ref int offset) {
            uint data = ReadUInt32(src, ref offset);
            FloatContent content = new FloatContent();
            content.uintValue = data;
            return content.floatValue;
        }

        public static double ReadDouble(byte[] src, ref int offset) {
            ulong data = ReadUInt64(src, ref offset);
            DoubleContent content = new DoubleContent();
            content.ulongValue = data;
            return content.doubleValue;
        }

        public static bool[] ReadBoolArr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            bool[] data = new bool[count];
            Buffer.BlockCopy(src, offset, data, 0, count);
            offset += count;
            return data;
        }

        public static byte[] ReadUInt8Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            byte[] data = new byte[count];
            Buffer.BlockCopy(src, offset, data, 0, count);
            offset += count;
            return data;
        }

        public static sbyte[] ReadInt8Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            sbyte[] data = new sbyte[count];
            Buffer.BlockCopy(src, offset, data, 0, count);
            offset += count;
            return data;
        }

        public static short[] ReadInt16Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            short[] data = new short[count];
            for (int i = 0; i < count; i += 1) {
                short d = ReadInt16(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static ushort[] ReadUInt16Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            ushort[] data = new ushort[count];
            for (int i = 0; i < count; i += 1) {
                ushort d = ReadUInt16(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static int[] ReadInt32Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            int[] data = new int[count];
            for (int i = 0; i < count; i += 1) {
                int d = ReadInt32(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static uint[] ReadUInt32Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            uint[] data = new uint[count];
            for (int i = 0; i < count; i += 1) {
                uint d = ReadUInt32(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static long[] ReadInt64Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            long[] data = new long[count];
            for (int i = 0; i < count; i += 1) {
                long d = ReadInt64(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static ulong[] ReadUInt64Arr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            ulong[] data = new ulong[count];
            for (int i = 0; i < count; i += 1) {
                ulong d = ReadUInt64(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static float[] ReadSingleArr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            float[] data = new float[count];
            for (int i = 0; i < count; i += 1) {
                float d = ReadSingle(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static double[] ReadDoubleArr(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            double[] data = new double[count];
            for (int i = 0; i < count; i += 1) {
                double d = ReadDouble(src, ref offset);
                data[i] = d;
            }
            return data;
        }

        public static string ReadUTF8String(byte[] src, ref int offset) {
            ushort count = ReadUInt16(src, ref offset);
            string data = Encoding.UTF8.GetString(src, offset, count);
            offset += count;
            return data;
        }

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

        // ==== VARINT ====
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

        public static ulong ReadVarintWithZigZag(byte[] src, ref int offset) {
            ulong udata = (ulong)ReadVarint(src, ref offset);
            ulong data = ReadZigZag(udata);
            return data;
        }

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