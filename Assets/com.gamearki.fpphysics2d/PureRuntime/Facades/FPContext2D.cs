using FixMath.NET;
using GameArki.FPPhysics2D.API;

namespace GameArki.FPPhysics2D {

    internal class FPContext2D {

        // ==== API ====
        IFPEventTrigger events;
        public IFPEventTrigger Events => events;

        // ==== Environment ====
        FPEnvironment2DModel env;
        public FPEnvironment2DModel Env => env;

        // ==== Ignore ====
        FPIgnoreLayer2DModel ignore;
        public FPIgnoreLayer2DModel Ignore => ignore;

        // ==== Event Center ====
        // - Trigger
        Trigger2DEventCenter triggerEventCenter;
        public Trigger2DEventCenter TriggerEventCenter => triggerEventCenter;

        // - Collision
        Collision2DEventCenter collisionEventCenter;
        public Collision2DEventCenter CollisionEventCenter => collisionEventCenter;

        // ==== Repo ====
        // - Rigidbody Repo
        FPRigidbody2DRepository rbRepo;
        public FPRigidbody2DRepository RBRepo => rbRepo;

        // - Prune Ignore Repo
        PruneIgnoreContact2DRepository pruneIgnoreContactRepo;
        public PruneIgnoreContact2DRepository PruneIgnoreContactRepo => pruneIgnoreContactRepo;

        // - Intersect Contact Repo
        IntersectContact2DRepository intersectContactRepo;
        public IntersectContact2DRepository IntersectContactRepo => intersectContactRepo;

        // - Collision Contact Repo
        CollisionContact2DRepository collisionContactRepo;
        public CollisionContact2DRepository CollisionContactRepo => collisionContactRepo;

        public FPContext2D(FPVector2 worldSize, int maxDepth) {

            this.env = new FPEnvironment2DModel();
            this.ignore = new FPIgnoreLayer2DModel();

            // ==== Event Center ====
            this.triggerEventCenter = new Trigger2DEventCenter();
            this.collisionEventCenter = new Collision2DEventCenter();

            // ==== Repo ====
            this.rbRepo = new FPRigidbody2DRepository(worldSize, maxDepth);
            this.pruneIgnoreContactRepo = new PruneIgnoreContact2DRepository();
            this.intersectContactRepo = new IntersectContact2DRepository();
            this.collisionContactRepo = new CollisionContact2DRepository();

        }

        public void Inject(IFPEventTrigger eventTrigger) {
            this.events = eventTrigger;
        }

    }

}