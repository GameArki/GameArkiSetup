using GameArki.FPEasing;

namespace GameArki.TripodCamera.API {

    public interface ITCDirectorAPI {

        /// <summary>
        /// 由当前相机直接切换到指定ID相机
        /// </summary>
        bool CutToTCCamera(int id);

        /// <summary>
        /// 由当前相机平滑切换到指定ID相机
        /// </summary>
        bool BlendToTCCamera(EasingType easingType, float duration, int id);

    }

}