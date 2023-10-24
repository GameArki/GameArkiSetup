using UnityEngine;
using NUnit.Framework;

namespace GameArki.FPEasing.Tests {

    public class Tests_FPEasing {

        [Test]
        public void Test_EasingFunction() {

            float v = FunctionHelper.EaseInSine(0);
            Assert.AreEqual(v, 0);

            v = FunctionHelper.EaseInSine(0.02f);
            Assert.That(IsNear(v, 0.000493466912f, 0.000001f));

            v = FunctionHelper.EaseInSine(1);
            Assert.AreEqual(v, 1);

            int v5 = Mathf.Abs(-5);

        }

        bool IsNear(float a1, float a2, float allowDiff) {
            float res = Mathf.Abs(Mathf.Abs(a1) - Mathf.Abs(a2));
            return res <= allowDiff;
        }

    }

}