using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Domain;
using GameArki.TripodCamera.Hook;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.API {

    internal class TCSetterAPI : ITCSetterAPI {

        TCContext context;
        TCDomain domain;

        internal TCSetterAPI() { }

        internal void Inject(TCContext context, TCDomain domain) {
            this.context = context;
            this.domain = domain;
        }

        // ==== Spawn ====

        void ITCSetterAPI.ApplyCameraTM(in TCCameraTM tm, int id) {
            domain.ApplyDomain.ApplyCameraTM(tm, id);
        }

        int ITCSetterAPI.SpawnTCCamera() {
            var cam = context.MainCamera;
            var tf = cam.transform;
            return domain.CameraDomain.Spawn(tf.position, tf.rotation, cam.fieldOfView);
        }

        int ITCSetterAPI.SpawnTCCamera(Vector3 position, Quaternion rotation, float fov) {
            return domain.CameraDomain.Spawn(position, rotation, fov);
        }

        void ITCSetterAPI.RemoveTCCamera(int id) {
            domain.CameraDomain.Remove(id);
        }

        bool ITCSetterAPI.SetTCCameraActive(bool active, int id) {
            return domain.CameraDomain.SetTCCameraActive(active, id);
        }

        bool ITCSetterAPI.CutToTCCamera(int id) {
            return domain.DirectorDomain.CutToTCCamera(id);
        }

        bool ITCSetterAPI.BlendToTCCamera(EasingType easingType, float duration, int id) {
            return domain.DirectorDomain.BlendToTCCamera(easingType, duration, id);
        }

        TCCameraHook ITCSetterAPI.GetHook(int id) {
            return domain.CameraDomain.SpawnHook(id);
        }

        // ==== Basic ====
        void ITCSetterAPI.Push_In(float value, int id) {
            domain.CameraDomain.Push_In(value, id);
        }

        void ITCSetterAPI.Move(Vector2 value, int id) {
            domain.CameraDomain.Move(value, id);
        }

        void ITCSetterAPI.Move_AndChangeLookAtOffset(Vector2 value, int id) {
            domain.CameraDomain.Move_AndChangeLookAtOffset(value, id);
        }

        void ITCSetterAPI.Rotate_Horizontal(float x, int id) {
            domain.CameraDomain.Rotate_Horizontal(x, id);
        }

        void ITCSetterAPI.Rotate_Vertical(float y, int id) {
            domain.CameraDomain.Rotate_Vertical(y, id);
        }

        void ITCSetterAPI.Rotate_Roll(float z, int id) {
            domain.CameraDomain.Rotate_Roll(z, id);
        }

        void ITCSetterAPI.Zoom_In(float value, int id) {
            domain.CameraDomain.Zoom_In(value, id);
        }

        // ==== Follow ====
        void ITCSetterAPI.Follow_SetInit(Transform target, Vector3 normalFollowOffset, int id) {
            domain.CameraDomain.Follow_SetInit(target, normalFollowOffset, EasingType.Immediate, 0f, EasingType.Immediate, 0f, id);
        }

        void ITCSetterAPI.Follow_SetInit(Transform target, Vector3 normalFollowOffset, EasingType easingType, float easingTime, int id) {
            domain.CameraDomain.Follow_SetInit(target, normalFollowOffset, easingType, easingTime, easingType, easingTime, id);
        }

        void ITCSetterAPI.Follow_SetEasing(EasingType easingType, float easingTime, int id) {
            domain.CameraDomain.Follow_SetEasing(easingType, easingTime, easingType, easingTime, id);
        }

        void ITCSetterAPI.Follow_SetInit(Transform target,
                                         Vector3 offset,
                                         EasingType easingType_horizontal,
                                         float easingTime_horizontal,
                                         EasingType easingType_vertical,
                                         float easingTime_vertical,
                                         int id) {
            domain.CameraDomain.Follow_SetInit(target, offset, easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical, id);
        }

        void ITCSetterAPI.Follow_SetEasing(EasingType easingType_horizontal,
                                           float easingTime_horizontal,
                                           EasingType easingType_vertical,
                                           float easingTime_vertical,
                                           int id) {
            domain.CameraDomain.Follow_SetEasing(easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical, id);
        }

        void ITCSetterAPI.Follow_ChangeTarget(Transform target, int id) {
            domain.CameraDomain.Follow_ChangeTarget(target, id);
        }

        void ITCSetterAPI.Follow_ChangeOffset(Vector3 offset, int id) {
            domain.CameraDomain.Follow_ChangeOffset(offset, id);
        }

        void ITCSetterAPI.Follow_SetFollowType(TCFollowType followType, int id) {
            domain.CameraDomain.Follow_SetFollowType(followType, id);
        }

        // ==== LookAt ====
        void ITCSetterAPI.LookAt_SetLookAtEnabled(bool flag, int id) {
            domain.CameraDomain.LookAt_SetEnable(flag, id);
        }

        void ITCSetterAPI.LookAt_SetInit(Transform target, Vector3 offset, int id) {
            var easingType = EasingType.Immediate;
            var easingTime = 0f;
            domain.CameraDomain.LookAt_SetInit(target, offset, easingType, easingTime, easingType, easingTime, id);
        }

        void ITCSetterAPI.LookAt_SetNormalLookActivated(bool activated, int id) {
            domain.CameraDomain.LookAt_SetNormalLookActivated(activated, id);
        }

        void ITCSetterAPI.LookAt_SetNormalAngles(in Vector3 eulerAngles, int id) {
            domain.CameraDomain.LookAt_SetNormalAngles(eulerAngles, id);
        }

        void ITCSetterAPI.LookAt_SetInit(Transform target,
                                         Vector3 offset,
                                         EasingType easingType,
                                         float easingTime,
                                         int id) {
            domain.CameraDomain.LookAt_SetInit(target, offset, easingType, easingTime, easingType, easingTime, id);
        }

        void ITCSetterAPI.LookAt_SetEasing(EasingType easingType, float easingTime, int id) {
            domain.CameraDomain.LookAt_SetEasing(easingType, easingTime, easingType, easingTime, id);
        }

        void ITCSetterAPI.LookAt_SetInit(Transform target,
                                         Vector3 offset,
                                         EasingType horizontalEasingType,
                                         float horizontalEasingTime,
                                         EasingType verticalEasingType,
                                         float verticalEasingTime,
                                         int id) {
            domain.CameraDomain.LookAt_SetInit(target, offset, horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime, id);
        }

        void ITCSetterAPI.LookAt_SetEasing(EasingType horizontalEasingType,
                                           float horizontalEasingTime,
                                           EasingType verticalEasingType,
                                           float verticalEasingTime,
                                           int id) {
            domain.CameraDomain.LookAt_SetEasing(horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime, id);
        }

        void ITCSetterAPI.LookAt_ChangeTarget(Transform target, int id) {
            domain.CameraDomain.LookAt_ChangeTarget(target, id);
        }

        void ITCSetterAPI.LookAt_SetComposerNormalLookActivated(bool activated, int id) {
            domain.CameraDomain.LookAt_SetComposerNormalLookActivated(activated, id);
        }

        void ITCSetterAPI.LookAt_SetComposerNormalAngles(in Vector3 eulerAngles, int id) {
            domain.CameraDomain.LookAt_SetComposerNormalAngles(eulerAngles, id);
        }
        void ITCSetterAPI.LookAt_SetComposerNormalDamping(float damping, int id) {
            domain.CameraDomain.LookAt_SetComposerNormalDamping(damping, id);
        }

        void ITCSetterAPI.LookAt_SetComposer(in TCLookAtComposerModel composerModel, int id) {
            domain.CameraDomain.SetLookAtComposer(composerModel, id);
        }

        void ITCSetterAPI.LookAt_SetComposerType(TCLookAtComposerType composerType, int id) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.SetLookAtComposerType(composerType, id);
        }

        // ============ State ========
        // ==== Shake ====
        void ITCSetterAPI.Enter_Shake(TCShakeStateModel[] mods, int id) {
            domain.CameraDomain.Enter_Shake(mods, id);
        }

        // ==== Move ====
        void ITCSetterAPI.Enter_Movement(TCMovementStateModel[] mods, bool isExitReset, int id) {
            domain.CameraDomain.Enter_Movement(mods, isExitReset, default, 0, id);
        }

        void ITCSetterAPI.Enter_Movement(TCMovementStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            domain.CameraDomain.Enter_Movement(mods, isExitReset, exitEasingType, exitDuration, id);
        }

        void ITCSetterAPI.Enter_Round(TCRoundStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            domain.CameraDomain.Enter_Round(mods, isExitReset, exitEasingType, exitDuration, id);
        }

        // ==== Push ====
        void ITCSetterAPI.Enter_Push(TCPushStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            domain.CameraDomain.Enter_Push(mods, isExitReset, exitEasingType, exitDuration, id);
        }

        // ==== Rotation ====
        void ITCSetterAPI.Enter_Rotation(TCRotateStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            domain.CameraDomain.Enter_Rotation(mods, isExitReset, exitEasingType, exitDuration, id);
        }

        // ==== FOV ====
        void ITCSetterAPI.Enter_FOV(TCFOVStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            domain.CameraDomain.Enter_FOV(mods, isExitReset, exitEasingType, exitDuration, id);
        }

        // ==== Auto Facing ====
        void ITCSetterAPI.SetAutoFacing(EasingType easingType, float duration, float minAngleDiff, float sameForwardBreakTime, int id) {
            domain.CameraDomain.SetAutoFacing(easingType, duration, minAngleDiff, sameForwardBreakTime, id);
        }

        void ITCSetterAPI.QuitAutoFacing() {
            var curTCCam = context.CameraRepo.CurrentTCCam;
            if (curTCCam == null) {
                return;
            }
            var autoFacingStateComponent = curTCCam.AutoFacingStateComponent;
            autoFacingStateComponent.QuitAuotFacing();
        }

        // ==== MISC ====
        void ITCSetterAPI.MISC_SetMaxLookDownDegree(float degree, int id) {
            domain.CameraDomain.MISC_SetMaxLookDownDegree(degree, id);
        }

        void ITCSetterAPI.MISC_SetMaxLookUpDegree(float degree, int id) {
            domain.CameraDomain.MISC_SetMaxLookUpDegree(degree, id);
        }

        void ITCSetterAPI.MISC_SetLookLimitActivated(bool activated, int id) {
            domain.CameraDomain.MISC_SetLookLimitActivated(activated, id);
        }

        void ITCSetterAPI.MISC_SetMaxMoveSpeedLimitActivated(bool activated, int id) {
            domain.CameraDomain.MISC_SetMoveSpeedLimitActivated(activated, id);
        }

        void ITCSetterAPI.MISC_SetMaxMoveSpeed(float limit, int id) {
            domain.CameraDomain.MISC_SetMaxMoveSpeed(limit, id);
        }

    }

}