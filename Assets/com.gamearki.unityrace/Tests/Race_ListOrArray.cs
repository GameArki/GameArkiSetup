using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

/*
    List: 75.67ms
    Array: 25.2ms

    Array Win!
*/
public class Race_ListOrArray {

    [Test]
    public void Run() {

        int count = 10000000;

        Stopwatch sw = new Stopwatch();

        List<int> list = new List<int>(count);
        int[] array = new int[count];
        for (int i = 0; i < count; i++) {
            list.Add(i);
            array[i] = i;
        }

        // List
        sw.Start();
        for (int i = 0; i < count; i++) {
            int v = list[i];
        }
        sw.Stop();
        UnityEngine.Debug.Log("List 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Array
        sw.Start();
        for (int i = 0; i < count; i++) {
            int v = array[i];
        }
        sw.Stop();
        UnityEngine.Debug.Log("Array 耗时" + sw.Elapsed.TotalMilliseconds);

    }

}