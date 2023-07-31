using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public struct FPAABB2D {

        FPVector2 min;
        public FPVector2 Min => min;

        FPVector2 max;
        public FPVector2 Max => max;

        public FPAABB2D(in FPVector2 center, in FPVector2 size) {
            FPVector2 halfSize = size * FP64.Half;
            this.min = center - halfSize;
            this.max = center + halfSize;
        }

        public FPVector2 Center() {
            return (min + max) * FP64.Half;
        }

        public FPVector2 Size() {
            return max - min;
        }

        public FPVector2 HalfSize() {
            return (max - min) * FP64.Half;
        }

        public bool IsValid() {
            FPVector2 diff = max - min;
            bool valid = diff.x >= FP64.Zero && diff.y >= FP64.Zero;
            return valid;
        }

        public void MoveCenter(in FPVector2 newCenter) {
            FPVector2 offset = newCenter - Center();
            this.min += offset;
            this.max += offset;
        }

        public bool IsInside(in FPVector2 point) {
            return ((point.x >= min.x) && (point.x <= max.x) && (point.y >= min.y) && (point.y <= max.y));
        }

    }

}