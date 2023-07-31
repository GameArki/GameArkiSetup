using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class EasingElement {

        // ==== Temp ====
        Vector3 easeValue;
        public Vector3 EaseValue => easeValue;

        Vector3 startValue;
        Vector3 dstValue;
        float time_horizontal;
        float time_vertical;

        public EasingElement() { }

        public void Reset(Vector3 startPos) {
            this.startValue = startPos;
            this.time_horizontal = 0;
            this.time_vertical = 0;
        }

        public void TickEasing(float dt,
                               EasingType easingType_horizontal,
                               float duration_horizontal,
                               EasingType easingType_vertical,
                               float duration_vertical,
                               Vector3 curValue) {

            if (dstValue != curValue) {
                startValue = easeValue;
                dstValue = curValue;
                time_horizontal = 0;
                time_vertical = 0;
            }

            if (duration_horizontal == 0) {
                easeValue.x = curValue.x;
                easeValue.z = curValue.z;
            } else if (time_horizontal < duration_horizontal) {
                time_horizontal += dt;
                var x = EasingHelper.Ease1D(easingType_horizontal, time_horizontal, duration_horizontal, startValue.x, dstValue.x);
                var z = EasingHelper.Ease1D(easingType_horizontal, time_horizontal, duration_horizontal, startValue.z, dstValue.z);
                easeValue.x = x;
                easeValue.z = z;
            }

            if (duration_vertical == 0) {
                easeValue.y = curValue.y;
            } else if (time_vertical < duration_vertical) {
                time_vertical += dt;
                var y = EasingHelper.Ease1D(easingType_vertical, time_vertical, duration_vertical, startValue.y, dstValue.y);
                easeValue.y = y;
            }

        }

    }

}