using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    [Serializable]
    public struct TCLookAtModel {

        public EasingType easingType_horizontal;
        public EasingType easingType_vertical;
        public float duration_horizontal;
        public float duration_vertical;
        public Vector3 normalLookAngles;
        public bool normalLookActivated;
        public Vector3 lookAtTargetOffset;
        
        public float maxLookDownDegree;
        public float maxLookUpDegree;

        public TCLookAtComposerModel composerModel;

    }

}