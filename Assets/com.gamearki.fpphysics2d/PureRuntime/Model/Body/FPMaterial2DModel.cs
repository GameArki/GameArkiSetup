using FixMath.NET;

namespace GameArki.FPPhysics2D {

    public class FPMaterial2DModel {

        FP64 frictionPercent;
        public FP64 FrictionPercent => frictionPercent;

        FP64 bouncinessPercent;
        public FP64 BouncinessPercent => bouncinessPercent;

        public FPMaterial2DModel() {
            this.frictionPercent = 0;
            this.bouncinessPercent = 0;
        }

    }

}