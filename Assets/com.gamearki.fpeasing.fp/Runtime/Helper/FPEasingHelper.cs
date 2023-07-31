using FixMath.NET;

namespace GameArki.FPEasing.FP {

    // Attentions:
    // 1. duration must be greater than 0
    // 2. duration must greater than passed time
    public static class FPEasingHelper {

        public static FP64 Ease1D(EasingType type, FP64 passTime, FP64 duration, FP64 startValue, FP64 endValue) {
            FP64 timePercent = passTime / duration;
            if (timePercent > FP64.One) {
                timePercent = FP64.One;
            }
            FP64 valuePercent = GetValuePercent(type, timePercent);
            return startValue + (endValue - startValue) * valuePercent;
        }

        public static FPVector2 Ease2D(EasingType type, FP64 passTime, FP64 duration, FPVector2 startValue, FPVector2 endValue) {
            FP64 timePercent = passTime / duration;
            if (timePercent > FP64.One) {
                timePercent = FP64.One;
            }
            FP64 valuePercent = GetValuePercent(type, timePercent);
            return startValue + (endValue - startValue) * valuePercent;
        }

        public static FPVector3 Ease3D(EasingType type, FP64 passTime, FP64 duration, FPVector3 startValue, FPVector3 endValue) {
            FP64 timePercent = passTime / duration;
            if (timePercent > FP64.One) {
                timePercent = FP64.One;
            }
            FP64 valuePercent = GetValuePercent(type, timePercent);
            return startValue + (endValue - startValue) * valuePercent;
        }

        static FP64 GetValuePercent(EasingType type, FP64 timePercent) {
            FP64 valuePercent;
            switch (type) {
                case EasingType.Immediate: valuePercent = FPFunctionHelper.EaseImmediate(timePercent); break;
                case EasingType.Linear: valuePercent = FPFunctionHelper.EaseLinear(timePercent); break;
                case EasingType.InQuad: valuePercent = FPFunctionHelper.EaseInQuad(timePercent); break;
                case EasingType.OutQuad: valuePercent = FPFunctionHelper.EaseOutQuad(timePercent); break;
                case EasingType.InOutQuad: valuePercent = FPFunctionHelper.EaseInOutQuad(timePercent); break;
                case EasingType.InCubic: valuePercent = FPFunctionHelper.EaseInCubic(timePercent); break;
                case EasingType.OutCubic: valuePercent = FPFunctionHelper.EaseOutCubic(timePercent); break;
                case EasingType.InOutCubic: valuePercent = FPFunctionHelper.EaseInOutCubic(timePercent); break;
                case EasingType.InQuart: valuePercent = FPFunctionHelper.EaseInQuart(timePercent); break;
                case EasingType.OutQuart: valuePercent = FPFunctionHelper.EaseOutQuart(timePercent); break;
                case EasingType.InOutQuart: valuePercent = FPFunctionHelper.EaseInOutQuart(timePercent); break;
                case EasingType.InQuint: valuePercent = FPFunctionHelper.EaseInQuint(timePercent); break;
                case EasingType.OutQuint: valuePercent = FPFunctionHelper.EaseOutQuint(timePercent); break;
                case EasingType.InOutQuint: valuePercent = FPFunctionHelper.EaseInOutQuint(timePercent); break;
                case EasingType.InSine: valuePercent = FPFunctionHelper.EaseInSine(timePercent); break;
                case EasingType.OutSine: valuePercent = FPFunctionHelper.EaseOutSine(timePercent); break;
                case EasingType.InOutSine: valuePercent = FPFunctionHelper.EaseInOutSine(timePercent); break;
                case EasingType.InExpo: valuePercent = FPFunctionHelper.EaseInExpo(timePercent); break;
                case EasingType.OutExpo: valuePercent = FPFunctionHelper.EaseOutExpo(timePercent); break;
                case EasingType.InOutExpo: valuePercent = FPFunctionHelper.EaseInOutExpo(timePercent); break;
                case EasingType.InCirc: valuePercent = FPFunctionHelper.EaseInCirc(timePercent); break;
                case EasingType.OutCirc: valuePercent = FPFunctionHelper.EaseOutCirc(timePercent); break;
                case EasingType.InOutCirc: valuePercent = FPFunctionHelper.EaseInOutCirc(timePercent); break;
                case EasingType.InElastic: valuePercent = FPFunctionHelper.EaseInElastic(timePercent); break;
                case EasingType.OutElastic: valuePercent = FPFunctionHelper.EaseOutElastic(timePercent); break;
                case EasingType.InOutElastic: valuePercent = FPFunctionHelper.EaseInOutElastic(timePercent); break;
                case EasingType.InBack: valuePercent = FPFunctionHelper.EaseInBack(timePercent); break;
                case EasingType.OutBack: valuePercent = FPFunctionHelper.EaseOutBack(timePercent); break;
                case EasingType.InOutBack: valuePercent = FPFunctionHelper.EaseInOutBack(timePercent); break;
                case EasingType.InBounce: valuePercent = FPFunctionHelper.EaseInBounce(timePercent); break;
                case EasingType.OutBounce: valuePercent = FPFunctionHelper.EaseOutBounce(timePercent); break;
                case EasingType.InOutBounce: valuePercent = FPFunctionHelper.EaseInOutBounce(timePercent); break;
                default: throw new System.ArgumentException("Invalid EasingType" + type.ToString());
            }
            return valuePercent;
        }

    }

}