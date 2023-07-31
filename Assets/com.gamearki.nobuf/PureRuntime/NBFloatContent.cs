using System.Runtime.InteropServices;

namespace GameArki.NoBuf {

    [StructLayout(LayoutKind.Explicit)]
    public struct NBFloatContent {
        [FieldOffset(0)]
        public float f;
        [FieldOffset(0)]
        public uint i;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct NBDoubleContent {
        [FieldOffset(0)]
        public double f;
        [FieldOffset(0)]
        public ulong i;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct NBDecimalContent {
        [FieldOffset(0)]
        public decimal f;
        [FieldOffset(0)]
        public ulong i1;
        [FieldOffset(8)]
        public ulong i2;
    }

}