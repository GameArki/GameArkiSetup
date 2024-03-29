using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public struct TCLookAtModel {

        public EasingType easingType_horizontal;
        public EasingType easingType_vertical;
        public float duration_horizontal;
        public float duration_vertical;
        public Vector3 normalLookAngles;
        public bool normalLookActivated;
        public Vector3 lookAtTargetOffset;
        
        public TCLookAtComposerModel composerModel;

    }

}