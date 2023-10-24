using GameArki.FPEasing;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Facades;
using UnityEngine;

namespace GameArki.TripodCamera.Domain {

    public class TCStrategyDomain {

        TCContext context;

        public TCStrategyDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public void SetAutoFacing(EasingType easingType, float duration, float minAngleDiff, float sameForwardBreakTime, int id) {
            var repo = context.CameraRepo;
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            var targeterModel = tcCam.TargetorModel;
            var autoFacingStateComponent = tcCam.AutoFacingStateComponent;
            var camTF = context.MainCamera.transform;
            var followPos = targeterModel.FollowTargetPos;
            followPos.y = 0;
            var camPos = camTF.position;
            camPos.y = 0;
            var xzDis = Vector3.Distance(followPos, camPos);
            autoFacingStateComponent.EnterAutoFacing(targeterModel.FollowTargetForward, camTF.forward, xzDis, easingType, duration, minAngleDiff, sameForwardBreakTime);
        }

        public void QuitAutoFacing(int id) {
            if (!_TryGetTCCameraByID(id, out var cam)) {
                return;
            }

            var autoFacingStateComponent = cam.AutoFacingStateComponent;
            autoFacingStateComponent.QuitAuotFacing();
        }


        public void SetMaxLookDownDegree(float degree, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.maxLookDownDegree = degree;
        }

        public void SetMaxLookUpDegree(float degree, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.maxLookUpDegree = degree;
        }

        public void SetLookLimitActivated(bool activated, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.lookLimitActivated = activated;
        }

        public void SetMaxMoveSpeedLimitActivated(bool activated, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.moveSpeedLimitActivated = activated;
        }

        public void SetMaxMoveSpeed(float speed, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var miscCom = tcCam.MISCComponent;
            miscCom.model.maxMoveSpeed = speed;
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