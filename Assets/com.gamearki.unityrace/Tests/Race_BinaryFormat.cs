using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NUnit.Framework;

/*
    U32Bytes write: 8ms
    U32Bytes read: 12ms
    bit operation write: 9ms
    bit operation read: 8ms
    
    U32Bytes write Win!
    bit operation read Win!
*/
public class Race_BinaryFormat {

    [StructLayout(LayoutKind.Explicit)]
    struct U32Bytes {
        [FieldOffset(0)]
        public uint value;
        [FieldOffset(0)]
        public byte b0;
        [FieldOffset(1)]
        public byte b1;
        [FieldOffset(2)]
        public byte b2;
        [FieldOffset(3)]
        public byte b3;
    }

    [Test]
    public void Run() {

        // Read Write 1000000 times
        int times = 100;
        byte[] data = new byte[times * 4];
        int offset = 0;

        Stopwatch sw = new Stopwatch();

        // U32Bytes: write
        sw.Start();
        for (int i = 0; i < times; i++) {
            U32Bytes u32Bytes = new U32Bytes() {
                value = (uint)i
            };
            data[offset++] = u32Bytes.b0;
            data[offset++] = u32Bytes.b1;
            data[offset++] = u32Bytes.b2;
            data[offset++] = u32Bytes.b3;
        }
        sw.Stop();
        UnityEngine.Debug.Log("U32Bytes write: " + sw.Elapsed.Ticks + "ms");

        sw.Reset();
        offset = 0;

        // U32Bytes: read
        sw.Start();
        for (int i = 0; i < times; i++) {
            U32Bytes u32Bytes = new U32Bytes() {
                b0 = data[offset++],
                b1 = data[offset++],
                b2 = data[offset++],
                b3 = data[offset++]
            };
            uint value = u32Bytes.value;
        }
        sw.Stop();
        UnityEngine.Debug.Log("U32Bytes read: " + sw.Elapsed.Ticks + "ms");

        sw.Reset();
        offset = 0;

        // bit operation: write
        sw.Start();
        for (int i = 0; i < times; i++) {
            uint value = (uint)i;
            data[offset++] = (byte)(value);
            data[offset++] = (byte)(value >> 8);
            data[offset++] = (byte)(value >> 16);
            data[offset++] = (byte)(value >> 24);
        }
        sw.Stop();
        UnityEngine.Debug.Log("bit operation write: " + sw.Elapsed.Ticks + "ms");

        sw.Reset();
        offset = 0;
        
        // bit operation: read
        sw.Start();
        for (int i = 0; i < times; i++) {
            uint value = (uint)(
                data[offset++] |
                data[offset++] << 8 |
                data[offset++] << 16 |
                data[offset++] << 24
            );
        }
        sw.Stop();
        UnityEngine.Debug.Log("bit operation read: " + sw.Elapsed.Ticks + "ms");

    }

}