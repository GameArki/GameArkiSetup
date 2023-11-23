using System;

namespace GameArki.BufferIO.Tests
{
    [BufferIOMessageObject]
    public struct HerModel : IBufferIOMessage<HerModel>
    {
        public int value;
        // 自动生成
        bool GetMaxSize(out int count)
        {
            bool isCertain = true;
            // 确定长度 + 字符串预估长度 + 自定义类型长度
            count = 1 + 2;
            // 是否确定的长度
            // 如果有字符串或自定义类型, 返回 false
            // 否则, 返回 true
            return isCertain;
        }

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