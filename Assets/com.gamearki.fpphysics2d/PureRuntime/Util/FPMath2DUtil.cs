using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public static class FPMath2DUtil {

        public static FPVector2 MulRotAndPos(in FPRotation2D rot, in FPVector2 pos) {
            FP64 x = rot.CosValue * pos.x - rot.SinValue * pos.y;
            FP64 y = rot.SinValue * pos.x + rot.CosValue * pos.y;
            return new FPVector2(x, y);
        }

    }

}