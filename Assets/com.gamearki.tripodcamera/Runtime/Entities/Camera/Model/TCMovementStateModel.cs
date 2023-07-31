using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera {

    public struct TCMovementStateModel {

        public Vector2 offset;
        public float duration;
        public EasingType easingType;
        public bool isInherit;

    }
}