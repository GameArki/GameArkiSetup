using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Template {

    [Serializable]
    public struct TCMovementStateTM {

        public Vector2 offset;
        public float duration;
        public EasingType easingType;
        public bool isInherit;
        
    }

}