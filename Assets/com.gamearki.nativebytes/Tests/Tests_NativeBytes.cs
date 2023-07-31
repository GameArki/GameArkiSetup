using System;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace GameArki.NativeBytes.Tests {

    public class Tests_NativeBytes {

        HashSet<uint> set;

        [Test]
        public void Run_NBString() {

            set = new HashSet<uint>();

            string str1 = "yoyo";
            NBString nb1 = new NBString(str1);
            uint hash1 = nb1.BytesToHash();

            string str2 = "yoyo2";
            NBString nb2 = new NBString(str2);
            uint hash2 = nb2.BytesToHash();

            string str3 = "yoyo";
            NBString nb3 = new NBString(str3);
            uint hash3 = nb3.BytesToHash();

            NBString concat1 = new NBString(stackalloc NBString[] { nb1, nb2, nb3 }, false);
            uint hashConcat1 = concat1.BytesToHash();
            string strConcat1 = concat1.GetString();
            Assert.AreEqual(strConcat1, str1 + str2 + str3);

            NBString concat2 = new NBString(stackalloc NBString[] { nb2, nb1, nb3 }, false);
            uint hashConcat2 = concat2.BytesToHash();
            string strConcat2 = concat2.GetString();
            Assert.AreEqual(strConcat2, str2 + str1 + str3);

            bool succ;
            succ = set.Add(hash1);
            Assert.IsTrue(succ);
            Assert.AreEqual(nb1.GetString(), "yoyo");
            Assert.AreNotEqual(nb1.GetString(), nb2.GetStringAndDispose());
            Assert.AreEqual(nb1.GetStringAndDispose(), nb3.GetStringAndDispose());

            succ = set.Add(hash2);
            Assert.IsTrue(succ);

            succ = set.Add(hash3);
            Assert.IsFalse(succ);

            Assert.AreNotSame(hash1, hash2);

            bool isSame = hash1 == hash2;
            Assert.IsFalse(isSame);

            Assert.AreNotEqual(hashConcat1, hashConcat2);
            Assert.AreNotEqual(concat1, concat2);
            concat1.Dispose();
            concat2.Dispose();

        }

        [Test]
        public void Run_NBString_GCAlloc() {

            NBStringPool.Initialize(100);

            string s1 = new string("yo");
            string s2 = new string("1");
            string s3 = new string("35");
            string s4 = new string("hehehei你好啊");
            string s5 = new string("世界");

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            long start = GC.GetTotalMemory(true);
            sw.Start();
            for (int i = 0; i < 100; i += 1) {
                string str = NBStringPool.GetCombineString(s1, s2, s3, s4);
            }
            sw.Stop();
            long end = GC.GetTotalMemory(true);
            Debug.Log("GCAlloc NBString Alloc: " + (end - start) + "time cost: " + sw.Elapsed.TotalMilliseconds);

            sw.Reset();

            start = GC.GetTotalMemory(true);
            sw.Start();
            for (int i = 0; i < 100; i += 1) {
                string str = s1;
                str += s2;
                str += s3;
                str += s4;
            }
            sw.Stop();
            end = GC.GetTotalMemory(true);
            Debug.Log("GCAlloc String Alloc: " + (end - start) + "time cost: " + sw.Elapsed.TotalMilliseconds);

        }

        [Test]
        public void Run_NBArray() {

            NBArray<int> arr = new NBArray<int>(10);
            for (int i = 0; i < 10; i++) {
                arr[i] = i;
            }

            for (int i = 0; i < 10; i++) {
                Assert.AreEqual(arr[i], i);
            }

            arr.Dispose();
        }

    }
}
