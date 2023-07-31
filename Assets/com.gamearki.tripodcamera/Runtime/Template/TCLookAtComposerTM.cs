using System;
using UnityEngine;

namespace GameArki.TripodCamera.Template {

    [Serializable]
    public struct TCLookAtComposerTM {

        public TCLookAtComposerType composerType;
        public float screenNormalizedX;
        public float screenNormalizedY;
        public float deadZoneNormalizedW;
        public float deadZoneNormalizedH;
        public float softZoneNormalizedW;
        public float softZoneNormalizedH;
        public float normalDamping;
        public Vector3 normalLookAngles;
        public bool normalLookActivated;

    }

}