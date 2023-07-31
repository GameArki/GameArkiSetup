using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public struct FPRotation2D {

        FP64 radAngle;
        public FP64 RadAngle => radAngle;

        FP64 sinValue;
        public FP64 SinValue => sinValue;

        FP64 cosValue;
        public FP64 CosValue => cosValue;

        public FPRotation2D(in FP64 radAngle) {
            this.radAngle = radAngle;
            sinValue = FP64.Sin(radAngle);
            cosValue = FP64.Cos(radAngle);
        }

        public static FPRotation2D operator *(in FPRotation2D a, in FPRotation2D b) {
            var radAngle = (a.radAngle + b.radAngle) % 360;
            return new FPRotation2D(radAngle);
        }

        public static FPVector2 operator *(in FPRotation2D rot, in FPVector2 pos) {
            FP64 x = rot.CosValue * pos.x - rot.SinValue * pos.y;
            FP64 y = rot.SinValue * pos.x + rot.CosValue * pos.y;
            return new FPVector2(x, y);
        }

    }

}