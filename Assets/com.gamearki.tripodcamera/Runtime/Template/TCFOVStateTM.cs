using System;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Template {

    [Serializable]
    public struct TCFOVStateTM {
        
        public float offset;
        public float duration;
        public EasingType easingType;
        public bool isInherit;
        
    }

}