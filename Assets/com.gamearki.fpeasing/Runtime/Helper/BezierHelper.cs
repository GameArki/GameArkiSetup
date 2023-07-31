using UnityEngine;
using System;


namespace GameArki.FPEasing {

    public static class BezierHelper {

        public static float CubicBezier(float t, float p0, float p1, float p2, float p3) {
            return p0 * MathF.Pow((1 - t), 3) +
                   p1 * 3 * t * MathF.Pow((1 - t), 2) +
                   p2 * 3 * MathF.Pow(t, 2) * (1 - t) +
                   p3 * MathF.Pow(t, 3);
        }

        // Cubic Bezier
        // Input: Time Percent
        // Input: P0, P1, P2, P3
        // Output: Value Percent
        public static Vector2 CubicBezier(float t, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4) {

            // 1. Get 4 points

            // 2. Get 3 points
            Vector2 p1_2 = Vector2.Lerp(p1, p2, t);
            Vector2 p2_3 = Vector2.Lerp(p2, p3, t);
            Vector2 p3_4 = Vector2.Lerp(p3, p4, t);

            // 3. Get 2 points
            Vector2 p12_23 = Vector2.Lerp(p1_2, p2_3, t);
            Vector2 p23_34 = Vector2.Lerp(p2_3, p3_4, t);

            // 4. Get 1 point
            Vector2 p1234 = Vector2.Lerp(p12_23, p23_34, t);

            return p1234;

        }


    }

}