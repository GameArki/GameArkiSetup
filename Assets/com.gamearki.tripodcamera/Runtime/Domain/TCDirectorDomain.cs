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

        public void TickFSM(float dt) {
            var fsmCom = context.directorEntity.FSMComponent;
            var fsmState = fsmCom.FSMState;
            if (fsmState == TCDirectorFSMState.None) {
                return;
            }

            if (fsmState == TCDirectorFSMState.Blending) {
                _TickFSM_Blending(fsmCom, dt);
            } else if (fsmState == TCDirectorFSMState.ManualRounding) {
                _TickFSM_ManualRounding(fsmCom, dt);
            }
        }

        void _TickFSM_Blending(TCDirectorFSMComponent fsmCom, float dt) {
            var stateModel = fsmCom.BlendStateModel;
            if (stateModel.IsEntering) {
                stateModel.SetIsEnteringFalse();
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
                Enter_None();
            }
        }

        void _TickFSM_ManualRounding(TCDirectorFSMComponent fsmCom, float dt) {
            var stateModel = fsmCom.ManualRoundingStateModel;

            // ======== Entering ========
            if (stateModel.IsEntering) {
                stateModel.SetIsEnteringFalse();
            }

            Vector3 roundEuler = stateModel.RoundingEulerAngles;

            // ======== Exiting ========
            if (stateModel.IsExiting()) {
                stateModel.AddExitTime(dt);
                var exitEasingType = stateModel.ExitEasingType;
                var exitTime = stateModel.ExitTime;
                var exitDuration = stateModel.ExitDuration;
                roundEuler = EasingHelper.Ease3D(exitEasingType, exitTime, exitDuration, roundEuler, Vector3.zero);
            } else {
                stateModel.ReduceDuration(dt);
            }

            // ======== Executing ========
            var director = context.directorEntity;
            var cam = context.CameraRepo.CurrentTCCam;
            var afterInfo = cam.AfterInfo;

            var followTargetPos = cam.TargetorModel.FollowTargetPos;
            var camPosOffset = afterInfo.Position - followTargetPos;
            var applyPosition = Quaternion.Euler(roundEuler) * camPosOffset + followTargetPos;
            afterInfo.SetPosition(applyPosition);
            afterInfo.SetRotation(Quaternion.LookRotation((followTargetPos - applyPosition).normalized));

            // ======== Exit ========
            if (stateModel.IsExitingOver()) {
                Exit_ManualRounding();
                return;
            }
        }

        public bool CutToTCCamera(int id) {
            var repo = context.CameraRepo;
            if (!repo.TryGet(id, out var cam)) {
                return false;
            }

            repo.SetCurrentTCCam(cam);
            return true;
        }

        public bool BlendToTCCamera(EasingType easingType, float duration, int id) {
            var repo = context.CameraRepo;
            if (!repo.TryGet(id, out var targetTCCam)) {
                return false;
            }

            var director = context.directorEntity;
            var fsmCom = director.FSMComponent;
            var curTCCam = repo.CurrentTCCam;
            if (curTCCam != null) {
                fsmCom.EnterBlend(easingType, duration, curTCCam.AfterInfo, targetTCCam);
            }

            repo.SetCurrentTCCam(targetTCCam);

            return true;
        }

        public void ManualRounding_Horizontal(float degreeY, float duration, EasingType exitEasingType, float exitDuration) {
            _ManualRounding(new Vector3(0, degreeY, 0), duration, exitEasingType, exitDuration);
        }

        public void ManualRounding_Vertical(float degreeX, float duration, EasingType exitEasingType, float exitDuration) {
            _ManualRounding(new Vector3(degreeX, 0, 0), duration, exitEasingType, exitDuration);
        }

        void _ManualRounding(in Vector3 addEulerAngles, float duration, EasingType exitEasingType, float exitDuration) {
            var director = context.directorEntity;
            var fsmCom = director.FSMComponent;
            var fsmState = fsmCom.FSMState;
            var stateModel = fsmCom.ManualRoundingStateModel;

            if (fsmState != TCDirectorFSMState.ManualRounding) {
                fsmCom.EnterManualRounding(duration);
            } else {
                fsmCom.ManualRoundingStateModel.SetDuration(duration);
            }

            if (stateModel.IsExiting()) return;

            stateModel.AddRoundingEulerAngles(addEulerAngles);
            stateModel.SetExitEasingType(exitEasingType);
            stateModel.SetExitDuration(exitDuration);
        }

        public void Exit_ManualRounding() {
            var director = context.directorEntity;
            var fsmCom = director.FSMComponent;
            if (fsmCom.FSMState != TCDirectorFSMState.ManualRounding) return;
            Enter_None();
        }

        void Enter_None() {
            context.directorEntity.FSMComponent.EnterNone();
        }

    }

}