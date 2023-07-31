using System;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

/*
    
    COUNT:          1000        10000       100000
    for:            0.0032      0.0306      0.3057
    vector(simd):   0.002       0.0018      0.0029
    parallel:       0.6658      0.3362      1.4242
                    单位(ms)
    
    vector(simd) Win!
*/
public class Race_Parallel {

    public struct AData {
        public int a;
        public int b;
    }

    [Test]
    public void Run([Values(1000, 10000, 100000)] int count) {

        UnityEngine.Debug.Log("COUNT: " + count);

        AData[] dataArr = new AData[count];

        Stopwatch sw = new Stopwatch();

        // Array
        sw.Start();
        for (int i = 0; i < dataArr.Length; i++) {
            dataArr[i].a += dataArr[i].b;
        }
        sw.Stop();
        UnityEngine.Debug.Log("Array 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Vector
        sw.Start();
        Vector<int> aVec = new Vector<int>(stackalloc int[count]);
        Vector<int> bVec = new Vector<int>(stackalloc int[count]);
        Vector.Add(aVec, bVec);
        sw.Stop();
        UnityEngine.Debug.Log("Vector 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset(); 

        // Parallel.For
        sw.Start();
        ParallelLoopResult res = Parallel.For(0, dataArr.Length, (i) => {
            dataArr[i].a += dataArr[i].b;
        });
        sw.Stop();
        UnityEngine.Debug.Log("Parallel.For 耗时" + sw.Elapsed.TotalMilliseconds);

    }

}