using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public struct FPSphere2D {

        FPVector2 center;
        public FPVector2 Center => center;

        FP64 radius;
        public FP64 Radius => radius;

        public FPSphere2D(in FPVector2 center, in FP64 radius) {
            this.center = center;
            this.radius = radius;
        }

    }

}