using System;
using GameArki.BufferIO;

namespace GameArki.BufferIO.Tests
{
    [BufferIOMessageObject]
    public struct HerModel : IBufferIOMessage<HerModel>
    {
        public string name;
        public int value;
        public void WriteTo(byte[] dst, ref int offset)
        {
            BufferWriter.WriteUTF8String(dst, name, ref offset);
            BufferWriter.WriteInt32(dst, value, ref offset);
        }

        public void FromBytes(byte[] src, ref int offset)
        {
            name = BufferReader.ReadUTF8String(src, ref offset);
            value = BufferReader.ReadInt32(src, ref offset);
        }
    }
}