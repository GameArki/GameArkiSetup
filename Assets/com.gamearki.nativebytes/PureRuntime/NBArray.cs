using System;
using System.Runtime.InteropServices;

namespace GameArki.NativeBytes {

    public unsafe struct NBArray<T> where T : unmanaged {

        T* ptr;
        int count;
        public int Length => count;

        public NBArray(int count) {
            if (count == 0) {
                ptr = null;
                this.count = 0;
                return;
            }
            ptr = (T*)Marshal.AllocHGlobal(sizeof(T) * count);
            this.count = count;
        }

        public T this[int index] {
            get {
                if (ptr == null){
                    return default(T);
                } else {
                    return ptr[index];
                }
            }
            set {
                if (ptr == null) {
                    return;
                }
                ptr[index] = value;
            }
        }

        public void Dispose() {
            // if ptr is null, it means the struct is not initialized
            if (ptr == null) return;
            Marshal.FreeHGlobal((IntPtr)ptr);
        }

        public static implicit operator NBArray<T>(ReadOnlySpan<T> array) {
            NBArray<T> nbArray = new NBArray<T>(array.Length);
            for (int i = 0; i < array.Length; i++) {
                nbArray[i] = array[i];
            }
            return nbArray;
        }

        public static implicit operator ReadOnlySpan<T>(NBArray<T> nbArray) {
            if (nbArray.ptr == null) {
                return ReadOnlySpan<T>.Empty;
            }
            return new ReadOnlySpan<T>(nbArray.ptr, nbArray.count);
        }

    }

}