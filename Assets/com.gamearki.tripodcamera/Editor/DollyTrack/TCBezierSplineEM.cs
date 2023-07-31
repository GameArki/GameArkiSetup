using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.EditorTool {

    [System.Serializable]
    public struct TCBezierSplineEM {

        public TCWayPoint start;
        public TCWayPoint end;
        public Vector3 c1;
        public Vector3 c2;
        public bool isScenePositionHandleEnabled;
        public float duration;
        public EasingType timeEasingType;

    }


}

