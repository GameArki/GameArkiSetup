using System;
using UnityEngine;
using NUnit.Framework;

namespace GameArki.FPEasing.Tests {

    public class Tests_FPEasing
    {
        
        void Test_RunFunction(Func<float, float> func , float x , float y)
        {
            Assert.That(IsNear(func(x), y, 0.000001f));
        }
        
        [Test]
        public void Test_EasingFunction() {

            float v = FunctionHelper.EaseInSine(0);
            Assert.AreEqual(v, 0);

            v = FunctionHelper.EaseInSine(0.02f);
            Assert.That(IsNear(v, 0.000493466912f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.03f);
            Assert.That(IsNear(v, 0.001110137f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.14f);
            Assert.That(IsNear(v, 0.02408326f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.25f);
            Assert.That(IsNear(v, 0.0761205f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.28f);
            Assert.That(IsNear(v, 0.09517294f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.3899999f);
            Assert.That(IsNear(v, 0.1818502f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.5299998f);
            Assert.That(IsNear(v, 0.3269873f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.6399997f);
            Assert.That(IsNear(v, 0.4641728f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.7799996f);
            Assert.That(IsNear(v, 0.6612614f, 0.000001f));
            
            v = FunctionHelper.EaseInSine(0.8899994f);
            Assert.That(IsNear(v, 0.82807f, 0.000001f));

            v = FunctionHelper.EaseInSine(1);
            Assert.AreEqual(v, 1);
            
        }
        
        [Test]
        public void Test_EaseImmediate() {

            float v = FunctionHelper.EaseImmediate(0);
            Assert.AreEqual(v, 1);

            v = FunctionHelper.EaseImmediate(0.02f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.03f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.14f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.25f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.28f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.3899999f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.5299998f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.6399997f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.7799996f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
            v = FunctionHelper.EaseImmediate(0.8899994f);
            Assert.That(IsNear(v, 1f, 0.000001f));

            v = FunctionHelper.EaseImmediate(1);
            Assert.AreEqual(v, 1);
            
        }

        [Test]
        public void Test_EaseLinear() {

            float v = FunctionHelper.EaseLinear(0);
            Assert.That(IsNear(v, 0f, 0.000001f));

            v = FunctionHelper.EaseLinear(0.02f);
            Assert.That(IsNear(v, 0.02f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.03f);
            Assert.That(IsNear(v, 0.03f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.14f);
            Assert.That(IsNear(v, 0.14f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.25f);
            Assert.That(IsNear(v, 0.25f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.28f);
            Assert.That(IsNear(v, 0.28f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.3899999f);
            Assert.That(IsNear(v, 0.3899999f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.5299998f);
            Assert.That(IsNear(v, 0.5299998f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.6399997f);
            Assert.That(IsNear(v, 0.6399997f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.7799996f);
            Assert.That(IsNear(v, 0.7799996f, 0.000001f));
            
            v = FunctionHelper.EaseLinear(0.8899994f);
            Assert.That(IsNear(v, 0.8899994f, 0.000001f));

            v = FunctionHelper.EaseLinear(1f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
        }
        
        [Test]
        public void Test_EaseInQuad() {
            
            float v = FunctionHelper.EaseInQuad(0);
            Assert.That(IsNear(v, 0f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.03f);
            Assert.That(IsNear(v, 0.0009f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.14f);
            Assert.That(IsNear(v, 0.0196f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.25f);
            Assert.That(IsNear(v, 0.06250001f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.28f);
            Assert.That(IsNear(v, 0.0784f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.3899999f);
            Assert.That(IsNear(v, 0.1520999f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.5299998f);
            Assert.That(IsNear(v, 0.2808998f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.6399997f);
            Assert.That(IsNear(v, 0.4095996f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.7799996f);
            Assert.That(IsNear(v, 0.6083993f, 0.000001f));
            
            v = FunctionHelper.EaseInQuad(0.8899994f);
            Assert.That(IsNear(v, 0.792099f, 0.000001f));

            v = FunctionHelper.EaseInQuad(1f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
        }
        
        [Test]
        public void Test_EaseOutQuad()
        {

            float v = FunctionHelper.EaseOutQuad(0);
            Assert.That(IsNear(v, 0f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.03f);
            Assert.That(IsNear(v, 0.0591f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.14f);
            Assert.That(IsNear(v, 0.2604f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.25f);
            Assert.That(IsNear(v, 0.4375f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.28f);
            Assert.That(IsNear(v, 0.4816f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.3899999f);
            Assert.That(IsNear(v, 0.6278999f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.5299998f);
            Assert.That(IsNear(v, 0.7790998f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.6399997f);
            Assert.That(IsNear(v, 0.8703998f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.7799996f);
            Assert.That(IsNear(v, 0.9515998f, 0.000001f));
            
            v = FunctionHelper.EaseOutQuad(0.8899994f);
            Assert.That(IsNear(v, 0.9878999f, 0.000001f));

            v = FunctionHelper.EaseOutQuad(1f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
        }
        
        [Test]
        public void Test_EaseInOutQuad()
        {

            float v = FunctionHelper.EaseInOutQuad(0);
            Assert.That(IsNear(v, 0f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.03f);
            Assert.That(IsNear(v, 0.0018f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.14f);
            Assert.That(IsNear(v, 0.03919999f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.25f);
            Assert.That(IsNear(v, 0.125f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.28f);
            Assert.That(IsNear(v, 0.1568f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.3899999f);
            Assert.That(IsNear(v, 0.3041998f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.5299998f);
            Assert.That(IsNear(v, 0.5581996f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.6399997f);
            Assert.That(IsNear(v, 0.7407995f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.7799996f);
            Assert.That(IsNear(v, 0.9031996f, 0.000001f));
            
            v = FunctionHelper.EaseInOutQuad(0.8899994f);
            Assert.That(IsNear(v, 0.9757997f, 0.000001f));

            v = FunctionHelper.EaseInOutQuad(1f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
        }
        
        [Test]
        public void Test_EaseInCubic()
        {

            float v = FunctionHelper.EaseInCubic(0);
            Assert.That(IsNear(v, 0f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.03f);
            Assert.That(IsNear(v, 2.7e-05f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.14f);
            Assert.That(IsNear(v, 0.002743999f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.25f);
            Assert.That(IsNear(v, 0.01562501f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.28f);
            Assert.That(IsNear(v, 0.021952f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.3899999f);
            Assert.That(IsNear(v, 0.05931895f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.5299998f);
            Assert.That(IsNear(v, 0.1488768f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.6399997f);
            Assert.That(IsNear(v, 0.2621436f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.7799996f);
            Assert.That(IsNear(v, 0.4745512f, 0.000001f));
            
            v = FunctionHelper.EaseInCubic(0.8899994f);
            Assert.That(IsNear(v, 0.7049677f, 0.000001f));

            v = FunctionHelper.EaseInCubic(1f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
        }
        
        [Test]
        public void Test_EaseOutCubic()
        {

            float v = FunctionHelper.EaseOutCubic(0);
            Assert.That(IsNear(v, 0f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.03f);
            Assert.That(IsNear(v, 0.08732692f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.14f);
            Assert.That(IsNear(v, 0.363944f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.25f);
            Assert.That(IsNear(v, 0.578125f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.28f);
            Assert.That(IsNear(v, 0.626752f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.3899999f);
            Assert.That(IsNear(v, 0.7730188f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.5299998f);
            Assert.That(IsNear(v, 0.8961769f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.6399997f);
            Assert.That(IsNear(v, 0.9533439f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.7799996f);
            Assert.That(IsNear(v, 0.9893519f, 0.000001f));
            
            v = FunctionHelper.EaseOutCubic(0.8899994f);
            Assert.That(IsNear(v, 0.998669f, 0.000001f));

            v = FunctionHelper.EaseOutCubic(1f);
            Assert.That(IsNear(v, 1f, 0.000001f));
            
        }
        
        [Test]
        public void Test_EaseInOutCubic()
        {
            Func<float, float> func = FunctionHelper.EaseInOutCubic;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f, 0.000108f);
            
            Test_RunFunction(func,0.14f, 0.010976f);
            
            Test_RunFunction(func,0.25f, 0.0625002f);
            
            Test_RunFunction(func,0.28f, 0.087808f);
            
            Test_RunFunction(func,0.3899999f, 0.2372758f);
            
            Test_RunFunction(func,0.5299998f, 0.5847074f);
            
            Test_RunFunction(func,0.6399997f, 0.8133755f);
            
            Test_RunFunction(func,0.7799996f, 0.9574077f);
            
            Test_RunFunction(func,0.8899994f, 0.9946759f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInQuart()
        {

            Func<float, float> func = FunctionHelper.EaseInQuart;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f, 8.099999E-07f);
            
            Test_RunFunction(func,0.14f, 0.0003841599f);
            
            Test_RunFunction(func,0.25f, 0.003906252f);
            
            Test_RunFunction(func,0.28f, 0.00614656f);
            
            Test_RunFunction(func,0.3899999f, 0.02313439f);
            
            Test_RunFunction(func,0.5299998f, 0.07890469f);
            
            Test_RunFunction(func,0.6399997f, 0.1677718f);
            
            Test_RunFunction(func,0.7799996f, 0.3701497f);
            
            Test_RunFunction(func,0.8899994f, 0.6274208f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutQuart()
        {

            Func<float, float> func = FunctionHelper.EaseOutQuart;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f,0.1147071f);

            Test_RunFunction(func,0.14f,0.4529918f);

            Test_RunFunction(func,0.25f,0.6835938f);

            Test_RunFunction(func,0.28f,0.7312614f);

            Test_RunFunction(func,0.3899999f,0.8615415f);

            Test_RunFunction(func,0.5299998f,0.9512031f);

            Test_RunFunction(func,0.6399997f,0.9832038f);

            Test_RunFunction(func,0.7799996f,0.9976574f);

            Test_RunFunction(func,0.8899994f,0.9998536f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInOutQuart()
        {

            Func<float, float> func = FunctionHelper.EaseInOutQuart;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f,6.479999E-06f);

            Test_RunFunction(func,0.14f,0.003073279f);

            Test_RunFunction(func,0.25f,0.03125001f);

            Test_RunFunction(func,0.28f,0.04917248f);

            Test_RunFunction(func,0.3899999f,0.1850751f);

            Test_RunFunction(func,0.5299998f,0.6096248f);

            Test_RunFunction(func,0.6399997f,0.8656303f);

            Test_RunFunction(func,0.7799996f,0.9812593f);

            Test_RunFunction(func,0.8899994f,0.9988287f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInQuint()
        {

            Func<float, float> func = FunctionHelper.EaseInQuint;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f,2.43E-08f);

            Test_RunFunction(func,0.14f,5.378237E-05f);

            Test_RunFunction(func,0.25f,0.0009765631f);

            Test_RunFunction(func,0.28f,0.001721037f);

            Test_RunFunction(func,0.3899999f,0.009022408f);

            Test_RunFunction(func,0.5299998f,0.04181947f);

            Test_RunFunction(func,0.6399997f,0.1073739f);

            Test_RunFunction(func,0.7799996f,0.2887166f);

            Test_RunFunction(func,0.8899994f,0.5584042f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutQuint()
        {

            Func<float, float> func = FunctionHelper.EaseOutQuint;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f,0.1412659f);

            Test_RunFunction(func,0.14f,0.529573f);

            Test_RunFunction(func,0.25f,0.7626953f);

            Test_RunFunction(func,0.28f,0.8065082f);

            Test_RunFunction(func,0.3899999f,0.9155403f);

            Test_RunFunction(func,0.5299998f,0.9770654f);

            Test_RunFunction(func,0.6399997f,0.9939533f);

            Test_RunFunction(func,0.7799996f,0.9994847f);

            Test_RunFunction(func,0.8899994f,0.9999839f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInOutQuint()
        {

            Func<float, float> func = FunctionHelper.EaseInOutQuint;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f,3.887999E-07f);

            Test_RunFunction(func,0.14f,0.000860518f);

            Test_RunFunction(func,0.25f,0.01562501f);

            Test_RunFunction(func,0.28f,0.02753659f);

            Test_RunFunction(func,0.3899999f,0.1443585f);

            Test_RunFunction(func,0.5299998f,0.6330472f);

            Test_RunFunction(func,0.6399997f,0.9032537f);

            Test_RunFunction(func,0.7799996f,0.9917541f);

            Test_RunFunction(func,0.8899994f,0.9997423f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInSine()
        {

            Func<float, float> func = FunctionHelper.EaseInSine;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.14f,0.02408326f);

            Test_RunFunction(func,0.25f,0.0761205f);

            Test_RunFunction(func,0.28f,0.09517294f);

            Test_RunFunction(func,0.3899999f,0.1818502f);

            Test_RunFunction(func,0.5299998f,0.3269873f);

            Test_RunFunction(func,0.6399997f,0.4641728f);

            Test_RunFunction(func,0.7799996f,0.6612614f);

            Test_RunFunction(func,0.8899994f,0.82807f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutSine()
        {

            Func<float, float> func = FunctionHelper.EaseOutSine;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f,0.04710645f);

            Test_RunFunction(func,0.14f,0.2181432f);

            Test_RunFunction(func,0.25f,0.3826835f);

            Test_RunFunction(func,0.28f,0.4257793f);

            Test_RunFunction(func,0.3899999f,0.5750051f);

            Test_RunFunction(func,0.5299998f,0.7396309f);

            Test_RunFunction(func,0.6399997f,0.8443277f);

            Test_RunFunction(func,0.7799996f,0.9408805f);

            Test_RunFunction(func,0.8899994f,0.9851092f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInOutSine()
        {

            Func<float, float> func = FunctionHelper.EaseInOutSine;

            Test_RunFunction(func,0, 0);
            
            Test_RunFunction(func,0.03f,0.002219021f);

            Test_RunFunction(func,0.14f,0.04758647f);

            Test_RunFunction(func,0.25f,0.1464466f);

            Test_RunFunction(func,0.28f,0.181288f);

            Test_RunFunction(func,0.3899999f,0.3306309f);

            Test_RunFunction(func,0.5299998f,0.5470538f);

            Test_RunFunction(func,0.6399997f,0.7128893f);

            Test_RunFunction(func,0.7799996f,0.8852562f);

            Test_RunFunction(func,0.8899994f,0.9704401f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInExpo()
        {
            
            Func<float, float> func = FunctionHelper.EaseInExpo;
            
            Test_RunFunction(func,0, 0.0009765625f);
            
            Test_RunFunction(func,0.03f,0.00120229f);

            Test_RunFunction(func,0.14f,0.002577163f);

            Test_RunFunction(func,0.25f,0.005524273f);

            Test_RunFunction(func,0.28f,0.006801177f);

            Test_RunFunction(func,0.3899999f,0.01457863f);

            Test_RunFunction(func,0.5299998f,0.0384732f);

            Test_RunFunction(func,0.6399997f,0.08246906f);

            Test_RunFunction(func,0.7799996f,0.2176369f);

            Test_RunFunction(func,0.8899994f,0.4665147f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutExpo()
        {
            
            Func<float, float> func = FunctionHelper.EaseOutExpo;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.1877476f);

            Test_RunFunction(func,0.14f,0.6210709f);

            Test_RunFunction(func,0.25f,0.8232234f);

            Test_RunFunction(func,0.28f,0.8564127f);

            Test_RunFunction(func,0.3899999f,0.9330141f);

            Test_RunFunction(func,0.5299998f,0.9746171f);

            Test_RunFunction(func,0.6399997f,0.9881585f);

            Test_RunFunction(func,0.7799996f,0.9955129f);

            Test_RunFunction(func,0.8899994f,0.9979067f);
            
            Test_RunFunction(func,1f, 0.9990234f);
            
            
        }
        
        [Test]
        public void Test_EaseInOutExpo()
        {
            
            Func<float, float> func = FunctionHelper.EaseInOutExpo;
            
            Test_RunFunction(func,0, 0.0004882813f);
            
            Test_RunFunction(func,0.03f,0.0007400961f);

            Test_RunFunction(func,0.14f,0.003400587f);

            Test_RunFunction(func,0.25f,0.01562501f);

            Test_RunFunction(func,0.28f,0.02368307f);

            Test_RunFunction(func,0.3899999f,0.1088187f);

            Test_RunFunction(func,0.5299998f,0.6701221f);

            Test_RunFunction(func,0.6399997f,0.928206f);

            Test_RunFunction(func,0.7799996f,0.9896913f);

            Test_RunFunction(func,0.8899994f,0.9977564f);
            
            Test_RunFunction(func,1f, 0.9995117f);
            
        }
        
        [Test]
        public void Test_EaseInCirc()
        {
            
            Func<float, float> func = FunctionHelper.EaseInCirc;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.0004500747f);

            Test_RunFunction(func,0.14f,0.009848475f);

            Test_RunFunction(func,0.25f,0.03175414f);

            Test_RunFunction(func,0.28f,0.04000002f);

            Test_RunFunction(func,0.3899999f,0.07918507f);

            Test_RunFunction(func,0.5299998f,0.1520022f);

            Test_RunFunction(func,0.6399997f,0.2316248f);

            Test_RunFunction(func,0.7799996f,0.37422f);

            Test_RunFunction(func,0.8899994f,0.5440384f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutCirc()
        {
            
            Func<float, float> func = FunctionHelper.EaseOutCirc;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.2431048f);

            Test_RunFunction(func,0.14f,0.510294f);

            Test_RunFunction(func,0.25f,0.6614378f);

            Test_RunFunction(func,0.28f,0.693974f);

            Test_RunFunction(func,0.3899999f,0.7924013f);

            Test_RunFunction(func,0.5299998f,0.8826663f);

            Test_RunFunction(func,0.6399997f,0.9329522f);

            Test_RunFunction(func,0.7799996f,0.9754997f);

            Test_RunFunction(func,0.8899994f,0.9939315f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInOutCirc()
        {
            
            Func<float, float> func = FunctionHelper.EaseInOutCirc;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.000900805f);

            Test_RunFunction(func,0.14f,0.01999998f);

            Test_RunFunction(func,0.25f,0.06698731f);

            Test_RunFunction(func,0.28f,0.08575371f);

            Test_RunFunction(func,0.3899999f,0.1871101f);

            Test_RunFunction(func,0.5299998f,0.6705866f);

            Test_RunFunction(func,0.6399997f,0.8469867f);

            Test_RunFunction(func,0.7799996f,0.9489987f);

            Test_RunFunction(func,0.8899994f,0.9877498f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInElastic()
        {
            
            Func<float, float> func = FunctionHelper.EaseInElastic;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.0001256734f);

            Test_RunFunction(func,0.14f,0.001724458f);

            Test_RunFunction(func,0.25f,-0.005524272f);

            Test_RunFunction(func,0.28f,-0.00550227f);

            Test_RunFunction(func,0.3899999f,0.01426004f);

            Test_RunFunction(func,0.5299998f,-0.03514696f);

            Test_RunFunction(func,0.6399997f,0.02548383f);

            Test_RunFunction(func,0.7799996f,-0.02274723f);

            Test_RunFunction(func,0.8899994f,-0.3121632f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutElastic()
        {
            
            Func<float, float> func = FunctionHelper.EaseOutElastic;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.342874f);

            Test_RunFunction(func,0.14f,1.370649f);

            Test_RunFunction(func,0.25f,0.9116116f);

            Test_RunFunction(func,0.28f,0.8688264f);

            Test_RunFunction(func,0.3899999f,1.0207f);

            Test_RunFunction(func,0.5299998f,0.9973469f);

            Test_RunFunction(func,0.6399997f,0.9920764f);

            Test_RunFunction(func,0.7799996f,1.00363f);

            Test_RunFunction(func,0.8899994f,0.9979525f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInOutElastic()
        {
            
            Func<float, float> func = FunctionHelper.EaseInOutElastic;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.0006276372f);

            Test_RunFunction(func,0.14f,-0.002751133f);

            Test_RunFunction(func,0.25f,0.01196946f);

            Test_RunFunction(func,0.28f,0.02345259f);

            Test_RunFunction(func,0.3899999f,-0.1085536f);

            Test_RunFunction(func,0.5299998f,0.7792671f);

            Test_RunFunction(func,0.6399997f,1.051645f);

            Test_RunFunction(func,0.7799996f,0.9996401f);

            Test_RunFunction(func,0.8899994f,1.000235f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInBack()
        {
            
            Func<float, float> func = FunctionHelper.EaseInBack;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,-0.001458479f);

            Test_RunFunction(func,0.14f,-0.02593783f);

            Test_RunFunction(func,0.25f,-0.06413657f);

            Test_RunFunction(func,0.28f,-0.07409879f);

            Test_RunFunction(func,0.3899999f,-0.09855529f);

            Test_RunFunction(func,0.5299998f,-0.0757708f);

            Test_RunFunction(func,0.6399997f,0.01123546f);

            Test_RunFunction(func,0.7799996f,0.2467979f);

            Test_RunFunction(func,0.8899994f,0.5567068f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutBack()
        {
            
            Func<float, float> func = FunctionHelper.EaseOutBack;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.1353574f);

            Test_RunFunction(func,0.14f,0.5401323f);

            Test_RunFunction(func,0.25f,0.8174097f);

            Test_RunFunction(func,0.28f,0.8737397f);

            Test_RunFunction(func,0.3899999f,1.019951f);

            Test_RunFunction(func,0.5299998f,1.095393f);

            Test_RunFunction(func,0.6399997f,1.09448f);

            Test_RunFunction(func,0.7799996f,1.05359f);

            Test_RunFunction(func,0.8899994f,1.016994f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInOutBack()
        {
            
            Func<float, float> func = FunctionHelper.EaseInOutBack;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,-0.004282587f);

            Test_RunFunction(func,0.14f,-0.06226271f);

            Test_RunFunction(func,0.25f,-0.09968184f);

            Test_RunFunction(func,0.28f,-0.09121999f);

            Test_RunFunction(func,0.3899999f,0.06361402f);

            Test_RunFunction(func,0.5299998f,0.6534929f);

            Test_RunFunction(func,0.6399997f,1.001704f);

            Test_RunFunction(func,0.7799996f,1.098073f);

            Test_RunFunction(func,0.8899994f,1.043658f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInBounce()
        {
            
            Func<float, float> func = FunctionHelper.EaseInBounce;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.01381874f);

            Test_RunFunction(func,0.14f,0.04927498f);

            Test_RunFunction(func,0.25f,0.02734375f);

            Test_RunFunction(func,0.28f,0.01959997f);

            Test_RunFunction(func,0.3899999f,0.2184936f);

            Test_RunFunction(func,0.5299998f,0.206944f);

            Test_RunFunction(func,0.6399997f,0.0198983f);

            Test_RunFunction(func,0.7799996f,0.6339735f);

            Test_RunFunction(func,0.8899994f,0.9084928f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseOutBounce()
        {
            
            Func<float, float> func = FunctionHelper.EaseOutBounce;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.00680625f);

            Test_RunFunction(func,0.14f,0.148225f);

            Test_RunFunction(func,0.25f,0.4726564f);

            Test_RunFunction(func,0.28f,0.5929f);

            Test_RunFunction(func,0.3899999f,0.9327565f);

            Test_RunFunction(func,0.5299998f,0.7518063f);

            Test_RunFunction(func,0.6399997f,0.8175995f);

            Test_RunFunction(func,0.7799996f,0.9485252f);

            Test_RunFunction(func,0.8899994f,0.9765056f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        [Test]
        public void Test_EaseInOutBounce()
        {
            
            Func<float, float> func = FunctionHelper.EaseInOutBounce;
            
            Test_RunFunction(func,0, 0f);
            
            Test_RunFunction(func,0.03f,0.007012516f);

            Test_RunFunction(func,0.14f,0.009799987f);

            Test_RunFunction(func,0.25f,0.1171875f);

            Test_RunFunction(func,0.28f,0.08295f);

            Test_RunFunction(func,0.3899999f,0.3169872f);

            Test_RunFunction(func,0.5299998f,0.5136123f);

            Test_RunFunction(func,0.6399997f,0.7964487f);

            Test_RunFunction(func,0.7799996f,0.8757999f);

            Test_RunFunction(func,0.8899994f,0.9742628f);
            
            Test_RunFunction(func,1f, 1f);
            
        }
        
        bool IsNear(float a1, float a2, float allowDiff) {
            float res = Mathf.Abs(Mathf.Abs(a1) - Mathf.Abs(a2));
            return res <= allowDiff;
        }
        
    }

}