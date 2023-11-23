using System;
using GameArki.NoBuf;
using GameArki.NativeBytes;

namespace GameArki.BufferIO.Sample {
    [BufferIOMessageObject]
    public struct HerModel : IBufferIOMessage<HerModel> {
        public int value;

        public void WriteTo(byte[] dst, ref int offset) {
            NBWriter.W_I32(dst, value, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset) {
            value = NBReader.R_I32(src, ref offset);
        }
    }
}