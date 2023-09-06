using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Template {

    [Serializable]
    public struct TCLookAtTM {

        public EasingType easingType_horizontal;
        public float duration_horizontal;
        public EasingType easingType_vertical;
        public float duration_vertical;
        public Vector3 normalLookAngles;
        public bool normalLookActivated;
        public Vector3 lookAtTargetOffset;

        public TCLookAtComposerTM composerTM;
    }

}