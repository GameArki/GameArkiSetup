using GameArki.PlatformerCamera.Facades;

namespace GameArki.PlatformerCamera.Phases {

    internal class PFStatePhase {

        AllPFContext ctx;
        AllPFDomain domain;

        internal PFStatePhase() { }

        internal void Inject(AllPFContext ctx, AllPFDomain domain) {
            this.ctx = ctx;
            this.domain = domain;
        }

        internal void Tick(float dt) {

            var cur = ctx.Repo.Current;
            var cameraFSMDomain = domain.CameraFSMDomain;
            cameraFSMDomain.TickFollow(cur, dt);

        }

    }

}