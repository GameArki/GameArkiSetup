using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.API {

    /// <summary>
    /// 包括基本的 推/拉、旋转、移动、缩放
    /// </summary>
    public interface ILowLevelSetterAPI {

        void Push_In(float value, int id);

        void Rotate_Roll(float z, int id);

        void Move(Vector2 value, int id);

        void Move_AndChangeLookAtOffset(Vector2 value, int id);

        void Zoom_In(float value, int id);

    }

}