using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraEntity {

        int id;
        public int ID => id;
        public void SetID(int value) => id = value;

        bool activated;
        public bool Activated => activated;
        public void SetActivated(bool value) => activated = value;

        public void SetPosition(in Vector3 pos) => beforeInfo.SetPosition(pos);
        public void SetRotation(in Quaternion rot) => beforeInfo.SetRotation(rot);

        // ==== Info ====
        TCInfoModel beforeInfo; // 一帧开始时的相机信息
        public TCInfoModel BeforeInfo => beforeInfo;

        TCInfoModel afterInfo; // 一帧中相机信息的变化(始终是最新的)
        public TCInfoModel AfterInfo => afterInfo;

        TCTargetorModel targetorModel;
        public TCTargetorModel TargetorModel => targetorModel;

        // ==== Normal ====
        // - Follow
        TCCameraFollowComponent followComponent;
        public TCCameraFollowComponent FollowComponent => followComponent;

        // - LookAt
        TCCameraLookAtComponent lookAtComponent;
        public TCCameraLookAtComponent LookAtComponent => lookAtComponent;

        // ==== State ====
        // - Track State
        TCCameraDollyTrackStateComponent dollyTrackStateComponent;
        public TCCameraDollyTrackStateComponent DollyTrackStateComponent => dollyTrackStateComponent;

        // - Shake State
        TCCameraShakeStateComponent shakeStateComponent;
        public TCCameraShakeStateComponent ShakeStateComponent => shakeStateComponent;

        // - Move State
        TCCameraMovementStateComponent movementStateComponent;
        public TCCameraMovementStateComponent MovementStateComponent => movementStateComponent;

        TCCameraRoundStateComponent roundStateComponent;
        public TCCameraRoundStateComponent RoundStateComponent => roundStateComponent;

        // - Rotate State
        TCCameraRotateStateComponent rotateStateComponent;
        public TCCameraRotateStateComponent RotateStateComponent => rotateStateComponent;

        // - Push State
        TCCameraPushStateComponent pushStateComponent;
        public TCCameraPushStateComponent PushStateComponent => pushStateComponent;

        // - Auto Facing State
        TCCameraAutoFacingStateComponent autoFacingStateComponent;
        public TCCameraAutoFacingStateComponent AutoFacingStateComponent => autoFacingStateComponent;

        // - FOV State
        TCCameraFOVStateComponent fovStateComponent;
        public TCCameraFOVStateComponent FOVStateComponent => fovStateComponent;

        // ==== MISC ====
        TCCameraMISCComponent miscComponent;
        public TCCameraMISCComponent MISCComponent => miscComponent;

        public TCCameraEntity() {
            this.beforeInfo = new TCInfoModel();
            this.afterInfo = new TCInfoModel();
            this.targetorModel = new TCTargetorModel();
            this.followComponent = new TCCameraFollowComponent();
            this.lookAtComponent = new TCCameraLookAtComponent();

            this.dollyTrackStateComponent = new TCCameraDollyTrackStateComponent();
            this.shakeStateComponent = new TCCameraShakeStateComponent();
            this.movementStateComponent = new TCCameraMovementStateComponent();
            this.roundStateComponent = new TCCameraRoundStateComponent();

            this.rotateStateComponent = new TCCameraRotateStateComponent();
            this.pushStateComponent = new TCCameraPushStateComponent();
            this.autoFacingStateComponent = new TCCameraAutoFacingStateComponent();
            this.fovStateComponent = new TCCameraFOVStateComponent();

            this.miscComponent = new TCCameraMISCComponent();

            followComponent.SetTargetorModel(targetorModel);
            dollyTrackStateComponent.SetTargetorModel(targetorModel);
            roundStateComponent.SetTargetorModel(targetorModel);
            lookAtComponent.SetTargetorModel(targetorModel);
        }

        // ==== Info ====
        public void InitInfo(Vector3 pos, Quaternion rot, float fov, float aspect, float nearClipPlane, float farClipPlane, int screenWidth, int screenHeight) {
            beforeInfo.Init(pos, rot, fov, aspect, nearClipPlane, farClipPlane, screenWidth, screenHeight);
            beforeInfo.SetFOVRange(new Vector2(10, 90));
        }

        public void CopyBeforeInfo2AfterInfo() {
            afterInfo.CopyFrom(beforeInfo);
        }

        public void CopyAfterInfo2BeforeInfo() {
            beforeInfo.CopyFrom(afterInfo);
        }

        // ==== Basic ====
        // - Move
        public void Move(Vector2 value) {
            var pos = beforeInfo.Position;
            var rot = beforeInfo.Rotation;
            Vector3 up = rot * Vector3.up;
            Vector3 right = rot * Vector3.right;
            up.Normalize();
            right.Normalize();
            up *= value.y;
            right *= value.x;
            pos += right + up;

            beforeInfo.SetPosition(pos);
        }

        public void Move_AndChangeLookAtOffset(Vector2 value) {
            var pos = beforeInfo.Position;
            var rot = beforeInfo.Rotation;
            Vector3 up;
            Vector3 right;
            up = rot * Vector3.up;
            right = rot * Vector3.right;
            up.Normalize();
            right.Normalize();
            right = right * value.x;
            up = up * value.y;
            pos += right + up;

            beforeInfo.SetPosition(pos);

            // - LookAt Component
            lookAtComponent.OffsetAdd(right + up);

        }

        // ==== Advance ====
        // - Follow
        public void Follow_SetInit(Transform target,
                                   Vector3 normalFollowOffset,
                                   EasingType easingType_horizontal,
                                   float easingTime_horizontal,
                                   EasingType easingType_vertical,
                                   float easingTime_vertical) {
            targetorModel.SetFollowTarget(target);
            followComponent.SetNormalFollowOffset(normalFollowOffset);
            followComponent.SetEasing(easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        public void Follow_SetEasing(EasingType easingType_horizontal,
                                     float easingTime_horizontal,
                                     EasingType easingType_vertical,
                                     float easingTime_vertical) {
            followComponent.SetEasing(easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        public void Follow_ChangeTarget(Transform target) {
            targetorModel.SetFollowTarget(target);
        }

        public void Follow_ChangeXEasing(EasingType easingType, float easingTime) {
            followComponent.ChangeXEasing(easingType, easingTime);
        }

        public void Follow_ChangeYEasing(EasingType easingType, float easingTime) {
            followComponent.ChangeYEasing(easingType, easingTime);
        }

        public void Follow_ChangeOffset(Vector3 offset) {
            followComponent.SetNormalFollowOffset(offset);
        }

        public bool IsFollowing() {
            return targetorModel.HasFollowTarget;
        }

        public bool IsLookingAt() {
            return targetorModel.HasLookAtTarget;
        }

        // - LookAt
        public void LookAt_SetInit(Transform target,
                                   Vector3 lookAtTargetOffset,
                                   EasingType horizontalEasingType,
                                   float horizontalEasingTime,
                                   EasingType verticalEasingType,
                                   float verticalEasingTime) {
            targetorModel.SetLookAtTarget(target);
            lookAtComponent.model.lookAtTargetOffset = lookAtTargetOffset;
            lookAtComponent.SetLookAtEnable(true);
            lookAtComponent.SetEasing(horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void LookAt_SetEasing(EasingType horizontalEasingType,
                                     float horizontalEasingTime,
                                     EasingType verticalEasingType,
                                     float verticalEasingTime) {
            lookAtComponent.SetEasing(horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void LookAt_ChangeTarget(Transform target) {
            targetorModel.SetLookAtTarget(target);
        }

    }

}