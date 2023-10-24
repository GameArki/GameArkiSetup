using GameArki.FPEasing;

namespace GameArki.TripodCamera.API {

    public interface IStrategySetterAPI {

        /// <summary>
        /// 设置指定ID相机 最大俯视角度
        /// </summary>
        void SetMaxLookDownDegree(float degree, int id);

        /// <summary>
        /// 设置指定ID相机 最大仰视角度
        /// </summary>
        void SetMaxLookUpDegree(float degree, int id);

        /// <summary>
        /// 启用或关闭指定ID相机的 视角角度限制 
        /// </summary>
        void SetLookLimitActivated(bool activated, int id);

        /// <summary>
        /// 设置指定ID相机 最大位置移动速度
        /// </summary>
        void SetMaxMoveSpeed(float speed, int id);

        /// <summary>
        /// 启用或关闭指定ID相机 最大位置移动限制 
        /// </summary>
        void SetMaxMoveSpeedLimitActivated(bool activated, int id);

        /// <summary>
        /// 开启并设置指定ID相机 自动面向功能参数
        /// </summary>
        void SetAutoFacing(EasingType easingType, float duration, float minAngleDiff, float sameForwardBreakTime, int id);

          /// <summary>
        /// 关闭指定ID相机 自动面向功能
        /// </summary>
        void QuitAutoFacing();

    }

}

