using System;
using System.Text;
using System.Runtime.InteropServices;

namespace GameArki.NativeBytes {

    public unsafe struct NBString {

        public static NBString Empty => new NBString();

        byte* ptr;
        int length;
        public int Length => length;

        public NBString(ReadOnlySpan<NBString> arr, bool isAutoDispose) {

            if (arr.Length == 0) {
                ptr = null;
                length = 0;
                return;
            }

            int totalLen = 0;
            for (int i = 0; i < arr.Length; i++) {
                totalLen += arr[i].Length - 1;
            }
            this.length = totalLen + 1;

            this.ptr = (byte*)Marshal.AllocCoTaskMem(length);
            int lastLen = 0;
            for (int i = 0; i < arr.Length; i++) {
                int len = arr[i].Length - 1;
                Buffer.MemoryCopy(arr[i].ptr, ptr + lastLen, len, len);
                lastLen += len;
                if (isAutoDispose) {
                    Marshal.ZeroFreeCoTaskMemUTF8((IntPtr)arr[i].ptr);
                }
            }
            ptr[length - 1] = 0;

        }

        public NBString(string src) {

            if (src == null) {
                ptr = null;
                length = 0;
                return;
            }

            ptr = (byte*)Marshal.StringToCoTaskMemUTF8(src);
            length = 0;
            length = Len(ptr);

        }

        public NBString(ReadOnlySpan<byte> srcWithoutNullTerminal) {

            if (srcWithoutNullTerminal.Length == 0) {
                ptr = null;
                length = 0;
                return;
            }

            ptr = (byte*)Marshal.AllocCoTaskMem(srcWithoutNullTerminal.Length + 1);
            fixed (byte* p = srcWithoutNullTerminal) {
                Buffer.MemoryCopy(p, ptr, srcWithoutNullTerminal.Length, srcWithoutNullTerminal.Length);
            }

            this.length = 0;
            this.length = Len(ptr);
            ptr[length - 1] = 0;

        }

        public NBString(Span<byte> srcWithoutNullTerminal) {

            if (srcWithoutNullTerminal.Length == 0) {
                ptr = null;
                length = 0;
                return;
            }

            ptr = (byte*)Marshal.AllocCoTaskMem(srcWithoutNullTerminal.Length + 1);
            fixed (byte* p = srcWithoutNullTerminal) {
                Buffer.MemoryCopy(p, ptr, srcWithoutNullTerminal.Length, srcWithoutNullTerminal.Length);
            }

            this.length = 0;
            this.length = Len(ptr);
            ptr[length - 1] = 0;

        }

        int Len(byte* ptr) {
            int len = 0;
            while (ptr[len] != 0) {
                len++;
            }
            len++;
            return len;
        }

        public byte this[int index] {
            get => ptr[index];
        }

        public string GetString() {
            if (ptr == null) return null;
            return Marshal.PtrToStringUTF8((IntPtr)ptr, length - 1);
        }

        public string GetStringAndDispose() {
            string str = GetString();
            Dispose();
            return str;
        }

        public void Dispose() {
            if (ptr == null) return;
            Marshal.ZeroFreeCoTaskMemUTF8((IntPtr)ptr);
        }

        public uint BytesToHash() {
            if (ptr == null) return 0;
            return XXHash.CalculateHash(ptr, length - 1);
        }

    }

}