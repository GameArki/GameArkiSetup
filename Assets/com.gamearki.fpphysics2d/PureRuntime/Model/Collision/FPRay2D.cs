using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public struct FPRay2D {

        FPVector2 origin;
        public FPVector2 Origin => origin;
        FPVector2 direction;
        public FPVector2 Direction => direction;

        public FPVector2 GetPoint(FP64 distance) {
            return origin + direction * distance;
        }

        public FPRay2D(FPVector2 origin, FPVector2 direction) {
            this.origin = origin;
            this.direction = direction / direction.Length();
        }

        public FPSegment2D GetFPSegment2D(FP64 distance) {
            return new FPSegment2D(origin, GetPoint(distance));
        }

        public FPBounds2 GetPruneBounding(FP64 distance) {
            var end = GetPoint(distance);
            var center = (origin + end) * FP64.Half;
            var size = FPVector2.Abs(end - origin);
            return new FPBounds2(center, size);
        }

    }

}