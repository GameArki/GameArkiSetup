namespace GameArki.FPPhysics2D.API {

    public struct CollisionEventArgs {

        public FPRigidbody2DEntity a;
        public FPRigidbody2DEntity b;

        public CollisionEventArgs(FPRigidbody2DEntity a, FPRigidbody2DEntity b) {
            this.a = a;
            this.b = b;
        }

    }

}