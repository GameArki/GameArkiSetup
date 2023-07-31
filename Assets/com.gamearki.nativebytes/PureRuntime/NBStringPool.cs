using System;
using System.Collections.Generic;

namespace GameArki.NativeBytes {

    public static class NBStringPool {

        static Dictionary<uint, string> pool;

        public static void Initialize(int capacity) {
            pool = new Dictionary<uint, string>(capacity);
        }

        public static string GetString(NBString nb) {
            uint hash = nb.BytesToHash();
            string str;
            if (pool.TryGetValue(hash, out str)) {
                nb.Dispose();
                return str;
            } else {
                str = nb.GetString();
                pool.Add(hash, str);
                nb.Dispose();
                return str;
            }
        }

        public static string GetCombineString(string src1, string src2) {
            NBString nb1 = new NBString(src1);
            NBString nb2 = new NBString(src2);
            return GetCombineString(stackalloc NBString[] { nb1, nb2 });
        }

        public static string GetCombineString(string src1, string src2, string src3) {
            NBString nb1 = new NBString(src1);
            NBString nb2 = new NBString(src2);
            NBString nb3 = new NBString(src3);
            return GetCombineString(stackalloc NBString[] { nb1, nb2, nb3 });
        }

        public static string GetCombineString(string src1, string src2, string src3, string src4) {
            NBString nb1 = new NBString(src1);
            NBString nb2 = new NBString(src2);
            NBString nb3 = new NBString(src3);
            NBString nb4 = new NBString(src4);
            return GetCombineString(stackalloc NBString[] { nb1, nb2, nb3, nb4 });
        }

        public static string GetCombineString(string src1, string src2, string src3, string src4, string src5) {
            NBString nb1 = new NBString(src1);
            NBString nb2 = new NBString(src2);
            NBString nb3 = new NBString(src3);
            NBString nb4 = new NBString(src4);
            NBString nb5 = new NBString(src5);
            return GetCombineString(stackalloc NBString[] { nb1, nb2, nb3, nb4, nb5 });
        }

        public static string GetCombineString(string src1, string src2, string src3, string src4, string src5, string src6) {
            NBString nb1 = new NBString(src1);
            NBString nb2 = new NBString(src2);
            NBString nb3 = new NBString(src3);
            NBString nb4 = new NBString(src4);
            NBString nb5 = new NBString(src5);
            NBString nb6 = new NBString(src6);
            return GetCombineString(stackalloc NBString[] { nb1, nb2, nb3, nb4, nb5, nb6 });
        }

        public static string GetCombineString(string src1, string src2, string src3, string src4, string src5, string src6, string src7) {
            NBString nb1 = new NBString(src1);
            NBString nb2 = new NBString(src2);
            NBString nb3 = new NBString(src3);
            NBString nb4 = new NBString(src4);
            NBString nb5 = new NBString(src5);
            NBString nb6 = new NBString(src6);
            NBString nb7 = new NBString(src7);
            return GetCombineString(stackalloc NBString[] { nb1, nb2, nb3, nb4, nb5, nb6, nb7 });
        }

        static string GetCombineString(in ReadOnlySpan<NBString> arr) {
            NBString combine = new NBString(arr, true);
            uint hash = combine.BytesToHash();
            string str;
            if (pool.TryGetValue(hash, out str)) {
                combine.Dispose();
                return str;
            } else {
                str = combine.GetString();
                pool.Add(hash, str);
                combine.Dispose();
                return str;
            }
        }

    }

}