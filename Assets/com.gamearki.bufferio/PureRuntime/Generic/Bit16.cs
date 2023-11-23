using System.Runtime.InteropServices;

namespace GameArki.BufferIO {

    [StructLayout(LayoutKind.Explicit)]
    public struct Bit16 {

        [FieldOffset(0)]
        public short shortValue;

        [FieldOffset(0)]
        public ushort ushortValue;

        [FieldOffset(0)]
        public char charValue;

        [FieldOffset(0)]
        public byte b0;

        [FieldOffset(1)]
        public byte b1;

    }

}