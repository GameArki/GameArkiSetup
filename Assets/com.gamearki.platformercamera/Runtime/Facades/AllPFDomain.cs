using GameArki.PlatformerCamera.Domain;

namespace GameArki.PlatformerCamera.Facades {

    internal class AllPFDomain {

        PFApplyDomain applyDomain;
        internal PFApplyDomain ApplyDomain => applyDomain;

        PFCameraDomain cameraDomain;
        internal PFCameraDomain CameraDomain => cameraDomain;

        PFCameraFSMDomain cameraFSMDomain;
        internal PFCameraFSMDomain CameraFSMDomain => cameraFSMDomain;

        internal AllPFDomain() {
            this.applyDomain = new PFApplyDomain();
            this.cameraDomain = new PFCameraDomain();
            this.cameraFSMDomain = new PFCameraFSMDomain();
        }

        internal void Inject(AllPFContext ctx) {
            applyDomain.Inject(ctx);
            cameraDomain.Inject(ctx);
            cameraFSMDomain.Inject(ctx);
        }

    }

}