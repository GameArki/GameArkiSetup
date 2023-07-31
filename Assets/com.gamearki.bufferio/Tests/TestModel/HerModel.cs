using System;

namespace GameArki.BufferIO.Tests
{
    [BufferIOMessageObject]
    public class HerModel : IBufferIOMessage<HerModel>
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

        public int GetEvaluatedSize(out bool isCertain)
        {
            int count = 4;
            isCertain = true;
            return count;
        }

        public byte[] ToBytes()
        {
            int count = GetEvaluatedSize(out bool isCertain);
            int offset = 0;
            byte[] src = new byte[count];
            WriteTo(src, ref offset);
            if (isCertain)
            {
                return src;
            }
            else
            {
                byte[] dst = new byte[offset];
                Buffer.BlockCopy(src, 0, dst, 0, offset);
                return dst;
            }
        }
    }
}