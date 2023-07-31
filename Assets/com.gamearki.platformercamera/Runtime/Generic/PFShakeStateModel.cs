using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.PlatformerCamera {

    public struct PFShakeStateModel {

        public Vector2 amplitudeOffset;
        public EasingType easingType;
        public float frequency;
        public float duration;

        public PFShakeStateModel(Vector2 amplitudeOffset, EasingType easingType, float frequency, float duration) {
            this.amplitudeOffset = amplitudeOffset;
            this.easingType = easingType;
            this.frequency = frequency;
            this.duration = duration;
        }
    }
}