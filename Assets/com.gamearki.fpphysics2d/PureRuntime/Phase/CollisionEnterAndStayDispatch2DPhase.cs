namespace GameArki.FPPhysics2D {

    internal class CollisionEnterAndStayDispatch2DPhase {

        FPContext2D context;

        internal CollisionEnterAndStayDispatch2DPhase() { }

        internal void Inject(FPContext2D context) {
            this.context = context;
        }

        internal void Tick() {
            var collisionEventCenter = context.CollisionEventCenter;
            while (collisionEventCenter.TryDequeueEnter(out var ev)) {
                ApplyCollisionEnter(ev);
            }

            while (collisionEventCenter.TryDequeueStay(out var ev)) {
                ApplyCollisionStay(ev);
            }
        }

        void ApplyCollisionEnter(in InternalCollision2DEventModel ev) {
            // Public Collision
            var events = context.Events;
            events.CollisionEnter(new API.CollisionEventArgs(ev.A, ev.B));
        }

        void ApplyCollisionStay(in InternalCollision2DEventModel ev) {
            // Public Collision
            var events = context.Events;
            events.CollisionStay(new API.CollisionEventArgs(ev.A, ev.B));
        }

    }

}