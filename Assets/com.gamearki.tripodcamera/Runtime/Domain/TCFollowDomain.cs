using GameArki.FPEasing;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Facades;
using UnityEngine;

namespace GameArki.TripodCamera.Domain {

    public class TCFollowDomain {

        TCContext context;

        public TCFollowDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public void SetInit(Transform target,
                            in Vector3 offset,
                            EasingType easingType_horizontal,
                            float easingTime_horizontal,
                            EasingType easingType_vertical,
                            float easingTime_vertical,
                            int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Follow_SetInit(target, offset, easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        public void SetEasing(EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Follow_SetEasing(easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        public void ChangeTarget(Transform target, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.Follow_ChangeTarget(target);
        }

        public void ChangeOffset(Vector3 offset, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.Follow_ChangeOffset(offset);
        }

        public void SetFollowType(TCFollowType followType, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            var followCom = tcCam.FollowComponent;
            followCom.model.followType = followType;
        }

        public bool HasTarget(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return false;
            var targeterModel = tcCam.TargetorModel;
            return targeterModel.HasFollowTarget;
        }

        public Transform GetTransform(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return null;
            return tcCam.TargetorModel.FollowTarget;
        }

        public Vector3 GetNormalOffset(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return Vector3.zero;
            return tcCam.FollowComponent.GetNormalOffset();
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
        
        bool _TryGetTCCameraByID(int id, out TCCameraEntity tcCam) {
            var repo = context.CameraRepo;
            if (id == -1) {
                tcCam = repo.CurrentTCCam;
            } else {
                repo.TryGet(id, out tcCam);
            }

            return tcCam != null;
        }
        
    }

}