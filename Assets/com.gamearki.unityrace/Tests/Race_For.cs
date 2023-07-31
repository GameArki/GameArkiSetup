using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

/*
    for: 22.2ms
    foreach: 23.16ms
    while: 25.47ms
    goto: 31.21ms
    
    for Win!
*/
public class Race_For {

    [Test]
    public void Run() {

        int[] array = new int[10000000];

        Stopwatch sw = new Stopwatch();

        // For
        sw.Start();
        for (int i = 0; i < array.Length; i++) {
            array[i] = i;
        }
        sw.Stop();
        UnityEngine.Debug.Log("For 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Foreach
        sw.Start();
        foreach (var item in array) {
            int i = item;
        }
        sw.Stop();
        UnityEngine.Debug.Log("Foreach 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // While
        sw.Start();
        int j = 0;
        while (j < array.Length) {
            int i = array[j];
            j++;
        }
        sw.Stop();
        UnityEngine.Debug.Log("While 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // Goto
        sw.Start();
        int k = 0;
        goto start;
    loop:
        int l = array[k];
        k++;
    start:
        if (k < array.Length) {
            goto loop;
        }
        sw.Stop();
        UnityEngine.Debug.Log("Goto 耗时" + sw.Elapsed.TotalMilliseconds);

    }

}