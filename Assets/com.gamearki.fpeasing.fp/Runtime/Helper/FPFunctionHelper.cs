using System;
using FixMath.NET;

namespace GameArki.FPEasing.FP {

    // Input: Time Percent
    // Output: Value Percent
    public static class FPFunctionHelper {

        static FP64 spec_170158_en5 = (FP64)170158 / 100000;
        static FP64 spec_1525_en3 = 1525 * FP64.EN3;
        static FP64 spec_275_en2 = 275 * FP64.EN2;
        static FP64 spec_75625_en4 = 75625 * FP64.EN4;
        static FP64 spec_9375_en4 = 9375 * FP64.EN4;
        static FP64 spec_2625_en3 = 2625 * FP64.EN3;
        static FP64 spec_984375_en6 = (FP64)984375 / 1000000;

        public static FP64 EaseImmediate(FP64 timePercent) {
            return FP64.One;
        }

        public static FP64 EaseLinear(FP64 t) {
            return t;
        }

        public static FP64 EaseInQuad(FP64 t) {
            return t * t;
        }

        public static FP64 EaseOutQuad(FP64 t) {
            return -t * (t - 2);
        }

        public static FP64 EaseInOutQuad(FP64 t) {
            t *= 2;
            if (t < 1) {
                return FP64.Half * t * t;
            }
            t -= 1;
            return -FP64.Half * (t * (t - 2) - 1);
        }

        public static FP64 EaseInCubic(FP64 t) {
            return t * t * t;
        }

        public static FP64 EaseOutCubic(FP64 t) {
            t -= 1;
            return (t * t * t + 1);
        }

        public static FP64 EaseInOutCubic(FP64 t) {
            t *= 2;
            if (t < 1) {
                return FP64.Half * t * t * t;
            }
            t -= 2;
            return FP64.Half * (t * t * t + 2);
        }

        public static FP64 EaseInQuart(FP64 t) {
            return t * t * t * t;
        }

        public static FP64 EaseOutQuart(FP64 t) {
            t -= 1;
            return -(t * t * t * t - 1);
        }

        public static FP64 EaseInOutQuart(FP64 t) {
            t *= 2;
            if (t < 1) {
                return FP64.Half * t * t * t * t;
            }
            t -= 2;
            return -FP64.Half * (t * t * t * t - 2);
        }

        public static FP64 EaseInQuint(FP64 t) {
            return t * t * t * t * t;
        }

        public static FP64 EaseOutQuint(FP64 t) {
            t -= 1;
            return (t * t * t * t * t + 1);
        }

        public static FP64 EaseInOutQuint(FP64 t) {
            t *= 2;
            if (t < 1) {
                return FP64.Half * t * t * t * t * t;
            }
            t -= 2;
            return FP64.Half * (t * t * t * t * t + 2);
        }

        public static FP64 EaseInSine(FP64 t) {
            return -FP64.Cos(t * (FP64.PiOver2)) + 1;
        }

        public static FP64 EaseOutSine(FP64 t) {
            return FP64.Sin(t * (FP64.PiOver2));
        }

        public static FP64 EaseInOutSine(FP64 t) {
            return -FP64.Half * (FP64.Cos(FP64.Pi * t) - 1);
        }

        public static FP64 EaseInExpo(FP64 t) {
            return FP64.Pow(2, 10 * (t - 1));
        }

        public static FP64 EaseOutExpo(FP64 t) {
            return -FP64.Pow(2, -10 * t) + 1;
        }

        public static FP64 EaseInOutExpo(FP64 t) {
            t *= 2;
            if (t < 1) {
                return FP64.Half * FP64.Pow(2, 10 * (t - 1));
            }
            t -= 1;
            return FP64.Half * (-FP64.Pow(2, -10 * t) + 2);
        }

        public static FP64 EaseInCirc(FP64 t) {
            return -(FP64.Sqrt(1 - t * t) - 1);
        }

        public static FP64 EaseOutCirc(FP64 t) {
            t -= 1;
            return FP64.Sqrt(1 - t * t);
        }

        public static FP64 EaseInOutCirc(FP64 t) {
            t *= 2;
            if (t < 1) {
                return -FP64.Half * (FP64.Sqrt(1 - t * t) - 1);
            }
            t -= 2;
            return FP64.Half * (FP64.Sqrt(1 - t * t) + 1);
        }

