using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Template {

    [Serializable]
    public struct TCShakeStateTM {

        public Vector3 amplitudeOffset;
        public EasingType easingType;
        public float frequency;
        public float duration;
        
    }

}