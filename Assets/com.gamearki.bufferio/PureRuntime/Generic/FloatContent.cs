using System.Runtime.InteropServices;

namespace GameArki.BufferIO {

    [StructLayout(LayoutKind.Explicit)]
    internal struct FloatContent {
        [FieldOffset(0)]
        public float floatValue;
        [FieldOffset(0)]
        public uint uintValue;
    }
}