        public static FP64 EaseInElastic(FP64 t) {
            FP64 s = spec_170158_en5;
            FP64 p = 0;
            FP64 a = 1;
            if (t == 0) {
                return 0;
            }
            if (t == 1) {
                return 1;
            }
            if (p == 0) {
                p = 3 * FP64.EN1;
            }
            if (a < 1) {
                a = 1;
                s = p / 4;
            } else {
                s = p / (2 * FP64.Pi) * (FP64.PiOver2 - FP64.Acos(1 / a)); // sin(x) = y; x = asin(y)
            }
            t -= 1;
            return -(a * FP64.Pow(2, 10 * t) * FP64.Sin((t - s) * (2 * FP64.Pi) / p));
        }

        public static FP64 EaseOutElastic(FP64 t) {
            FP64 s = spec_170158_en5;
            FP64 p = 0;
            FP64 a = 1;
            if (t == 0) {
                return 0;
            }
            if (t == 1) {
                return 1;
            }
            if (p == 0) {
                p = 3 * FP64.EN1;
            }
            if (a < 1) {
                a = 1;
                s = p / 4;
            } else {
                s = p / (2 * FP64.Pi) * (FP64.PiOver2 - FP64.Acos(1 / a));
            }
            return a * FP64.Pow(2, -10 * t) * FP64.Sin((t - s) * (2 * FP64.Pi) / p) + 1;
        }

        public static FP64 EaseInOutElastic(FP64 t) {
            FP64 s = spec_170158_en5;
            FP64 p = 0;
            FP64 a = 1;
            if (t == 0) {
                return 0;
            }
            t *= 2;
            if (t == 2) {
                return 1;
            }
            if (p == 0) {
                p = 45 * FP64.EN2;
            }
            if (a < 1) {
                a = 1;
                s = p / 4;
            } else {
                s = p / (2 * FP64.Pi) * (FP64.PiOver2 - FP64.Acos(1 / a));
            }
            if (t < 1) {
                t -= 1;
                return -FP64.Half * (a * FP64.Pow(2, 10 * t) * FP64.Sin((t - s) * (2 * FP64.Pi) / p));
            }
            t -= 1;
            return a * FP64.Pow(2, -10 * t) * FP64.Sin((t - s) * (2 * FP64.Pi) / p) * FP64.Half + 1;
        }

        public static FP64 EaseInBack(FP64 t) {
            FP64 s = spec_170158_en5;
            return t * t * ((s + 1) * t - s);
        }

        public static FP64 EaseOutBack(FP64 t) {
            FP64 s = spec_170158_en5;
            t -= 1;
            return (t * t * ((s + 1) * t + s) + 1);
        }

        public static FP64 EaseInOutBack(FP64 t) {
            FP64 s = spec_170158_en5;
            t *= 2;
            if (t < 1) {
                s *= spec_1525_en3;
                return FP64.Half * (t * t * ((s + 1) * t - s));
            }
            t -= 2;
            s *= spec_1525_en3;
            return FP64.Half * (t * t * ((s + 1) * t + s) + 2);
        }

        public static FP64 EaseInBounce(FP64 t) {
            return 1 - EaseOutBounce(1 - t);
        }

        public static FP64 EaseOutBounce(FP64 t) {
            if (t < (1 / spec_275_en2)) {
                return (spec_75625_en4 * t * t);
            } else if (t < (2 / spec_275_en2)) {
                t -= ((1 + FP64.Half) / spec_275_en2);
                return (spec_75625_en4 * t * t + 1 - FP64.C0p25);
            } else if (t < ((2 + FP64.Half) / spec_275_en2)) {
                t -= ((2 + FP64.C0p25) / spec_275_en2);
                return (spec_75625_en4 * t * t + spec_9375_en4);
            } else {
                t -= (spec_2625_en3 / spec_275_en2);
                return (spec_75625_en4 * t * t + spec_984375_en6);
            }
        }

        public static FP64 EaseInOutBounce(FP64 t) {
            if (t < FP64.Half) {
                return EaseInBounce(t * 2) * FP64.Half;
            }
            return EaseOutBounce(t * 2 - 1) * FP64.Half + FP64.Half;
        }

    }

}