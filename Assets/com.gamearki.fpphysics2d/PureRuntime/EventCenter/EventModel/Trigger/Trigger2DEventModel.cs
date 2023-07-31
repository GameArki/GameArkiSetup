namespace GameArki.FPPhysics2D {

    public struct Trigger2DEventModel {

        public FPRigidbody2DEntity other;

        public Trigger2DEventModel(FPRigidbody2DEntity other) {
            this.other = other;
        }

    }

}