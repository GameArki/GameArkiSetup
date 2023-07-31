using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public class FPBoxShape2D : IShape2D {

        FPVector2 size;
        public FPVector2 Size => size;

        FP64 radius;
        public FP64 Radius => radius;

        internal FPBoxShape2D(in FPVector2 size) {

            this.size = size;
            this.radius = MathHelper.Max(size.x, size.y) * FP64.Half;

        }

        internal FPAABB2D GetAABB(in FPTransform2D tf) {
            return new FPAABB2D(tf.Pos, size);
        }

        internal FPOBB2D GetOBB(in FPTransform2D tf) {
            return new FPOBB2D(tf.Pos, size, tf.RadAngle);
        }

        public void SetSize(in FPVector2 size) {
            this.size = size;
            this.radius = MathHelper.Max(size.x, size.y);
        }

        FPBounds2 IShape2D.GetPruneBounding(FPTransform2D tf) {
            if (tf.RadAngle == FP64.Zero) {
                return new FPBounds2(tf.Pos, size);
            } else {
                FP64 maxSide = FP64.Max(size.x, size.y);
                FPVector2 tarSize = new FPVector2(maxSide, maxSide);
                return new FPBounds2(tf.Pos, tarSize);
            }
        }
    }

}