using System;

namespace GameArki.FPEasing {

    // Input: Time Percent
    // Output: Value Percent
    public static class FunctionHelper {

        public static float EaseImmediate(float timePercent) {
            return 1;
        }

        public static float EaseLinear(float t) {
            return t;
        }

        public static float EaseInQuad(float t) {
            return t * t;
        }

        public static float EaseOutQuad(float t) {
            return -t * (t - 2);
        }

        public static float EaseInOutQuad(float t) {
            t *= 2;
            if (t < 1) {
                return 0.5f * t * t;
            }
            t -= 1;
            return -0.5f * (t * (t - 2) - 1);
        }

        public static float EaseInCubic(float t) {
            return t * t * t;
        }

        public static float EaseOutCubic(float t) {
            t -= 1;
            return (t * t * t + 1);
        }

        public static float EaseInOutCubic(float t) {
            t *= 2;
            if (t < 1) {
                return 0.5f * t * t * t;
            }
            t -= 2;
            return 0.5f * (t * t * t + 2);
        }

        public static float EaseInQuart(float t) {
            return t * t * t * t;
        }

        public static float EaseOutQuart(float t) {
            t -= 1;
            return -(t * t * t * t - 1);
        }

        public static float EaseInOutQuart(float t) {
            t *= 2;
            if (t < 1) {
                return 0.5f * t * t * t * t;
            }
            t -= 2;
            return -0.5f * (t * t * t * t - 2);
        }

        public static float EaseInQuint(float t) {
            return t * t * t * t * t;
        }

        public static float EaseOutQuint(float t) {
            t -= 1;
            return (t * t * t * t * t + 1);
        }

        public static float EaseInOutQuint(float t) {
            t *= 2;
            if (t < 1) {
                return 0.5f * t * t * t * t * t;
            }
            t -= 2;
            return 0.5f * (t * t * t * t * t + 2);
        }

        public static float EaseInSine(float t) {
            return -MathF.Cos(t * (MathF.PI / 2)) + 1;
        }

        public static float EaseOutSine(float t) {
            return MathF.Sin(t * (MathF.PI / 2));
        }

        public static float EaseInOutSine(float t) {
            return -0.5f * (MathF.Cos(MathF.PI * t) - 1);
        }

        public static float EaseInExpo(float t) {
            return MathF.Pow(2, 10 * (t - 1));
        }

        public static float EaseOutExpo(float t) {
            return -MathF.Pow(2, -10 * t) + 1;
        }

        public static float EaseInOutExpo(float t) {
            t *= 2;
            if (t < 1) {
                return 0.5f * MathF.Pow(2, 10 * (t - 1));
            }
            t -= 1;
            return 0.5f * (-MathF.Pow(2, -10 * t) + 2);
        }

        public static float EaseInCirc(float t) {
            return -(MathF.Sqrt(1 - t * t) - 1);
        }

        public static float EaseOutCirc(float t) {
            t -= 1;
            return MathF.Sqrt(1 - t * t);
        }

        public static float EaseInOutCirc(float t) {
            t *= 2;
            if (t < 1) {
                return -0.5f * (MathF.Sqrt(1 - t * t) - 1);
            }
            t -= 2;
            return 0.5f * (MathF.Sqrt(1 - t * t) + 1);
        }

        public static float EaseInElastic(float t) {
            float s = 1.70158f;
            float p = 0;
            float a = 1;
            if (t == 0) {
                return 0;
            }
            if (t == 1) {
                return 1;
            }
            if (p == 0) {
                p = 0.3f;
            }
            if (a < 1) {
                a = 1;
                s = p / 4;
            } else {
                s = p / (2 * MathF.PI) * MathF.Asin(1 / a);
            }
            t -= 1;
            return -(a * MathF.Pow(2, 10 * t) * MathF.Sin((t - s) * (2 * MathF.PI) / p));
        }

