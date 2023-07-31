namespace GameArki.FPPhysics2D {

    public struct CollisionContact2DModel {

        public ulong key;

        FPRigidbody2DEntity a;
        public FPRigidbody2DEntity A => a;

        FPRigidbody2DEntity b;
        public FPRigidbody2DEntity B => b;

        public CollisionContact2DModel(ulong key, FPRigidbody2DEntity a, FPRigidbody2DEntity b) {
            this.key = key;
            this.a = a;
            this.b = b;
        }


    }

}