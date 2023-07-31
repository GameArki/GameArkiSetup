using FixMath.NET;

namespace GameArki.FPPhysics2D.Phases {

    internal class TriggerEnterAndStayDispatch2DPhase {

        FPContext2D context;

        internal TriggerEnterAndStayDispatch2DPhase() { }

        internal void Inject(FPContext2D context) {
            this.context = context;
        }

        internal void Tick() {

            var triggerEventCenter = context.TriggerEventCenter;

            while (triggerEventCenter.TryDequeueEnter(out var ev)) {
                ApplyTriggerEnter(ev);
            }

            while (triggerEventCenter.TryDequeueStay(out var ev)) {
                ApplyTriggerStay(ev);
            }

        }

        void ApplyTriggerEnter(in InternalTrigger2DEventModel ev) {
            var a = ev.A;
            var b = ev.B;

            // Public Trigger
            if (a.IsTrigger || b.IsTrigger) {
                var events = context.Events;
                events.TriggerEnter(new API.TriggerEventArgs(a, b));
            }

            // Add To Collision
            var collisionRepo = context.CollisionContactRepo;
            var collisionEventCenter = context.CollisionEventCenter;
            ulong key = DictionaryKeyUtil.ComputeRBKey(a, b);
            if (!collisionRepo.Contains(key)) {
                collisionRepo.Add(new CollisionContact2DModel(key, a, b));
                collisionEventCenter.EnqueueEnter(new InternalCollision2DEventModel(a, b));
            }
        }

        void ApplyTriggerStay(in InternalTrigger2DEventModel ev) {
            var a = ev.A;
            var b = ev.B;

            // Public Trigger
            if (a.IsTrigger || b.IsTrigger) {
                var events = context.Events;
                events.TriggerStay(new API.TriggerEventArgs(a, b));
            }

        }

    }

}