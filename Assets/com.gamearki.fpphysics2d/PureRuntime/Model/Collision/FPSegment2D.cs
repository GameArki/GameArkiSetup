using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public struct FPSegment2D {

        FPVector2 a;
        public FPVector2 A => a;
        FPVector2 b;
        public FPVector2 B => b;

        public FPSegment2D(FPVector2 a, FPVector2 b) {
            this.a = a;
            this.b = b;
        }
        
        public FPBounds2 GetPruneBounding() {
            var center = (a + b) * FP64.Half;
            var size = FPVector2.Abs(a - b);
            return new FPBounds2(center, size);
        }

    }

}