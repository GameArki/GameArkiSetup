using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

/*
    COUNT:          100        10000       1000000
    stack.push:     0.0028     0.0365      5.187
    queue.enqueue:  0.0027     0.0402      8.0967
    stack.pop:      0.0005     0.0183      1.7784
    queue.dequeue   0.0004     0.0649      10.556 
                    单位(ms)
    
    多次测试，入栈入队效率差不多。
    只有数据量大的时候，出队效率远比出栈效率慢。
*/
public class Race_StackOrQueue {

    [Test]
    public void Run100([Values(100, 10000, 1000000)] int count) {

        Stopwatch sw = new Stopwatch();

        // stack - push
        Stack<int> stack = new Stack<int>();
        sw.Start();
        for (int i = 0; i < count; i++) {
            stack.Push(i);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Stack入栈 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // queue - enqueue
        Queue<int> queue = new Queue<int>();
        sw.Start();
        for (int i = 0; i < count; i++) {
            queue.Enqueue(i);
        }
        sw.Stop();
        UnityEngine.Debug.Log("Queue入队 耗时" + sw.Elapsed.TotalMilliseconds);


        sw.Reset();
        // stack - pop
        sw.Start();
        for (int i = 0; i < count; i++) {
            stack.Pop();
        }
        sw.Stop();
        UnityEngine.Debug.Log("Stack出栈 耗时" + sw.Elapsed.TotalMilliseconds);

        sw.Reset();

        // queue - dequeue
        sw.Start();
        for (int i = 0; i < count; i++) {
            queue.Dequeue();
        }
        sw.Stop();
        UnityEngine.Debug.Log("Queue出队 耗时" + sw.Elapsed.TotalMilliseconds);
    }

}
