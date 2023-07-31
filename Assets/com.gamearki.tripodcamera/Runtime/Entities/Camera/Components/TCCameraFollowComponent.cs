using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraFollowComponent {

        TCTargetorModel targetorModel;
        public void SetTargetorModel(in TCTargetorModel targetorModel) => this.targetorModel = targetorModel;

        public TCFollowModel model;

        public Vector3 roundOffset;
        public Vector3 physicsRecoilOffset;
        public Vector3 faceLookOffset;
        EasingElement easingElement;

        public TCCameraFollowComponent() {
            easingElement = new EasingElement();
        }

        public void SetEasing(EasingType easingType_horizontal, float duration_horizontal, EasingType easingType_vertical, float duration_vertical) {
            this.model.easingType_horizontal = easingType_horizontal;
            this.model.easingType_vertical = easingType_vertical;
            this.model.duration_horizontal = duration_horizontal;
            this.model.duration_vertical = duration_vertical;
        }

        public void Tick(float dt) {

            if (!targetorModel.HasFollowTarget) {
                return;
            }

            easingElement.TickEasing(dt,
                                     model.easingType_horizontal,
                                     model.duration_horizontal,
                                     model.easingType_vertical,
                                     model.duration_vertical,
                                     GetCameraDstPos());

        }

        public void ResetOffset() {
            if (targetorModel.HasFollowTarget) {
                easingElement.Reset(targetorModel.FollowTargetPos);
            }
            roundOffset = Vector3.zero;
            faceLookOffset = Vector3.zero;
            physicsRecoilOffset = Vector3.zero;
        }

        public void AddNormalFollowOffset(Vector3 offset) {
            if (targetorModel.HasFollowTarget) {
                model.normalFollowOffset += offset;
            }
        }

        public void SetNormalFollowOffset(Vector3 offset) {
            if (offset == Vector3.zero) {
                offset.z = -0.01f;
            }
            this.model.normalFollowOffset = offset;
        }

        public void ChangeXEasing(EasingType easingType, float duration) {
            this.model.easingType_horizontal = easingType;
            this.model.duration_horizontal = duration;
        }

        public void ChangeYEasing(EasingType easingType, float duration) {
            this.model.easingType_vertical = easingType;
            this.model.duration_vertical = duration;
        }

        public Vector3 GetCurrentCameraPos() {
            return easingElement.EaseValue;
        }

        public Vector3 GetCurrentCameraOffset() {
            return easingElement.EaseValue - targetorModel.FollowTargetPos;
        }

        Vector3 GetCameraDstPos() {
            return targetorModel.FollowTargetPos + model.normalFollowOffset + faceLookOffset + physicsRecoilOffset;
        }

    }

}