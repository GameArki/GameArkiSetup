using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.API {

    public interface ITCCameraAPI {

        /// <summary>
        /// 生成一个相机实体, 并初始化 位置 旋转 FOV
        /// </summary>
        int Spawn(in Vector3 position, in Quaternion rotation, float fov);

        /// <summary>
        /// 移除一个相机实体
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// 该接口可应用相机的所有配置参数(包括状态效果)
        /// </summary>
        void ApplyTM(in TCCameraTM tm, int id);

        /// <summary>
        /// 激活或关闭指定ID相机的更新逻辑
        /// </summary>
        bool SetActive(bool active, int id);

        /// <summary>
        /// 直接设置指定ID相机的位置
        /// </summary>
        void SetPosition(in Vector3 pos, int id);

        /// <summary>
        /// 直接设置指定ID相机的旋转
        /// </summary>
        void SetRotation(in Quaternion rot, int id);

    }

}