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
            var targetTCCamera = stateModel.TargetTCCamera;

            // - Position Blending
            var basePosition = baseTCCameraInfo.Position;
            var targetPosition = targetTCCamera.AfterInfo.Position;
            var easingType = stateModel.EasingType;
            var stateTime = stateModel.time;
            var stateDuration = stateModel.Duration;
            var newEndPosition = EasingHelper.Ease3D(easingType, stateTime, stateDuration, basePosition, targetPosition);
            targetTCCamera.AfterInfo.SetPosition(newEndPosition);

            // - Rotation Blending
            var baseEulerAngles = baseTCCameraInfo.Rotation.eulerAngles;
            var targetEulerAngles = targetTCCamera.AfterInfo.Rotation.eulerAngles;
            if (baseEulerAngles.y < 0) baseEulerAngles.y += 360;
            if (targetEulerAngles.y < 0) targetEulerAngles.y += 360;
            if (targetEulerAngles.y - baseEulerAngles.y > 180) {
                baseEulerAngles.y += 360;
            } else if (baseEulerAngles.y - targetEulerAngles.y > 180) {
                targetEulerAngles.y += 360;
            }

            var newEndEulerAngles = EasingHelper.Ease3D(easingType, stateTime, stateDuration, baseEulerAngles, targetEulerAngles);

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

            // ======== Executing ========
            var director = context.directorEntity;
            var cam = context.CameraRepo.CurrentTCCam;
            var afterInfo = cam.AfterInfo;

            var followTargetPos = cam.TargetorModel.FollowTargetPos;
            var normalOffset = cam.FollowComponent.GetNormalOffset();
            var applyPosition = Quaternion.Euler(stateModel.RoundingEulerAngles) * normalOffset + followTargetPos;
            afterInfo.SetPosition(applyPosition);
            afterInfo.SetRotation(Quaternion.LookRotation((followTargetPos - applyPosition).normalized));
            stateModel.ReduceDuration(dt);

            // ======== Exit ========
            if (stateModel.IsOver()) {
                Enter_None();
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

        public bool Enter_Blend(EasingType easingType, float duration, int id) {
            var repo = context.CameraRepo;
            if (!repo.TryGet(id, out var targetTCCam)) {
                return false;
            }

            var director = context.directorEntity;
            var fsmCom = director.FSMComponent;
            var curTCCam = repo.CurrentTCCam;
            if (curTCCam != null && curTCCam != targetTCCam) {
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
            if (addEulerAngles == Vector3.zero) return;

            var director = context.directorEntity;
            var fsmCom = director.FSMComponent;
            var fsmState = fsmCom.FSMState;
            var stateModel = fsmCom.ManualRoundingStateModel;

            if (fsmState != TCDirectorFSMState.ManualRounding) fsmCom.EnterManualRounding(duration);
            else fsmCom.ManualRoundingStateModel.SetDuration(duration);

            stateModel.AddRoundingEulerAngles(addEulerAngles);
            stateModel.SetExitEasingType(exitEasingType);
        }

        void Enter_None() {
            context.directorEntity.FSMComponent.EnterNone();
        }

    }

}