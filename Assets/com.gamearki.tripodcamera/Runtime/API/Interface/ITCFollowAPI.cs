using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.API {

    public interface ITCFollowAPI {

        /// <summary>
        /// 初始化指定ID相机的 #跟随点 #跟随位置偏移 #缓动函数(水平方向) #缓动时间(水平方向) #缓动函数(垂直方向) #缓动时间(垂直方向)
        /// </summary>
        void SetInit(Transform target,
                            in Vector3 offset,
                            EasingType easingType_horizontal,
                            float easingTime_horizontal,
                            EasingType easingType_vertical,
                            float easingTime_vertical,
                            int id);

        /// <summary>
        /// 设置指定ID相机的 #缓动函数(水平方向) #缓动时间(水平方向) #缓动函数(垂直方向) #缓动时间(垂直方向)
        /// </summary>
        void SetEasing(EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical, int id);

        /// <summary>
        /// 设置指定ID相机的 #跟随点
        /// </summary>
        void ChangeTarget(Transform target, int id);

        /// <summary>
        /// 设置指定ID相机的 #跟随位置偏移
        /// </summary>
        void ChangeOffset(in Vector3 offset, int id);

        /// <summary>
        /// 设置指定ID相机的 #跟随类型
        /// </summary>
        void SetFollowType(TCFollowType followType, int id);

        /// <summary>
        /// 对指定ID相机 以跟随点为圆心 在水平面上 进行圆周运动
        /// </summary>
        void ManualRounding_Horizontal(float degreeY, float duration, EasingType exitEasingType, float exitDuration);

        /// <summary>
        /// 对指定ID相机 以跟随点为圆心 在垂直平面上 进行圆周运动
        /// </summary>
        void ManualRounding_Vertical(float degreeX, float duration, EasingType exitEasingType, float exitDuration);

        bool HasTarget(int id);
        Vector3 GetNormalOffset(int id);
        Transform GetTransform(int id);

    }

}