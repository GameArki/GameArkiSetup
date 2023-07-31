using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera {

    public struct TCDollyTrackStateModel {

        public TCDollyTrackType trackType;
        public TCBezierSplineModel[] bezierSlineModelArray;

        public float totalDuraration;

    }
}