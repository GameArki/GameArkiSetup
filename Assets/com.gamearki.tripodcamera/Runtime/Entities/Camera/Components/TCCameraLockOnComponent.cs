using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraLockOnComponent {

        Transform tf;

        EasingType xEasingType;
        float xEasingDuration;
        EasingType yEasingType;
        float yEasingDuration;

        // ==== Temp ====
        EasingElement easingElement;

        public TCCameraLockOnComponent() {
            easingElement = new EasingElement();
        }

        public void SetLockOn(Transform tf, EasingType xEasingType, float xEasingDuration, EasingType yEasingType, float yEasingDuration) {
            this.tf = tf;
            this.xEasingType = xEasingType;
            this.xEasingDuration = xEasingDuration;
            this.yEasingType = yEasingType;
            this.yEasingDuration = yEasingDuration;
            easingElement.Reset(tf.position);
        }

        public void Tick(float dt) {
            if (tf == null) {
                return;
            }
            easingElement.TickEasing(dt,
                                     xEasingType,
                                     xEasingDuration,
                                     yEasingType,
                                     yEasingDuration,
                                     tf.position);
        }

        public bool IsLockOn() {
            return tf != null;
        }

        public Vector3 GetLockOnPos() {
            if (tf == null) {
                return Vector3.zero;
            }
            return easingElement.EaseValue;
        }

    }

}