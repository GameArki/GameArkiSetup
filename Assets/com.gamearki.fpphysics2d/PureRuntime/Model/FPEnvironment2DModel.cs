using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public class FPEnvironment2DModel {

        FPVector2 gravity;
        public FPVector2 Gravity => gravity;

        public FPEnvironment2DModel() {}

        public void SetGravity(FPVector2 gravity) {
            this.gravity = gravity;
        }
        
    }
}