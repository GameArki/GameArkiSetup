namespace GameArki.FPPhysics2D {

    internal class CollisionExitDispatch2DPhase {

        FPContext2D context;

        internal CollisionExitDispatch2DPhase() {}

        internal void Inject(FPContext2D context) {
            this.context = context;
        }

        internal void Tick() {
            var collisionEventCenter = context.CollisionEventCenter;
            while(collisionEventCenter.TryDequeueExit(out var ev)) {
                ApplyCollisionExit(ev);
            }
        }

        void ApplyCollisionExit(in InternalCollision2DEventModel ev) {
            var a = ev.A;
            var b = ev.B;

            // Public Collision
            var events = context.Events;
            if (!a.IsTrigger && !b.IsTrigger) {
                events.CollisionExit(new API.CollisionEventArgs(ev.A, ev.B));
            }
        }

    }
}