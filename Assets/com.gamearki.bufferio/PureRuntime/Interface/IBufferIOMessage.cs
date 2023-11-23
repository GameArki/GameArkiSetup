using System;

namespace GameArki.BufferIO {

    public interface IBufferIOMessage<T> {

        void WriteTo(byte[] dst, ref int offset);
        void FromBytes(byte[] src, ref int offset);

    }

}