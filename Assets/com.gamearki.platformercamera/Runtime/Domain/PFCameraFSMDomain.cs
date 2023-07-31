using UnityEngine;
using GameArki.PlatformerCamera.Facades;
using GameArki.PlatformerCamera.Entities;

namespace GameArki.PlatformerCamera.Domain {

    internal class PFCameraFSMDomain {

        AllPFContext ctx;

        internal PFCameraFSMDomain() { }

        internal void Inject(AllPFContext ctx) {
            this.ctx = ctx;
        }

        internal void TickFollow(PFCameraEntity cam, float dt) {
            cam.TickEasing(dt);
        }

    }

}