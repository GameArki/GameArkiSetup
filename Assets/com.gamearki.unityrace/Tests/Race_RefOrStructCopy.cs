using System;
using System.Diagnostics;
using NUnit.Framework;

/*
    SmallCopy: 99.3ms
    SmallRef: 81.4ms
    MidCopy: 99.75ms
    MidRef: 74.1ms
    BigCopy: 114.4ms
    BigRef: 95.1ms

    Ref Win!
*/
public class Race_RefOrStructCopy {

    struct TestStruct_Big {
        public long a;
        public long b;
        public long c;
        public long d;
        public long f;
        public long g;
    }

    struct TestStruct_Mid {
        public int a;
        public int b;
    }

    struct TestStruct_Small {
        public byte a;
        public byte b;
    }

    [Test]
    public void Run() {

        int count = 10000000;

        Stopwatch sw = new Stopwatch();

        // Small
        sw.Start();
        for (int i = 0; i < count; i++) {
            TestStruct_Small s = new TestStruct_Small() {
                a = (byte)i,
                b = (byte)i
            };
            byte v = SmallCopy(s);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Small Copy 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        sw.Start();
        for (int i = 0; i < count; i++) {
            TestStruct_Small s = new TestStruct_Small() {
                a = (byte)i,
                b = (byte)i
            };
            byte v = SmallRef(in s);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Small Ref 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Mid
        sw.Start();
        for (int i = 0; i < count; i++) {
            TestStruct_Mid s = new TestStruct_Mid() {
                a = i,
                b = i
            };
            int v = MidCopy(s);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Mid Copy 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        sw.Start();
        for (int i = 0; i < count; i++) {
            TestStruct_Mid s = new TestStruct_Mid() {
                a = i,
                b = i
            };
            int v = MidRef(in s);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Mid Ref 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Big
        sw.Start();
        for (int i = 0; i < count; i++) {
            TestStruct_Big s = new TestStruct_Big() {
                a = i,
                b = i,
                c = i,
                d = i,
                f = i,
                g = i
            };
            long v = BigCopy(s);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Big Copy 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        sw.Start();
        for (int i = 0; i < count; i++) {
            TestStruct_Big s = new TestStruct_Big() {
                a = i,
                b = i,
                c = i,
                d = i,
                f = i,
                g = i
            };
            long v = BigRef(in s);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Big Ref 耗时" + sw.Elapsed.TotalMilliseconds);

    }

    byte SmallCopy(TestStruct_Small s) {
        return (byte)(s.a + s.b);
    }

    byte SmallRef(in TestStruct_Small s) {
        return (byte)(s.a + s.b);
    }

    int MidCopy(TestStruct_Mid s) {
        return s.a + s.b;
    }

    int MidRef(in TestStruct_Mid s) {
        return s.a + s.b;
    }

    long BigCopy(TestStruct_Big s) {
        return s.a + s.b + s.c + s.d + s.f + s.g;
    }

    long BigRef(in TestStruct_Big s) {
        return s.a + s.b + s.c + s.d + s.f + s.g;
    }

}
