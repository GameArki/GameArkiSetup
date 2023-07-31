using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Hook;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.API {

    public interface ITCSetterAPI {

        // ==== Apply ====
        void ApplyCameraTM(in TCCameraTM tm, int id);

        // ==== Spawn ====
        int SpawnTCCamera();
        int SpawnTCCamera(Vector3 position, Quaternion rotation, float fov);
        void RemoveTCCamera(int id);
        bool SetTCCameraActive(bool active, int id);
        bool CutToTCCamera(int id);
        bool BlendToTCCamera(EasingType easingType, float duration, int id);
        TCCameraHook GetHook(int id);

        // ==== Basic ====
        void Push_In(float value, int id);

        void Move(Vector2 value, int id);
        void Move_AndChangeLookAtOffset(Vector2 value, int id);

        void Rotate_Horizontal(float x, int id);
        void Rotate_Vertical(float y, int id);
        void Rotate_Roll(float z, int id);

        void Zoom_In(float value, int id);

        // ==== Follow ====
        void Follow_SetInit(Transform target, Vector3 offset, int id);

        void Follow_SetInit(Transform target, Vector3 offset, EasingType easingType, float easingTime, int id);
        void Follow_SetEasing(EasingType easingType, float easingTime, int id);

        void Follow_SetInit(Transform target, Vector3 offset, EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical, int id);
        void Follow_SetEasing(EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical, int id);

        void Follow_ChangeTarget(Transform target, int id);
        void Follow_ChangeOffset(Vector3 offset, int id);

        void Follow_SetFollowType(TCFollowType followType, int id);

        // ==== Look ====
        void LookAt_SetLookAtEnabled(bool enabled, int id);
        void LookAt_SetInit(Transform target, Vector3 offset, int id);
        void LookAt_SetInit(Transform target, Vector3 offset, EasingType easingType, float easingTime, int id);
        void LookAt_SetInit(Transform target, Vector3 offset, EasingType horizontalEasingType, float horizontalEasingTime, EasingType verticalEasingType, float verticalEasingTime, int id);
        void LookAt_SetEasing(EasingType easingType, float easingTime, int id);
        void LookAt_SetEasing(EasingType horizontalEasingType, float horizontalEasingTime, EasingType verticalEasingType, float verticalEasingTime, int id);
        void LookAt_ChangeTarget(Transform target, int id);
        void LookAt_SetNormalLookActivated(bool activated, int id);
        void LookAt_SetNormalAngles(in Vector3 eulerAngles, int id);
        void LookAt_SetComposerType(TCLookAtComposerType composerType, int id);
        void LookAt_SetComposer(in TCLookAtComposerModel composerModel, int id);
        void LookAt_SetComposerNormalLookActivated(bool activated, int id);
        void LookAt_SetComposerNormalAngles(in Vector3 eulerAngles, int id);
        void LookAt_SetComposerNormalDamping(float damping, int id);

        // ==== Preview ====
        void SetAutoFacing(EasingType easingType, float duration, float minAngleDiff, float sameForwardBreakTime, int id);
        void QuitAutoFacing();

        // ==== State ====
        void Enter_Shake(TCShakeStateModel[] args, int id);
        void Enter_Movement(TCMovementStateModel[] args, bool isExitReset, int id);
        void Enter_Movement(TCMovementStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);
        void Enter_Round(TCRoundStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);
        void Enter_Push(TCPushStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);
        void Enter_Rotation(TCRotateStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);
        void Enter_FOV(TCFOVStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id);

        // ==== MISC ====
        void MISC_SetMaxLookDownDegree(float degree, int id);
        void MISC_SetMaxLookUpDegree(float degree, int id);
        void MISC_SetLookLimitActivated(bool activated, int id);

        void MISC_SetMaxMoveSpeed(float speed, int id);
        void MISC_SetMaxMoveSpeedLimitActivated(bool activated, int id);

    }

}