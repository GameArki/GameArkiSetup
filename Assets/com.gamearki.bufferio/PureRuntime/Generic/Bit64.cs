using System.Runtime.InteropServices;

namespace GameArki.BufferIO {

    [StructLayout(LayoutKind.Explicit)]
    public struct Bit64 {

        [FieldOffset(0)]
        public long longValue;

        [FieldOffset(0)]
        public ulong ulongValue;

        [FieldOffset(0)]
        public double doubleValue;

        [FieldOffset(0)]
        public byte b0;

        [FieldOffset(1)]
        public byte b1;

        [FieldOffset(2)]
        public byte b2;

        [FieldOffset(3)]
        public byte b3;

        [FieldOffset(4)]
        public byte b4;

        [FieldOffset(5)]
        public byte b5;

        [FieldOffset(6)]
        public byte b6;

        [FieldOffset(7)]
        public byte b7;

    }

}