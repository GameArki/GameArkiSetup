using UnityEngine;
using GameArki.PlatformerCamera.Facades;
using GameArki.PlatformerCamera.Entities;

namespace GameArki.PlatformerCamera.Phases {

    internal class PFInitPhase {

        AllPFContext ctx;
        AllPFDomain domain;

        internal PFInitPhase() { }

        internal void Inject(AllPFContext ctx, AllPFDomain domain) {
            this.ctx = ctx;
            this.domain = domain;
        }

        internal void Init() {
            
        }

    }

}