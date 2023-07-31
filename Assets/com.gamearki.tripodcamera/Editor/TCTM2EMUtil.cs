using UnityEngine;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.EditorTool {

    public static class TCTM2EMUtil {

        public static TCDollyTrackStateEM ToTCDollyTrackStateEM(in TCDollyTrackStateTM tm) {
            TCDollyTrackStateEM em;
            em.trackType = tm.trackType;
            em.bezierSlineEMArray = ToTCBezierSplineEMArray(tm.bezierSlineTMArray);
            return em;
        }

        public static TCBezierSplineEM ToTCBezierSplineEM(in TCBezierSplineTM tm) {
            TCBezierSplineEM em;
            em.start = tm.start;
            em.c1 = tm.c1;
            em.c2 = tm.c2;
            em.end = tm.end;
            em.duration = tm.duration;
            em.timeEasingType = tm.timeEasingType;
            em.isScenePositionHandleEnabled = false;
            return em;
        }

        public static TCBezierSplineEM[] ToTCBezierSplineEMArray(TCBezierSplineTM[] tmArray) {
            TCBezierSplineEM[] emArray = new TCBezierSplineEM[tmArray.Length];
            for (int i = 0; i < tmArray.Length; i++) {
                emArray[i] = ToTCBezierSplineEM(tmArray[i]);
            }
            return emArray;
        }

    }

}