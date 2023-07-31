using FixMath.NET;

namespace GameArki.FPPhysics2D.Phases {

    // 穿透处理
    internal class Penetrate2DPhase {

        FPContext2D context;

        internal Penetrate2DPhase() { }

        internal void Inject(FPContext2D context) {
            this.context = context;
        }

        internal void Tick(in FP64 step) {
            var contactRepo = context.CollisionContactRepo;
            var contacts = contactRepo.GetAll();
            for (int i = 0; i < contacts.Length; i += 1) {
                var kv = contacts[i];
                var contact = kv.Value;
                var a = contact.A;
                var b = contact.B;
                if (a.IsStatic && b.IsStatic) {
                    continue;
                }
                if (a.Material.BouncinessPercent > FP64.Zero || b.Material.BouncinessPercent > FP64.Zero) {
                    ApplyBounce(contact);
                } else {
                    ApplyRestore(a, b);
                }
                ApplyCollisionStayEvent(a, b);
            }
        }

        // ==== Restore ====
        // 如无弹性, 恢复至交接点
        void ApplyRestore(FPRigidbody2DEntity a, FPRigidbody2DEntity b) {

            // Static & Dynamic
            if (a.IsStatic && !b.IsStatic) {
                RestoreStatic_Dynamic(b, a);
                return;
            } else if (!a.IsStatic && b.IsStatic) {
                RestoreStatic_Dynamic(a, b);
                return;
            }

            // Dynamic & Dynamic
            if (!a.IsStatic && !b.IsStatic) {
                RestoreDynamic_Dynamic(a, b);
                return;
            }

        }

        // 1. 获取交叉的点
        // 2. 取交叉的中间点
        // 3. 恢复至这个中间点
        // 4. 入列 Collision Enter
        void RestoreStatic_Dynamic(FPRigidbody2DEntity d_rb, FPRigidbody2DEntity s_rb) {

            // d: dynamic
            // s: static
            var d_shape = d_rb.Shape;
            var s_shape = s_rb.Shape;

            var d_tf = d_rb.TF;
            var s_tf = s_rb.TF;

            FPBoxShape2D d_box = d_shape as FPBoxShape2D;
            FPBoxShape2D s_box = s_shape as FPBoxShape2D;
            if (d_box != null && s_box != null) {
                if (d_tf.RadAngle == FP64.Zero && s_tf.RadAngle == FP64.Zero) {
                    // D_AABB & S_AABB
                    RestoreDAABB_SAABB(d_rb, d_box, s_rb, s_box);
                    return;
                }
            }

            throw new System.Exception($"未实现该情况: shapeD{d_shape.GetType().ToString()}, shapeS{s_shape.GetType().ToString()}");

        }

        void RestoreDAABB_SAABB(FPRigidbody2DEntity d_rb, FPBoxShape2D d_box, FPRigidbody2DEntity s_rb, FPBoxShape2D s_box) {
            var d_velo = d_rb.LinearVelocity;
            var d_tf = d_rb.TF;
            var s_tf = s_rb.TF;
            var d_aabb = d_box.GetAABB(d_tf);
            var s_aabb = s_box.GetAABB(s_tf);
            if (s_rb.PassableDirection == FPPassThroughDirection.Up) {
                if (d_velo.y > FP64.Zero) {
                    return;
                } else if (d_velo.y < FP64.Zero) {
                    if (d_aabb.Min.y < s_aabb.Center().y) {
                        return;
                    }
                }
            }

            FPVector2 d_pos = d_tf.Pos;
            FPVector2 s_pos = s_tf.Pos;
            FPVector2 diff = d_pos - s_pos;
            var d_half = d_aabb.HalfSize();
            var s_half = s_aabb.HalfSize();
            var abs_diff = FPVector2.Abs(diff);
            var abs_d_min = -d_half + abs_diff;
            var abs_s_max = s_half;
            var depth_diff = abs_s_max - abs_d_min;
            if (depth_diff.y <= depth_diff.x) {
                if (diff.y >= FP64.Zero) {
                    // dynamic RB is up
                    d_pos.y = s_aabb.Max.y + d_half.y;
                    if (d_velo.y < 0) {
                        d_velo.y = FP64.Zero;
                    }
                } else {
                    // dynamic RB is down
                    d_pos.y = s_aabb.Min.y - d_half.y;
                    if (d_velo.y > 0) {
                        d_velo.y = FP64.Zero;
                    }
                }
            } else if (depth_diff.x <= depth_diff.y) {
                if (diff.x >= FP64.Zero) {
                    // dynamic RB is right
                    d_pos.x = s_aabb.Max.x + d_half.x;
                    if (d_velo.x < 0) {
                        d_velo.x = FP64.Zero;
                    }
                } else {
                    // dynamic RB is left
                    d_pos.x = s_aabb.Min.x - d_half.x;
                    if (d_velo.x > 0) {
                        d_velo.x = FP64.Zero;
                    }
                }
            }
            d_rb.SetPos(d_pos);

            // Friction
            FP64 d_friction = FP64.One - d_rb.Material.FrictionPercent;
            d_velo = d_velo * d_friction;
            d_rb.SetLinearVelocity(d_velo);

        }

        void RestoreDynamic_Dynamic(FPRigidbody2DEntity rb1, FPRigidbody2DEntity rb2) {
            // System.Console.WriteLine("TODO: D & D");
        }

        // ==== Rebound ====
        // 如有弹性, 反弹
        void ApplyBounce(in CollisionContact2DModel contact) {

            var a = contact.A;
            var b = contact.B;

            System.Console.WriteLine("TODO: Rebound");

            // Static & Dynamic

            // Dynamic & Dynamic

        }

        // 1. 获取交叉的点
        // 2. 取一个中间点
        // 3. 设置反弹力 / 反弹旋转力
        // 4. 入列 Collision Enter
        void ReboundStatic_Dynamic() {

        }

        void ReboundDynamic_Dynamic() {

        }

        // ==== Event ====
        void ApplyCollisionStayEvent(FPRigidbody2DEntity a, FPRigidbody2DEntity b) {
            ulong key = DictionaryKeyUtil.ComputeRBKey(a, b);
            var collisionContactRepo = context.CollisionContactRepo;
            var eventCenter = context.CollisionEventCenter;
            if (collisionContactRepo.Contains(key)) {
                // Collision: Stay
                eventCenter.EnqueueStay(new InternalCollision2DEventModel(a, b));
            }
        }

    }

}