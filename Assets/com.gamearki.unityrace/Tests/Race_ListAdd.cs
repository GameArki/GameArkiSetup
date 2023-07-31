using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

/*
    List Default Capacity: 112ms
    List Custom Capacity: 79ms

    Custom Capacity Win!
*/
public class Race_ListAdd {

    [Test]
    public void Run() {

        int count = 10000000;

        Stopwatch sw = new Stopwatch();

        // List1 - Default Capacity
        List<int> list1 = new List<int>();
        sw.Start();
        for (int i = 0; i < count; i++) {
            list1.Add(i);
        }
        sw.Stop();
        UnityEngine.Debug.Log("List1 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // List2 - Capacity
        List<int> list2 = new List<int>(count);
        sw.Start();
        for (int i = 0; i < count; i++) {
            list2.Add(i);
        }
        sw.Stop();
        UnityEngine.Debug.Log("List2 耗时" + sw.Elapsed.TotalMilliseconds);
    }
}
