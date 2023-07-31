using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Entities;

namespace GameArki.TripodCamera.Domain {

    public class TCDirectorDomain {

        TCContext context;

        public TCDirectorDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public bool CutToTCCamera(int id) {
            var repo = context.CameraRepo;
            if (!repo.TryGet(id, out var cam)) {
                Debug.LogError($"[TCCameraDomain] SwithToTCCamera: Failed to get camera from repo. ID: {id}");
                return false;
            }

            repo.SetCurrentTCCam(cam);
            return true;
        }

        public bool BlendToTCCamera(EasingType easingType, float duration, int id) {
            var repo = context.CameraRepo;
            if (!repo.TryGet(id, out var targetTCCam)) {
                Debug.LogError($"[TCCameraDomain] BlendToTCCamera: Failed to get camera from repo. ID: {id}");
                return false;
            }

            var directorEntity = context.directorEntity;
            var directorFSMComponent = directorEntity.FSMComponent;
            var curTCCam = repo.CurrentTCCam;
            if (curTCCam != null) {
                directorFSMComponent.EnterBlend(easingType, duration, curTCCam.AfterInfo, targetTCCam);
            }

            repo.SetCurrentTCCam(targetTCCam);

            return true;
        }

        public void ApplyFSM(float dt) {
            var fsmCom = context.directorEntity.FSMComponent;
            var fsmState = fsmCom.FSMState;
            if (fsmState == TCDirectorFSMState.None) {
                return;
            }

            if (fsmState == TCDirectorFSMState.Blending) {
                ApplyFSM_Blending(fsmCom, dt);
            }
        }

        void ApplyFSM_Blending(TCDirectorFSMComponent fsmCom, float dt) {
            var stateModel = fsmCom.BlendStateModel;
            if (stateModel.IsEntering) {
                stateModel.SetIsEntering(false);
            }

            stateModel.time += dt;
            var baseTCCameraInfo = stateModel.BaseTCCameraInfo;

            // - Position Blending
            var basePosition = baseTCCameraInfo.Position;
            var targetTCCamera = stateModel.TargetTCCamera;
            var targetPosition = targetTCCamera.AfterInfo.Position;
            var easingType = stateModel.EasingType;
            var stateTime = stateModel.time;
            var stateDuration = stateModel.Duration;
            float px = EasingHelper.Ease1D(easingType, stateTime, stateDuration, basePosition.x, targetPosition.x);
            float py = EasingHelper.Ease1D(easingType, stateTime, stateDuration, basePosition.y, targetPosition.y);
            float pz = EasingHelper.Ease1D(easingType, stateTime, stateDuration, basePosition.z, targetPosition.z);
            var newEndPosition = new Vector3(px, py, pz);
            targetTCCamera.AfterInfo.SetPosition(newEndPosition);

            // - Rotation Blending
            var baseEulerAngles = baseTCCameraInfo.Rotation.eulerAngles;
            var targetEulerAngles = targetTCCamera.AfterInfo.Rotation.eulerAngles;
            float rx = EasingHelper.Ease1D(easingType, stateTime, stateDuration, baseEulerAngles.x, targetEulerAngles.x);
            float ry = EasingHelper.Ease1D(easingType, stateTime, stateDuration, baseEulerAngles.y, targetEulerAngles.y);
            float rz = EasingHelper.Ease1D(easingType, stateTime, stateDuration, baseEulerAngles.z, targetEulerAngles.z);
            var newEndEulerAngles = new Vector3(rx, ry, rz);
            targetTCCamera.AfterInfo.SetRotation(Quaternion.Euler(newEndEulerAngles));

            if (stateTime >= stateDuration) {
                EnterFSM_None();
            }
        }

        void EnterFSM_None() {
            context.directorEntity.FSMComponent.EnterNone();
        }

    }

}