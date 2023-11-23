using System;
using System.Collections.Generic;

namespace GameArki.BufferIO {

    public static class BufferReaderExtra {

        public static T ReadMessage<T>(byte[] src, Func<T> createHandle, ref int offset) where T : struct, IBufferIOMessage<T> {
            T msg = createHandle.Invoke();
            msg.FromBytes(src, ref offset);
            return msg;
        }

        public static T[] ReadMessageArr<T>(byte[] src, Func<T> createHandle, ref int offset) where T : struct, IBufferIOMessage<T> {
            ushort totalCount = BufferReader.ReadUInt16(src, ref offset);
            T[] arr = new T[totalCount];
            for (int i = 0; i < arr.Length; i += 1) {
                arr[i] = ReadMessage(src, createHandle, ref offset);
            }
            return arr;
        }

        public static List<T> ReadMessageList<T>(byte[] src, Func<T> createHandle, ref int offset) where T : struct, IBufferIOMessage<T> {
            ushort totalCount = BufferReader.ReadUInt16(src, ref offset);
            List<T> list = new List<T>(totalCount);
            for (int i = 0; i < totalCount; i += 1) {
                list.Add(ReadMessage(src, createHandle, ref offset));
            }
            return list;
        }

    }
}