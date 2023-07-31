using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEditor.TestTools;
using FixMath.NET;
using GameArki.FPPhysics2D;


namespace GameArki.FPPhysics2D.Test {

    public class Test_Intersect2DUtil {

        [TestCase(-4f, 1f, 1f, 7f, 2f, 11f, 6f, 4f, false)]
        [TestCase(-4f, 1f, 6f, 4f, 2f, 11f, 6f, 4f, true)]
        [TestCase(-14f, 1f, 11f, 7f, -1f, 6f, 6f, 4f, true)]
        [TestCase(-1f, 6f, 6f, 4f, -1f, 7f, 11f, 7f, false)]
        [TestCase(-1f, 6f, 5f, 0f, 2f, 3f, 6f, 7f, true)]
        [TestCase(-1f, 6f, 5f, 0f, 3f, 4f, 6f, 7f, false)]
        [TestCase(-1f, 6f, 5f, 0f, 0f, 7f, 6f, 1f, false)]
        public void Test_Segment_Segment(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, bool expected) {
            var aPos = new FPVector2((FP64)x1, (FP64)y1);
            var bPos = new FPVector2((FP64)x2, (FP64)y2);
            var cPos = new FPVector2((FP64)x3, (FP64)y3);
            var dPos = new FPVector2((FP64)x4, (FP64)y4);
            //var isIntersect = Intersect2DUtil.IsIntersectSegment_Segment(aPos, bPos, cPos, dPos, out FPVector2 intersectPoint);
            //Assert.That(isIntersect == expected);
        }

        [TestCase(-4f, 1f, 1f, 7f, 2f, 11f, 6f, 4f, true)]
        [TestCase(-4f, 1f, 1f, 7f, -6f, 1f, -4f, -2f, false)]
        [TestCase(-4f, 1f, 1f, 7f, -5f, 8f, -3f, 5f, false)]
        [TestCase(-4f, 1f, 6f, 4f, 2f, 11f, 6f, 4f, true)]
        [TestCase(-14f, 1f, 11f, 7f, -1f, 6f, 6f, 4f, true)]
        [TestCase(-1f, 6f, 6f, 4f, -1f, 7f, 11f, 7f, false)]
        [TestCase(-1f, 6f, 5f, 0f, 2f, 3f, 6f, 7f, true)]
        [TestCase(-1f, 6f, 5f, 0f, 3f, 4f, 6f, 7f, false)]
        [TestCase(-1f, 6f, 5f, 0f, 0f, 7f, 6f, 1f, false)]
        public void Test_Ray_Segment(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, bool expected) {
            var aPos = new FPVector2((FP64)x1, (FP64)y1);
            var bPos = new FPVector2((FP64)x2, (FP64)y2);
            var cPos = new FPVector2((FP64)x3, (FP64)y3);
            var dPos = new FPVector2((FP64)x4, (FP64)y4);
            //var isIntersect = Intersect2DUtil.IsIntersectRay_Segment(aPos, bPos, cPos, dPos, out FPVector2 intersectPoint);
            //Assert.That(isIntersect == expected);
        }

        [TestCase(0f, 2f, 2f, 0f, 0f, 0f, 1f, false)]
        [TestCase(0f, 2f, 2f, 0f, 0f, 0f, 2f, true)]
        public void Test_Segment_Circle(float x1, float y1, float x2, float y2, float centerX, float centerY, float radius, bool expected) {
            var aPos = new FPVector2((FP64)x1, (FP64)y1);
            var bPos = new FPVector2((FP64)x2, (FP64)y2);
            var center = new FPVector2((FP64)centerX, (FP64)centerY);
            FPTransform2D TF = new FPTransform2D(center, (FP64)0);
            FPCircleShape2D circle = new FPCircleShape2D((FP64)radius);
            //var isIntersect = Intersect2DUtil.IsIntersectSegment_Circle(aPos, bPos, TF, circle, out FPVector2 intersectPoint, FP64.Epsilon);
            //Assert.That(isIntersect == expected);
        }

    }
}
