using UnityEngine;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.EditorTool {

    public static class TCEM2TMUtil {

        public static TCDollyTrackStateTM ToTCDollyTrackStateTM(in TCDollyTrackStateEM em) {
            TCDollyTrackStateTM tm;
            tm.trackType = em.trackType;
            tm.bezierSlineTMArray = ToTCBezierSplineTMArray(em.bezierSlineEMArray);
            return tm;
        }

        public static TCBezierSplineTM ToTCBezierSplineTM(in TCBezierSplineEM em) {
            TCBezierSplineTM tm;
            tm.start = em.start;
            tm.c1 = em.c1;
            tm.c2 = em.c2;
            tm.end = em.end;
            tm.duration = em.duration;
            tm.timeEasingType = em.timeEasingType;
            return tm;
        }
        
        public static TCBezierSplineTM[] ToTCBezierSplineTMArray(TCBezierSplineEM[] emArray) {
            TCBezierSplineTM[] tmArray = new TCBezierSplineTM[emArray.Length];
            for (int i = 0; i < emArray.Length; i++) {
                tmArray[i] = ToTCBezierSplineTM(emArray[i]);
            }
            return tmArray;
        }

    }

}