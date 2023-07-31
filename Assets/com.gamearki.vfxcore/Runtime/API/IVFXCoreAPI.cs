using UnityEngine;

namespace GameArki.VFX {

    public interface IVFXCoreAPI {

        /// <summary>
        /// Try to play an vfx with a given name. If the vfx is not found, return -1.
        /// </summary>
        int TryPlayVFX_Default(string clipName, Transform parent);

        /// <summary>
        /// Try to play an vfx with a given name and set loop. If the vfx is not found, return -1.
        /// </summary>
        int TryPlayVFX(string clipName, bool isLoop, Transform parent);

        /// <summary>
        /// Try to play an vfx and stop it after a given frame. If the vfx is not found, return -1.
        /// </summary>
        int TryPlayVFX_ManualTick(string clipName, int maintainFrame, Transform parent);

        bool TryStopVFX(int vfxID);

    }

}