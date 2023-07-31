using UnityEngine;
using GameArki.PlatformerCamera.Facades;
using GameArki.PlatformerCamera.Entities;

namespace GameArki.PlatformerCamera.Phases {

    internal class PFApplyPhase {

        AllPFContext ctx;
        AllPFDomain domain;

        internal PFApplyPhase() { }

        internal void Inject(AllPFContext ctx, AllPFDomain domain) {
            this.ctx = ctx;
            this.domain = domain;
        }

        internal void Tick(float dt) {

            var curCam = ctx.Repo.Current;
            if (curCam == null) {
                return;
            }

            var applyDomain = domain.ApplyDomain;
            applyDomain.Apply(curCam, ctx.MainCam, dt);


        }

    }

}