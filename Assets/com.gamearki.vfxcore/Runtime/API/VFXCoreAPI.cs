using UnityEngine;

namespace GameArki.VFX {

    public class VFXCoreAPI : IVFXCoreAPI {

        VFXContext context;
        VFXDomain domains;

        public VFXCoreAPI() { }

        public void Inject(VFXContext context, VFXDomain domains) {
            this.context = context;
            this.domains = domains;
        }

        int IVFXCoreAPI.TryPlayVFX_Default(string clipName, Transform parent) {
            return domains.TryPlayVFX_Default(clipName, parent);
        }

        int IVFXCoreAPI.TryPlayVFX(string clipName, bool isLoop, Transform parent) {
            return domains.TryPlayVFX(clipName, isLoop, parent);
        }

        int IVFXCoreAPI.TryPlayVFX_ManualTick(string clipName, int maintainFrame, Transform parent) {
            return domains.TryPlayVFX_ManualTick(clipName, maintainFrame, parent);
        }

        bool IVFXCoreAPI.TryStopVFX(int vfxID) {
            return domains.TryStopVFX(vfxID);
        }
    }

}