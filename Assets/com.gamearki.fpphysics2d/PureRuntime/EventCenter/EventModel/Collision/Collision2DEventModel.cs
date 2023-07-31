namespace GameArki.FPPhysics2D {

    public struct Collision2DEventModel {

        public FPRigidbody2DEntity other;

        public Collision2DEventModel(FPRigidbody2DEntity other) {
            this.other = other;
        }

    }

}