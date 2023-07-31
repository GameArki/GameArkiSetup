using GameArki.PlatformerCamera.Entities;

namespace GameArki.PlatformerCamera.Facades {

    internal static class PFFactory {

        internal static PFCameraEntity CreateCameraEntity() {
            return new PFCameraEntity();
        }

    }

}