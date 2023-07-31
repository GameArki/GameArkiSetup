using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public interface IShape2D {
        FPBounds2 GetPruneBounding(FPTransform2D tf);
    }

}