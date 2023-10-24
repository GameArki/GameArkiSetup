using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Entities;

namespace GameArki.TripodCamera.API {

    public interface ITCLookAtAPI {

        /// <summary>
        /// 启用或关闭指定ID相机的看向功能 
        /// </summary>
        void SetEnabled(bool enabled, int id);

        /// <summary>
        /// 初始化指定ID相机的 #看向点 #看向位置偏移 #缓动函数(水平方向) #缓动时间(水平方向) #缓动函数(垂直方向) #缓动时间(垂直方向)
        /// </summary>
        void SetInit(Transform target,
                     in Vector3 offset,
                     EasingType horizontalEasingType,
                     float horizontalEasingTime,
                     EasingType verticalEasingType,
                     float verticalEasingTime,
                     int id);


        /// <summary>
        /// 设置指定ID相机的 #缓动函数(水平方向) #缓动时间(水平方向) #缓动函数(垂直方向) #缓动时间(垂直方向)
        /// </summary>
        void SetEasing(EasingType horizontalEasingType, float horizontalEasingTime, EasingType verticalEasingType, float verticalEasingTime, int id);

        /// <summary>
        /// 设置指定ID相机的 #看向点
        /// </summary>
        void ChangeTarget(Transform target, int id);

        /// <summary>
        /// 启用或关闭指定ID相机的 正常看向角度
        /// </summary>
        void SetNormalAngles(in Vector3 eulerAngles, int id);

        /// <summary>
        /// 启用或关闭指定ID相机的 正常看向功能
        /// </summary>
        void SetNormalLookActivated(bool activated, int id);

        /// <summary>
        /// 设置指定ID相机看向点的 屏幕死区类型
        /// </summary>
        void SetComposerType(TCLookAtComposerType composerType, int id);

        /// <summary>
        /// 设置指定ID相机看向点的 屏幕死区参数
        /// </summary>
        void SetComposer(in TCLookAtComposerModel composerModel, int id);

        /// <summary>
        /// 启用或关闭指定ID相机 屏幕死区的  正常看向功能
        /// </summary>
        void SetComposerNormalLookActivated(bool activated, int id);

        /// <summary>
        /// 启用或关闭指定ID相机 屏幕死区的 正常看向功能
        /// </summary>
        void SetComposerNormalAngles(in Vector3 eulerAngles, int id);

        /// <summary>
        /// 设置指定ID相机 屏幕死区的 正常看向阻尼
        /// </summary>
        void SetComposerNormalDamping(float damping, int id);

        bool HasTarget(int id);
        TCLookAtComposerType GetComposerType(int id);
        Vector2 GetDeadZoneLT(int id);
        Vector2 GetDeadZoneRB(int id);
        Transform GetTransform(int id);
        Vector3 GetNormalAngle(int id);

    }

}