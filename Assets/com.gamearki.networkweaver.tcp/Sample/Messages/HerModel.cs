using System;
using GameArki.BufferIO;

namespace GameArki.Network.Sample
{
    [BufferIOMessageObject]
    public struct HerModel : IBufferIOMessage<HerModel>
    {
        public int value;

        public void WriteTo(byte[] dst, ref int offset)
        {
            BufferWriter.WriteInt32(dst, value, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset)
        {
            value = BufferReader.ReadInt32(src, ref offset);
        }

    }
}