using UnityEngine;

namespace GameArki.TripodCamera {

    public static class TCBezierUtil {
        
        public static float GetBezierValueLv2(float start, float end, float c1, float t) {
            return Mathf.Pow((1 - t), 2) * start
            + 2 * t * (1 - t) * c1
            + Mathf.Pow(t, 2) * end;
        }

        public static float GetBezierValueLv3(float start, float end, float c1, float c2, float t) {
            return Mathf.Pow((1 - t), 3) * start
             + 3 * t * Mathf.Pow((1 - t), 2) * c1
             + 3 * Mathf.Pow(t, 2) * (1 - t) * c2
             + Mathf.Pow(t, 3) * end;
        }

    }

}