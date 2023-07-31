#if UNITY_EDITOR
using UnityEngine;
using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public static class VectorExtention {

        public static Vector2 ToVector2(this FPVector2 v) {
            return new Vector2(v.x.AsFloat(), v.y.AsFloat());
        }

        public static FPVector2 ToFPVector2(this Vector2 v) {
            return new FPVector2((FP64)v.x, (FP64)v.y);
        }

        public static FPVector2 ToFPVector2(this Vector3 v) {
            return new FPVector2((FP64)v.x, (FP64)v.y);
        }

    }
}
#endif
