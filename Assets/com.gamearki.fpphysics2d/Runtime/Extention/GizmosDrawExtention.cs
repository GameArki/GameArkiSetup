using UnityEngine;
using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public static class GizmosDrawExtention {

        public static void GizmosDrawAllRigidbody(this FPSpace2D space2D) {
            var all = space2D.GetAllRigidbody();
            for (int i = 0; i < all.Length; i += 1) {
                all[i].GizmosDrawGizmos();
            }
        }

        public static void GizmosDrawGizmos(this FPRigidbody2DEntity rb) {
            Color color = Color.yellow;
            FPVector2 pos = rb.TF.Pos;
            FP64 radAngle = rb.TF.Rot.RadAngle;

            switch (rb.Shape) {
                case FPBoxShape2D box:

                    if (radAngle == FP64.Zero) {
                        // AABB
                        GizmosHelper.DrawPoint(pos, color);
                        //GizmosHelper.DrawCube(pos, box.Size, color);
                        var min = pos - box.Size / 2;
                        var max = pos + box.Size / 2;
                        GizmosHelper.DrawBox(min, max, color);
                    } else {
                        // OBB
                        GizmosHelper.DrawPoint(pos, color);
                        FPRotation2D rot = new FPRotation2D(radAngle);
                        var axisY = FPMath2DUtil.MulRotAndPos(rot, FPVector2.UnitY);
                        var axisX = FPMath2DUtil.MulRotAndPos(rot, FPVector2.UnitX);
                        FPVector2 half = box.Size * FP64.Half;
                        FPVector2 ax = axisX * half.x;
                        FPVector2 ay = axisY * half.y;
                        var vertices = new FPVector2[4];
                        vertices[0] = pos + (-ax + -ay);
                        vertices[1] = pos + (-ax + ay);
                        vertices[2] = pos + (ax + ay);
                        vertices[3] = pos + (ax + -ay);
                        GizmosHelper.DrawVerticis(vertices, color);
                    }
                    break;
                case FPCircleShape2D circle:
                    Gizmos.color = color;
                    Gizmos.DrawWireSphere(pos.ToVector2(), circle.Radius.AsFloat());
                    break;
                default:
                    throw new System.Exception($"未实现: {rb.Shape.GetType()}");
            }
        }

    }
}