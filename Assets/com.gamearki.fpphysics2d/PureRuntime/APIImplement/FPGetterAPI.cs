using System;
using FixMath.NET;

namespace GameArki.FPPhysics2D.API {

    public class FPGetterAPI : IFPGetterAPI {

        FPContext2D context;
        internal void Inject(FPContext2D context) {
            this.context = context;
        }

        // - Raycast 
        public bool Raycast2D(in FPRay2D ray, in FP64 distance, FPContactFilter2DArgs contactFilter, RayCastHit2DArgs[] hits) {

            var result = false;
            var repo = context.RBRepo;
            var bounds = ray.GetPruneBounding(distance);
            var candidates = repo.GetCandidatesByBounds(bounds);

            foreach (var rb in candidates) {
                var intersectNum = 0;

                // 过滤
                var isFiltering = contactFilter.isFiltering;
                var useTriggers = contactFilter.useTriggers;
                var useLayerMask = contactFilter.useLayerMask;
                var layerMask = contactFilter.layerMask;

                if (isFiltering) {
                    if (!useTriggers && rb.IsTrigger) {
                        continue;
                    }
                    if (useLayerMask && rb.Layer == layerMask) {
                        continue;
                    }
                    if (rb.IsStatic) {
                        continue;
                    }
                }

                // 检测
                bool isIntersect = Intersect2DUtil.IsIntersectRay_RB(ray, distance, rb, out FPVector2 intersectPoint, FP64.Epsilon);
                if (!isIntersect) {
                    continue;
                } else {
                    var hit = new RayCastHit2DArgs();
                    hit.rigidbody = rb;
                    hit.point = intersectPoint;
                    hits[intersectNum] = hit;
                    result = true;
                    continue;
                }
            }
            return result;
        }

        public bool SegmentCast2DNearStart(in FPSegment2D segment, FPContactFilter2DArgs contactFilter, SegmentCastHit2DArgs[] hits) {
            var hitType = FPSegment2DHitType.NearStart;
            return SegmentCast2D(segment, contactFilter, hitType, hits);
        }

        public bool SegmentCast2DNearEnd(in FPSegment2D segment, FPContactFilter2DArgs contactFilter, SegmentCastHit2DArgs[] hits) {
            var hitType = FPSegment2DHitType.NearEnd;
            return SegmentCast2D(segment, contactFilter, hitType, hits);
        }

        public bool SegmentCast2DAll(in FPSegment2D segment, FPContactFilter2DArgs contactFilter, SegmentCastHit2DArgs[] hits) {
            var hitType = FPSegment2DHitType.All;
            return SegmentCast2D(segment, contactFilter, hitType, hits);
        }

        internal bool SegmentCast2D(in FPSegment2D segment, FPContactFilter2DArgs contactFilter, FPSegment2DHitType hitType, SegmentCastHit2DArgs[] hits) {
            var result = false;
            var repo = context.RBRepo;
            var bounds = segment.GetPruneBounding();
            var candidates = repo.GetCandidatesByBounds(bounds);

            foreach (var rb in candidates) {
                var intersectNum = 0;

                // 过滤
                var isFiltering = contactFilter.isFiltering;
                var useTriggers = contactFilter.useTriggers;
                var useLayerMask = contactFilter.useLayerMask;
                var layerMask = contactFilter.layerMask;

                if (isFiltering) {
                    if (!useTriggers && rb.IsTrigger) {
                        continue;
                    }
                    if (useLayerMask && rb.Layer == layerMask) {
                        continue;
                    }
                    if (rb.IsStatic) {
                        continue;
                    }
                }
                var intersectPoints = new FPVector2[2];
                // 检测
                bool isIntersect = Intersect2DUtil.IsIntersectSegment_RB(segment, rb, in hitType, intersectPoints, FP64.Epsilon);
                if (!isIntersect) {
                    continue;
                } else {
                    var hit = new SegmentCastHit2DArgs();
                    hit.rigidbody = rb;
                    hit.points = intersectPoints;
                    hits[intersectNum] = hit;
                    result = true;
                    continue;
                }
            }
            return result;

        }

    }
}