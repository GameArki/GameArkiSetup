using System;
using System.Collections.Generic;

/*
*/
public class Race_StringAlloc {

    // mcs xx.cs
    // mono xx.exe
    public static void Main() {

        String_Const();
        String_New();
        String_ReadOnlySpan();
        String_PassParam();
        String_MapKey();

    }

    // const
    static void String_Const() {

        long start = GC.GetAllocatedBytesForCurrentThread();

        // 不开辟堆内存, "a" 在常量池中
        string strA = "a";

        long end = GC.GetAllocatedBytesForCurrentThread();
        System.Console.WriteLine(strA + " String_Const " + (end - start));

    }

    // new
    static void String_New() {

        long start = GC.GetAllocatedBytesForCurrentThread();

        // 开辟堆内存
        string strA = new string("a");

        long end = GC.GetAllocatedBytesForCurrentThread();
        System.Console.WriteLine(strA + " String_New " + (end - start));

    }

    // ReadOnlySpan
    static void String_ReadOnlySpan() {
        
        long start = GC.GetAllocatedBytesForCurrentThread();

        // 不开辟堆内存, "a" 在常量池中
        ReadOnlySpan<char> strB = "a";

        long end = GC.GetAllocatedBytesForCurrentThread();
        System.Console.WriteLine(strB.ToString() + " String_ReadOnlySpan " + (end - start));

    }

    // PassParam
    static void String_PassParam() {

        long start = GC.GetAllocatedBytesForCurrentThread();

        string strA = "a";
        // 传递引用, 不开辟堆内存
        PassParam(strA);

        long end = GC.GetAllocatedBytesForCurrentThread();
        System.Console.WriteLine(strA + " String_PassParam " + (end - start));
    }

    static void PassParam(string value) {

    }

    // Map Key
    static Dictionary<string, int> all = new Dictionary<string, int>();
    static void String_MapKey() {

        all.Add("yoyo", 1);

        long start = GC.GetAllocatedBytesForCurrentThread();

        string strA = "a";
        // 传递引用, 不开辟堆内存
        all.TryGetValue(strA, out int value);

        long end = GC.GetAllocatedBytesForCurrentThread();
        System.Console.WriteLine(strA + " String_MapKey " + (end - start));

    }

}