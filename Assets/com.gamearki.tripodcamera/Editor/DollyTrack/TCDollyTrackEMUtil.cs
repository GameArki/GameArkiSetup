using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.EditorTool {

    public static class TCDollyTrackEMUtil {

        public static TCWayPoint GetBezierWayPointLv3(in TCBezierSplineEM line, float ratioT) {
            TCWayPoint start = line.start;
            TCWayPoint end = line.end;
            Vector3 c1 = line.c1;
            Vector3 c2 = line.c2;
            float x = TCBezierUtil.GetBezierValueLv3(start.x, end.x, c1.x, c2.x, ratioT);
            float y = TCBezierUtil.GetBezierValueLv3(start.y, end.y, c1.y, c2.y, ratioT);
            float z = TCBezierUtil.GetBezierValueLv3(start.z, end.z, c1.z, c2.z, ratioT);
            float r = (end.r - start.r) * ratioT + start.r;
            return new TCWayPoint(x, y, z, r);
        }

        public static TCWayPoint GetBezierWayPointLv3(TCBezierSplineEM[] lineArray, float t, out int elementIndex, out float ratioT) {
            elementIndex = 0;
            ratioT = 0;

            var len = lineArray.Length;
            if (len == 0) {
                return new TCWayPoint();
            }

            var time = 0f;
            var timeEasingType = EasingType.Linear;
            for (int i = 0; i < len; i++) {
                var line = lineArray[i];
                var lineDuration = line.duration;
                timeEasingType = line.timeEasingType;
                var afterTime = time + lineDuration;
                if (t < afterTime) {
                    elementIndex = i;
                    ratioT = EasingHelper.Ease1D(timeEasingType, t - time, lineDuration, 0, 1);
                    break;
                }

                time = afterTime;
            }

            if (elementIndex >= len) {
                elementIndex = len - 1;
            }

            return GetBezierWayPointLv3(lineArray[elementIndex], ratioT);
        }
    }

    public static class Bezier3D3LineExtension {

        public static Vector3 GetTangentDir(this TCBezierSplineEM bezier3D3Line, float ratioT) {
            var start = bezier3D3Line.start.position;
            var c1 = bezier3D3Line.c1;
            var c2 = bezier3D3Line.c2;
            var end = bezier3D3Line.end.position;

            var param1 = 3 * Mathf.Pow((1 - ratioT), 2);
            var param2 = 6 * ratioT * (1 - ratioT);
            var param3 = 3 * Mathf.Pow(ratioT, 2);
            var slope = param1 * (c1 - start) + param2 * (c2 - c1) + param3 * (end - c2);
            var tangentDir = slope.normalized;
            return tangentDir;
        }

    }

}