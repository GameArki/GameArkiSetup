using System.Runtime.InteropServices;

namespace GameArki.BufferIO {

    [StructLayout(LayoutKind.Explicit)]
    public struct Bit128 {

        [FieldOffset(0)]
        public decimal decimalValue;

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

        [FieldOffset(8)]
        public byte b8;

        [FieldOffset(9)]
        public byte b9;

        [FieldOffset(10)]
        public byte b10;

        [FieldOffset(11)]
        public byte b11;

        [FieldOffset(12)]
        public byte b12;

        [FieldOffset(13)]
        public byte b13;

        [FieldOffset(14)]
        public byte b14;

        [FieldOffset(15)]
        public byte b15;

    }

}