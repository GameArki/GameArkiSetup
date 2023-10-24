using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.API {

    public interface ITCStateEffectAPI {

        /// <summary>
        /// 设置指定ID相机的状态效果: 相机震屏
        /// </summary>
        void ApplyShake(TCShakeStateModel[] args, int id);

        /// <summary>
        /// 设置指定ID相机的状态效果: 根据世界坐标系 平移相机
        /// </summary>
        void ApplyMovement(TCMovementStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);

        /// <summary>
        /// 设置指定ID相机的状态效果: 以跟随点为球心 进行球面上的平移运动
        /// </summary>
        void ApplyRound(TCRoundStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);

        /// <summary>
        /// 设置指定ID相机的状态效果: 根据相机的前方 平移相机
        /// </summary>
        void ApplyPush(TCPushStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);

        /// <summary>
        /// 设置指定ID相机的状态效果: 基于相机自身坐标系 旋转相机
        /// </summary>
        void ApplyRotation(TCRotateStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);

        /// <summary>
        /// 设置指定ID相机的状态效果: 相机FOV变化
        /// </summary>
        void ApplyFOV(TCFOVStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);

    }

}