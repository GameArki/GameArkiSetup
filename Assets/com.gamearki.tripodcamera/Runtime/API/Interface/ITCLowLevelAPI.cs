using UnityEngine;

namespace GameArki.TripodCamera.API {

    /// <summary>
    /// 包括基本的 推/拉、旋转、移动、缩放
    /// </summary>
    public interface ITCLowLevelAPI {

        void Push(float value, int id);

        void Rotate_Roll(float z, int id);

        void Move(Vector2 value, int id);

        void Move_AndChangeLookAtOffset(Vector2 value, int id);

        void Zoom(float value, int id);

    }

}