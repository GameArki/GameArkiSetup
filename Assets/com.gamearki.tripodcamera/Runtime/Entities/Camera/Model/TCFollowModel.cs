using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    [Serializable]
    public struct TCFollowModel {

        public TCFollowType followType;
        public Vector3 normalFollowOffset;
        public EasingType easingType_horizontal;
        public EasingType easingType_vertical;
        public float duration_horizontal;
        public float duration_vertical;

    }

}