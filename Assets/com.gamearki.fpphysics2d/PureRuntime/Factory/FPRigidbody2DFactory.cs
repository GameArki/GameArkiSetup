using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public static class FPRigidbody2DFactory {

        public static FPRigidbody2DEntity CreateBoxRB(in FPVector2 pos, in FP64 degreeAngle, in FPVector2 size) {
            FPBoxShape2D box = new FPBoxShape2D(size);
            FPRigidbody2DEntity rb = new FPRigidbody2DEntity(pos, degreeAngle * FP64.Deg2Rad, box);
            return rb;
        }

        public static FPRigidbody2DEntity CreateCircleRB(in FPVector2 pos, in FP64 radius) {
            FPCircleShape2D circle = new FPCircleShape2D(radius);
            FPRigidbody2DEntity rb = new FPRigidbody2DEntity(pos, 0, circle);
            return rb;
        }

    }

}