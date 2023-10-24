using GameArki.FPEasing;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Facades;
using UnityEngine;

namespace GameArki.TripodCamera.Domain {

    public class TCLookAtDomain {

        TCContext context;

        public TCLookAtDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public void LookAt_SetEnable(bool flag, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetLookAtEnable(flag);
        }

        public void LookAt_SetInit(Transform target,
                                   in Vector3 offset,
                                   EasingType horizontalEasingType,
                                   float horizontalEasingTime,
                                   EasingType verticalEasingType,
                                   float verticalEasingTime,
                                   int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_SetInit(target, offset, horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void LookAt_SetNormalLookActivated(bool flag, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.model.normalLookActivated = flag;
        }

        public void LookAt_SetNormalAngles(in Vector3 eulerAngles, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetNormalLookAngles(eulerAngles);
        }

        public void LookAt_SetEasing(EasingType horizontalEasingType,
                                     float horizontalEasingTime,
                                     EasingType verticalEasingType,
                                     float verticalEasingTime,
                                     int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_SetEasing(horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void LookAt_ChangeTarget(Transform target, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_ChangeTarget(target);

            if (target == null) {
                var composerModel = tcCam.LookAtComponent.model.composerModel;
                var composerType = composerModel.composerType;
                if (composerType == TCLookAtComposerType.LookAtTarget) {
                    composerModel.composerType = TCLookAtComposerType.None;
                }
            }
        }

        public void LookAt_SetComposerNormalLookActivated(bool flag, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAtComponent.SetComposerNormalLookActivated(flag);
        }

        public void LookAt_SetComposerNormalAngles(in Vector3 angles, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAtComponent.SetComposerNormalLookAngles(angles);
        }

        public void LookAt_SetComposerNormalDamping(float damping, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetComposerNormalDamping(damping);
        }

        public Transform LookAt_GetTransform(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return null;
            }

            return tcCam.TargetorModel.LookAtTarget;
        }

        public Vector3 LookAt_GetNormalAngle(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return Vector3.zero;
            }

            return tcCam.LookAtComponent.model.normalLookAngles;
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