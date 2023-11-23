using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace GameArki.BufferIO {

    public static class BufferWriter {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBool(byte[] dst, bool data, ref int offset) {
            byte b = data ? (byte)1 : (byte)0;
            WriteUInt8(dst, b, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt8(byte[] dst, sbyte data, ref int offset) {
            WriteUInt8(dst, (byte)data, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt8(byte[] dst, byte data, ref int offset) {
            dst[offset++] = data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteChar(byte[] dst, char data, ref int offset) {
            Bit16 content = new Bit16();
            content.charValue = data;
            dst[offset++] = content.b0;
            dst[offset++] = content.b1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt16(byte[] dst, short data, ref int offset) {
            WriteUInt16(dst, (ushort)data, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt16(byte[] dst, ushort data, ref int offset) {
            Bit16 content = new Bit16();
            content.ushortValue = data;
            dst[offset++] = content.b0;
            dst[offset++] = content.b1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteSingle(byte[] dst, float data, ref int offset) {
            Bit32 content = new Bit32();
            content.floatValue = data;
            dst[offset++] = content.b0;
            dst[offset++] = content.b1;
            dst[offset++] = content.b2;
            dst[offset++] = content.b3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt32(byte[] dst, int data, ref int offset) {
            WriteUInt32(dst, (uint)data, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt32(byte[] dst, uint data, ref int offset) {
            Bit32 content = new Bit32();
            content.uintValue = data;
            dst[offset++] = content.b0;
            dst[offset++] = content.b1;
            dst[offset++] = content.b2;
            dst[offset++] = content.b3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDouble(byte[] dst, double data, ref int offset) {
            Bit64 content = new Bit64();
            content.doubleValue = data;
            dst[offset++] = content.b0;
            dst[offset++] = content.b1;
            dst[offset++] = content.b2;
            dst[offset++] = content.b3;
            dst[offset++] = content.b4;
            dst[offset++] = content.b5;
            dst[offset++] = content.b6;
            dst[offset++] = content.b7;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt64(byte[] dst, long data, ref int offset) {
            WriteUInt64(dst, (ulong)data, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt64(byte[] dst, ulong data, ref int offset) {
            Bit64 content = new Bit64();
            content.ulongValue = data;
            dst[offset++] = content.b0;
            dst[offset++] = content.b1;
            dst[offset++] = content.b2;
            dst[offset++] = content.b3;
            dst[offset++] = content.b4;
            dst[offset++] = content.b5;
            dst[offset++] = content.b6;
            dst[offset++] = content.b7;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUTF8String(byte[] dst, string data, ref int offset) {
            if (!string.IsNullOrEmpty(data)) {
                byte[] d = Encoding.UTF8.GetBytes(data);
                ushort count = (ushort)d.Length;
                WriteUInt16(dst, count, ref offset);
                Buffer.BlockCopy(d, 0, dst, offset, count);
                offset += count;
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUTF8StringArr(byte[] dst, string[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Length; i += 1) {
                    WriteUTF8String(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUTF8StringList(byte[] dst, List<string> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteUTF8String(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBoolArr(byte[] dst, bool[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                Buffer.BlockCopy(data, 0, dst, offset, count);
                offset += count;
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteBoolList(byte[] dst, List<bool> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteBool(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt8Arr(byte[] dst, sbyte[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                Buffer.BlockCopy(data, 0, dst, offset, count);
                offset += count;
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt8List(byte[] dst, List<sbyte> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteInt8(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUint8Arr(byte[] dst, byte[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                Buffer.BlockCopy(data, 0, dst, offset, count);
                offset += count;
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUint8List(byte[] dst, List<byte> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteUInt8(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt16Arr(byte[] dst, short[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteInt16(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt16List(byte[] dst, List<short> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteInt16(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt16Arr(byte[] dst, ushort[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteUInt16(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt16List(byte[] dst, List<ushort> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteUInt16(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt32Arr(byte[] dst, int[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteInt32(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt32List(byte[] dst, List<int> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteInt32(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt32Arr(byte[] dst, uint[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteUInt32(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt32List(byte[] dst, List<uint> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteUInt32(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteSingleArr(byte[] dst, float[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteSingle(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteSingleList(byte[] dst, List<float> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteSingle(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt64Arr(byte[] dst, long[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteInt64(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt64List(byte[] dst, List<long> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteInt64(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt64Arr(byte[] dst, ulong[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteUInt64(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUInt64List(byte[] dst, List<ulong> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteUInt64(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDoubleArr(byte[] dst, double[] data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Length;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < count; i += 1) {
                    WriteDouble(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteDoubleList(byte[] dst, List<double> data, ref int offset) {
            if (data != null) {
                ushort count = (ushort)data.Count;
                WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Count; i += 1) {
                    WriteDouble(dst, data[i], ref offset);
                }
            } else {
                WriteUInt16(dst, 0, ref offset);
            }
        }

        // ==== VARINT ====
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteVarint(byte[] dst, ulong data, ref int offset) {
            while (data >= 0x80) {
                dst[offset++] = (byte)(data | 0x80);
                data >>= 7;
            }
            dst[offset++] = (byte)data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteVarintWithZigZag(byte[] dst, long data, ref int offset) {
            ulong udata = WriteZigZag(data);
            WriteVarint(dst, udata, ref offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ulong WriteZigZag(long value) {
            bool isNegative = value < 0;
            ulong uv = (ulong)value;
            unchecked {
                if (isNegative) {
                    return ((~uv + 1ul) << 1) + 1ul;
                } else {
                    return uv << 1;
                }
            }
        }

    }

}