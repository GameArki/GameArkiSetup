namespace GameArki.FPPhysics2D.API {

    public struct TriggerEventArgs {

        public FPRigidbody2DEntity a;
        public FPRigidbody2DEntity b;

        public TriggerEventArgs(FPRigidbody2DEntity a, FPRigidbody2DEntity b) {
            this.a = a;
            this.b = b;
        }

    }

}