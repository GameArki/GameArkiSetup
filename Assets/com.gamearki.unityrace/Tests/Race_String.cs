using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

/*
  字符串拼接
    Splice Count:                 1           5          10         100          1000           10000
    StringBuilder:              0.0252     0.0013      0.0014      0.0094       0.0761          0.7131
    String Interpolation:       0.0019     0.0009      0.0031      0.0261       0.4142          126.6165
    String Concat:              0.0047     0.0037      0.0033      0.0202       0.298           127.9383
    String Format:              0.0031     0.0009      0.0021      0.0113       0.0822          0.7553
    拼接次数少的时候，String Interpolation 效率高。
    拼接次数多的时候，StringBuilder 效率高。
*/
public class Race_String {

    [Test]
    public void Run([Values(1, 5, 10, 100, 1000, 10000)] int count) {
        Stopwatch sw = new Stopwatch();
        string s = "";

        sw.Start();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < count; i++) {
            sb.Append(i);
        }
        s = sb.ToString();
        sw.Stop();
        UnityEngine.Debug.Log($"StringBuilder: {count},{sw.Elapsed.TotalMilliseconds}ms");
        sw.Reset();

        s = "";
        sw.Start();
        for (int i = 0; i < count; i++) {
            s += $"{i}";
        }
        sw.Stop();
        UnityEngine.Debug.Log($"String Interpolation:{count},{sw.Elapsed.TotalMilliseconds}ms");
        sw.Reset();
        GC.Collect();

        s = "";
        sw.Start();
        for (int i = 0; i < count; i++) {
            s += i;
        }
        sw.Stop();
        UnityEngine.Debug.Log($"String Concat: {count},{sw.Elapsed.TotalMilliseconds}ms");
        sw.Reset();
        GC.Collect();

        s = "";
        sw.Start();
        for (int i = 0; i < count; i++) {
            s = string.Format(s, i);
        }
        sw.Stop();
        UnityEngine.Debug.Log($"String Format: {count},{sw.Elapsed.TotalMilliseconds}ms");
        sw.Reset();

    }
}