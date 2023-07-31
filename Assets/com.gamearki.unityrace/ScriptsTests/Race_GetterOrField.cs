using System;
using System.Diagnostics;
using NUnit.Framework;

/*
    Field: 39ms
    Getter: 100ms
    
    Field Win!
*/
public class Race_GetterOrField {

    struct TestStruct {
        public int a;
        public int A => a;
        public int b;
        public int B => b;
    }

    [Test]
    public void Run() {

        int count = 10000000;
        Span<TestStruct> testStructs = new TestStruct[count];
        for (int i = 0; i < count; i++) {
            testStructs[i] = new TestStruct() {
                a = i,
                b = i
            };
        }

        Stopwatch sw = new Stopwatch();

        // Getter
        sw.Start();
        for (int i = 0; i < count; i++) {
            int a = testStructs[i].A;
            int b = testStructs[i].B;
        }
        sw.Stop();
        UnityEngine.Debug.Log("Getter 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Field
        sw.Start();
        for (int i = 0; i < count; i++) {
            int a = testStructs[i].a;
            int b = testStructs[i].b;
        }
        sw.Stop();
        UnityEngine.Debug.Log("Field 耗时" + sw.Elapsed.TotalMilliseconds);

    }

}
