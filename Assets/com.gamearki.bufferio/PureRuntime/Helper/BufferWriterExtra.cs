using System;

namespace GameArki.BufferIO {

    public static class BufferWriterExtra {

        public static void WriteMessage<T>(byte[] dst, T data, ref int offset)  where T : IBufferIOMessage<T> {
            if (data != null) {
                byte[] b = data.ToBytes();
                ushort count = (ushort)b.Length;
                BufferWriter.WriteUInt16(dst, count, ref offset);
                Buffer.BlockCopy(b, 0, dst, offset, count);
                offset += count;
            } else {
                BufferWriter.WriteUInt16(dst, 0, ref offset);
            }
        }

        public static void WriteMessageArr<T>(byte[] dst, T[] data, ref int offset)  where T : IBufferIOMessage<T> {
            if (data != null) {
                ushort count = (ushort)data.Length;
                BufferWriter.WriteUInt16(dst, count, ref offset);
                for (int i = 0; i < data.Length; i += 1) {
                    WriteMessage(dst, data[i], ref offset);
                }
            } else {
                BufferWriter.WriteUInt16(dst, 0, ref offset);
            }
        }

    }
}