namespace GameArki.FPPhysics2D {

    internal class TriggerExitDispatch2DPhase {

        FPContext2D context;

        public TriggerExitDispatch2DPhase() { }

        internal void Inject(FPContext2D context) {
            this.context = context;
        }

        internal void Tick() {
            var triggerEventCenter = context.TriggerEventCenter;
            while (triggerEventCenter.TryDequeueExit(out var ev)) {
                ApplyTriggerExit(ev);
            }
        }

        void ApplyTriggerExit(in InternalTrigger2DEventModel ev) {
            var a = ev.A;
            var b = ev.B;

            // Public Trigger
            if (a.IsTrigger || b.IsTrigger) {
                var events = context.Events;
                events.TriggerExit(new API.TriggerEventArgs(a, b));
            }

            // Remove From Collision
            var collisionRepo = context.CollisionContactRepo;
            if (a.IsTrigger && b.IsTrigger) {
                return;
            }
            var collisionEventCenter = context.CollisionEventCenter;
            ulong key = DictionaryKeyUtil.ComputeRBKey(a, b);
            if (collisionRepo.Remove(key)) {
                collisionEventCenter.EnqueueExit(new InternalCollision2DEventModel(a, b));
            }

        }

    }
}