using System;
using System.Diagnostics;
using NUnit.Framework;

/*
    NoFunc: 25.3ms
    Normal: 52.8ms
    Inline: 52.8ms

    NoFunc Win!
*/
public class Race_InlineFuncOrNormal {

    [Test]
    public void Run() {

        int count = 100000000;

        Stopwatch sw = new Stopwatch();

        // No Func
        sw.Start();
        for (int i = 0; i < count; i++) {
            int v = i + i + i + i;
        }
        sw.Stop();
        UnityEngine.Debug.Log("No Func 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Normal
        sw.Start();
        for (int i = 0; i < count; i++) {
            int v = Normal(i, i, i, i);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Normal 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Inline
        int Inline(int a, int b, int c, int d) {
            return a + b + c + d;
        }

        sw.Start();
        for (int i = 0; i < count; i++) {
            int v = Inline(i, i, i, i);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Inline 耗时" + sw.Elapsed.TotalMilliseconds);

    }

    int Normal(int a, int b, int c, int d) {
        return a + b + c + d;
    }

}
