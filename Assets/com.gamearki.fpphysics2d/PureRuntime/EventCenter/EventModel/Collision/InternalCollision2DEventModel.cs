namespace GameArki.FPPhysics2D {

    internal struct InternalCollision2DEventModel {

        FPRigidbody2DEntity a;
        public FPRigidbody2DEntity A => a;

        FPRigidbody2DEntity b;
        public FPRigidbody2DEntity B => b;

        internal InternalCollision2DEventModel(FPRigidbody2DEntity a, FPRigidbody2DEntity b) {
            this.a = a;
            this.b = b;
        }

    }

}