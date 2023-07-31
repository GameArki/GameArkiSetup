using FixMath.NET;

namespace GameArki.FPPhysics2D.API {

    public interface IFPGetterAPI {

        // - Raycast
        bool Raycast2D(in FPRay2D ray, in FP64 distance, FPContactFilter2DArgs contactFilter, RayCastHit2DArgs[] hits);
        bool SegmentCast2DNearStart(in FPSegment2D segment, FPContactFilter2DArgs contactFilter, SegmentCastHit2DArgs[] hits);
        bool SegmentCast2DNearEnd(in FPSegment2D segment, FPContactFilter2DArgs contactFilter, SegmentCastHit2DArgs[] hits);
        bool SegmentCast2DAll(in FPSegment2D segment, FPContactFilter2DArgs contactFilter, SegmentCastHit2DArgs[] hits);

    }

}