        public static float EaseOutElastic(float t) {
            float s = 1.70158f;
            float p = 0;
            float a = 1;
            if (t == 0) {
                return 0;
            }
            if (t == 1) {
                return 1;
            }
            if (p == 0) {
                p = 0.3f;
            }
            if (a < 1) {
                a = 1;
                s = p / 4;
            } else {
                s = p / (2 * MathF.PI) * MathF.Asin(1 / a);
            }
            return a * MathF.Pow(2, -10 * t) * MathF.Sin((t - s) * (2 * MathF.PI) / p) + 1;
        }

        public static float EaseInOutElastic(float t) {
            float s = 1.70158f;
            float p = 0;
            float a = 1;
            if (t == 0) {
                return 0;
            }
            t *= 2;
            if (t == 2) {
                return 1;
            }
            if (p == 0) {
                p = 0.3f * 1.5f;
            }
            if (a < 1) {
                a = 1;
                s = p / 4;
            } else {
                s = p / (2 * MathF.PI) * MathF.Asin(1 / a);
            }
            if (t < 1) {
                t -= 1;
                return -0.5f * (a * MathF.Pow(2, 10 * t) * MathF.Sin((t - s) * (2 * MathF.PI) / p));
            }
            t -= 1;
            return a * MathF.Pow(2, -10 * t) * MathF.Sin((t - s) * (2 * MathF.PI) / p) * 0.5f + 1;
        }

        public static float EaseInBack(float t) {
            float s = 1.70158f;
            return t * t * ((s + 1) * t - s);
        }

        public static float EaseOutBack(float t) {
            float s = 1.70158f;
            t -= 1;
            return (t * t * ((s + 1) * t + s) + 1);
        }

        public static float EaseInOutBack(float t) {
            float s = 1.70158f;
            t *= 2;
            if (t < 1) {
                s *= 1.525f;
                return 0.5f * (t * t * ((s + 1) * t - s));
            }
            t -= 2;
            s *= 1.525f;
            return 0.5f * (t * t * ((s + 1) * t + s) + 2);
        }

        public static float EaseInBounce(float t) {
            return 1 - EaseOutBounce(1 - t);
        }

        public static float EaseOutBounce(float t) {
            if (t < (1 / 2.75f)) {
                return (7.5625f * t * t);
            } else if (t < (2 / 2.75f)) {
                t -= (1.5f / 2.75f);
                return (7.5625f * t * t + 0.75f);
            } else if (t < (2.5 / 2.75)) {
                t -= (2.25f / 2.75f);
                return (7.5625f * t * t + 0.9375f);
            } else {
                t -= (2.625f / 2.75f);
                return (7.5625f * t * t + 0.984375f);
            }
        }

        public static float EaseInOutBounce(float t) {
            if (t < 0.5f) {
                return EaseInBounce(t * 2) * 0.5f;
            }
            return EaseOutBounce(t * 2 - 1) * 0.5f + 0.5f;
        }

        // ==== Hill ====
        public static float EaseHill_EN1(float t) => EaseHill_Base(t, 0.1f);
        public static float EaseHill_EN2(float t) => EaseHill_Base(t, 0.2f);
        public static float EaseHill_EN3(float t) => EaseHill_Base(t, 0.3f);
        public static float EaseHill_EN4(float t) => EaseHill_Base(t, 0.4f);
        public static float EaseHill_EN5(float t) => EaseHill_Base(t, 0.5f);
        public static float EaseHill_EN6(float t) => EaseHill_Base(t, 0.6f);
        public static float EaseHill_EN7(float t) => EaseHill_Base(t, 0.7f);
        public static float EaseHill_EN8(float t) => EaseHill_Base(t, 0.8f);
        public static float EaseHill_EN9(float t) => EaseHill_Base(t, 0.9f);
        public static float EaseHill_EN10(float t) => EaseHill_Base(t, 1f);

        static float EaseHill_Base(float t, float multiple) {
            float v;
            if (t < 0.5f) {
                v = EaseOutCirc(t * 2);
            } else {
                v = EaseOutCirc(2 - t * 2);
            }
            v *= multiple;
            return v;
        }

    }

}