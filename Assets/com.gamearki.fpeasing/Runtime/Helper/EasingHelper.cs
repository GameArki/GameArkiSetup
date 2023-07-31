using System;
using System.Linq;
using UnityEngine;

namespace GameArki.FPEasing {

    // Attentions:
    // 1. duration must be greater than 0
    // 2. duration must greater than passed time
    public static class EasingHelper {

        public static float Ease1D(EasingType type, float passTime, float duration, float startValue, float endValue) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            float valuePercent = GetValuePercent(type, timePercent);
            return startValue + (endValue - startValue) * valuePercent;
        }

        public static float Ease1D_Combine(ReadOnlySpan<EasingModel> easingModels, float passTime, float duration, float startValue, float endValue) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            var model = GetEasingType(easingModels, ref timePercent);
            float valuePercent = GetValuePercent(model.type, timePercent);
            if (model.isFlipValue) {
                valuePercent = 1 - valuePercent;
            }
            valuePercent *= 1 - model.maxValuePercentReduce;
            return startValue + (endValue - startValue) * valuePercent;
        }

        public static Vector2 Ease2D(EasingType type, float passTime, float duration, Vector2 startValue, Vector2 endValue) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            float valuePercent = GetValuePercent(type, timePercent);
            return startValue + (endValue - startValue) * valuePercent;
        }

        public static Vector2 Ease2D_Combine(ReadOnlySpan<EasingModel> easingModels, float passTime, float duration, Vector2 startValue, Vector2 endValue) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            var model = GetEasingType(easingModels, ref timePercent);
            float valuePercent = GetValuePercent(model.type, timePercent);
            if (model.isFlipValue) {
                valuePercent = 1 - valuePercent;
            }
            valuePercent *= 1 - model.maxValuePercentReduce;
            return startValue + (endValue - startValue) * valuePercent;
        }

        public static Vector3 Ease3D(EasingType type, float passTime, float duration, Vector3 startValue, Vector3 endValue) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            float valuePercent = GetValuePercent(type, timePercent);
            return startValue + (endValue - startValue) * valuePercent;
        }

        public static Vector3 Ease3D_Combine(ReadOnlySpan<EasingModel> easingModels, float passTime, float duration, Vector3 startValue, Vector3 endValue) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            var model = GetEasingType(easingModels, ref timePercent);
            float valuePercent = GetValuePercent(model.type, timePercent);
            if (model.isFlipValue) {
                valuePercent = 1 - valuePercent;
            }
            valuePercent *= 1 - model.maxValuePercentReduce;
            return startValue + (endValue - startValue) * valuePercent;
        }

        /// <summary>
        /// 3D 球型环绕移动函数.
        /// <para>解释: 以中心点为球心, 球面上的起始点, 球面上的终点, (起始点-中心点).magnitude 为半径, 环绕移动.</para>
        /// <returns>返回当前时间在球面上的位置</returns>
        /// <para name="type">type: 缓动类型</para>
        /// <para>passTime: 经过的时间</para>
        /// <para>duration: 总时间</para>
        /// <para>centerValue: 绕柱的中心点</para>
        /// <para>startValue: 起始点</para>
        /// <para>endValue: 终点</para>
        /// </summary>
        public static Vector3 Ease3DSphereRound(EasingType type, float passTime, float duration, Vector3 center, Vector3 startValue, Vector3 endValue, bool isFallbackClockwise = false) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            float valuePercent = GetValuePercent(type, timePercent);
            Vector3 endDiff = (endValue - center);
            Vector3 startDiff = (startValue - center);
            float radius = (startValue - center).magnitude;
            // 1. 计算起始点和终点的夹角 angle
            // 2. angle = valuePercent * angle
            // 3. 以 center 为中心, radius 为半径, angle 为角度, 计算出 pos
            float angle = Vector3.Angle(startDiff, endDiff);
            if (isFallbackClockwise) {
                angle = -angle;
            }
            Vector3 pos = Quaternion.AngleAxis(angle * valuePercent, Vector3.Cross(startDiff, endDiff)) * startDiff;
            pos = pos.normalized * radius;

            return center + pos;
        }

        public static Quaternion EaseQuaternion(EasingType type, float passTime, float duration, Quaternion startValue, Quaternion endValue) {
            float timePercent = passTime / duration;
            if (timePercent > 1) {
                timePercent = 1;
            }
            float valuePercent = GetValuePercent(type, timePercent);
            return Quaternion.Lerp(startValue, endValue, valuePercent);
        }

        static EasingModel GetEasingType(ReadOnlySpan<EasingModel> easingModels, ref float timePercent) {
            float totalWeight = 0;
            float sum = 0;
            for (int i = 0; i < easingModels.Length; i += 1)  {
                sum += easingModels[i].timeWeight;
            }
            float t = timePercent;
            for (int i = 0; i < easingModels.Length; i += 1) {
                var model = easingModels[i];
                totalWeight += model.timeWeight;

                // 找到 Model
                float weightPercent = totalWeight / sum;
                if (timePercent <= weightPercent) {
                    timePercent = t / model.timeWeight;
                    return model;
                } else {
                    t -= weightPercent;
                }
            }
            return default;
        }

        public static float GetValuePercent(EasingType type, float timePercent) {
            float valuePercent;
            switch (type) {
                case EasingType.Immediate: valuePercent = FunctionHelper.EaseImmediate(timePercent); break;
                case EasingType.Linear: valuePercent = FunctionHelper.EaseLinear(timePercent); break;
                case EasingType.InQuad: valuePercent = FunctionHelper.EaseInQuad(timePercent); break;
                case EasingType.OutQuad: valuePercent = FunctionHelper.EaseOutQuad(timePercent); break;
                case EasingType.InOutQuad: valuePercent = FunctionHelper.EaseInOutQuad(timePercent); break;
                case EasingType.InCubic: valuePercent = FunctionHelper.EaseInCubic(timePercent); break;
                case EasingType.OutCubic: valuePercent = FunctionHelper.EaseOutCubic(timePercent); break;
                case EasingType.InOutCubic: valuePercent = FunctionHelper.EaseInOutCubic(timePercent); break;
                case EasingType.InQuart: valuePercent = FunctionHelper.EaseInQuart(timePercent); break;
                case EasingType.OutQuart: valuePercent = FunctionHelper.EaseOutQuart(timePercent); break;
                case EasingType.InOutQuart: valuePercent = FunctionHelper.EaseInOutQuart(timePercent); break;
                case EasingType.InQuint: valuePercent = FunctionHelper.EaseInQuint(timePercent); break;
                case EasingType.OutQuint: valuePercent = FunctionHelper.EaseOutQuint(timePercent); break;
                case EasingType.InOutQuint: valuePercent = FunctionHelper.EaseInOutQuint(timePercent); break;
                case EasingType.InSine: valuePercent = FunctionHelper.EaseInSine(timePercent); break;
                case EasingType.OutSine: valuePercent = FunctionHelper.EaseOutSine(timePercent); break;
                case EasingType.InOutSine: valuePercent = FunctionHelper.EaseInOutSine(timePercent); break;
                case EasingType.InExpo: valuePercent = FunctionHelper.EaseInExpo(timePercent); break;
                case EasingType.OutExpo: valuePercent = FunctionHelper.EaseOutExpo(timePercent); break;
                case EasingType.InOutExpo: valuePercent = FunctionHelper.EaseInOutExpo(timePercent); break;
                case EasingType.InCirc: valuePercent = FunctionHelper.EaseInCirc(timePercent); break;
                case EasingType.OutCirc: valuePercent = FunctionHelper.EaseOutCirc(timePercent); break;
                case EasingType.InOutCirc: valuePercent = FunctionHelper.EaseInOutCirc(timePercent); break;
                case EasingType.InElastic: valuePercent = FunctionHelper.EaseInElastic(timePercent); break;
                case EasingType.OutElastic: valuePercent = FunctionHelper.EaseOutElastic(timePercent); break;
                case EasingType.InOutElastic: valuePercent = FunctionHelper.EaseInOutElastic(timePercent); break;
                case EasingType.InBack: valuePercent = FunctionHelper.EaseInBack(timePercent); break;
                case EasingType.OutBack: valuePercent = FunctionHelper.EaseOutBack(timePercent); break;
                case EasingType.InOutBack: valuePercent = FunctionHelper.EaseInOutBack(timePercent); break;
                case EasingType.InBounce: valuePercent = FunctionHelper.EaseInBounce(timePercent); break;
                case EasingType.OutBounce: valuePercent = FunctionHelper.EaseOutBounce(timePercent); break;
                case EasingType.InOutBounce: valuePercent = FunctionHelper.EaseInOutBounce(timePercent); break;
                default: throw new System.ArgumentException("Invalid EasingType" + type.ToString());
            }
            return valuePercent;
        }

    }

}