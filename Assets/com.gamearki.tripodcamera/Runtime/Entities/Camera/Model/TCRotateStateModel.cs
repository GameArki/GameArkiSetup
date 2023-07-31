using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera {

    public struct TCRotateStateModel {

        public Vector3 offset;
        public float duration;
        
        public EasingType easingType;
        public bool isInherit;

    }
}