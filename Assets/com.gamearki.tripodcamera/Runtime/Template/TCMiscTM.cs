using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Template {

    [Serializable]
    public struct TCMiscTM {

        public float maxLookUpDegree;
        public float maxLookDownDegree;
        public bool lookLimitActivated;

        public float maxMoveSpeed;
        public bool moveSpeedLimitActivated;

        public float maxRotateSpeed;
        public bool rotateSpeedLimitActivated;

    }

}