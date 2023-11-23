using System.Runtime.InteropServices;

namespace GameArki.BufferIO {

    [StructLayout(LayoutKind.Explicit)]
    public struct Bit32 {

        [FieldOffset(0)]
        public int intValue;

        [FieldOffset(0)]
        public uint uintValue;

        [FieldOffset(0)]
        public float floatValue;

        [FieldOffset(0)]
        public byte b0;

        [FieldOffset(1)]
        public byte b1;

        [FieldOffset(2)]
        public byte b2;

        [FieldOffset(3)]
        public byte b3;

    }
}