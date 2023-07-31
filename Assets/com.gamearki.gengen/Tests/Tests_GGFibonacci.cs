using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using NUnit.Framework;

namespace GameArki.GenGen.Tests {

    public class Tests_GGFibonacci {

        [Test]
        public void RunTest() {

            TestCachedStandardFibonacci();
            TestStandardFibonacci();

        }

        void TestCachedStandardFibonacci() {
            // Reflected from GGFibonacciHelper.cacheFibonacci
            long[] array = typeof(GGFibonacciHelper).GetField("cacheFibonacci", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null) as long[];
            Assert.That(array, Is.Not.Null);
            int index = 2;
            if (index >= array.Length) {
                Assert.Fail("index >= array.Length");
            }
            for (int i = index; i < array.Length; i += 1) {
                var last2 = array[i - 2];
                var last1 = array[i - 1];
                var current = array[i];
                Assert.That(current, Is.EqualTo(last2 + last1));
            }
        }

        void TestStandardFibonacci() {
            long[] array = GGFibonacciHelper.GetStandardFibonacciArray(100);
            for (int i = 2; i < array.Length; i += 1) {
                var last2 = array[i - 2];
                var last1 = array[i - 1];
                var current = array[i];
                Assert.That(current, Is.EqualTo(last2 + last1));
            }
        }

    }

}