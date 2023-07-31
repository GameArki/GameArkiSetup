using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera {

    public struct TCBezierSplineModel {

        public TCWayPoint start;
        public TCWayPoint end;
        public Vector3 c1;
        public Vector3 c2;
        public float duration;
        public EasingType timeEasingType;

    }

}

