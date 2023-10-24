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

        public void SetEnabled(bool flag, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetLookAtEnable(flag);
        }

        public void SetInit(Transform target,
                            in Vector3 offset,
                            EasingType horizontalEasingType,
                            float horizontalEasingTime,
                            EasingType verticalEasingType,
                            float verticalEasingTime,
                            int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_SetInit(target, offset, horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void SetNormalLookActivated(bool flag, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.model.normalLookActivated = flag;
        }

        public void SetNormalAngles(in Vector3 eulerAngles, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetNormalLookAngles(eulerAngles);
        }

        public void SetEasing(EasingType horizontalEasingType,
                                     float horizontalEasingTime,
                                     EasingType verticalEasingType,
                                     float verticalEasingTime,
                                     int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAt_SetEasing(horizontalEasingType, horizontalEasingTime, verticalEasingType, verticalEasingTime);
        }

        public void ChangeTarget(Transform target, int id) {
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

        public void SetComposerNormalLookActivated(bool flag, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAtComponent.SetComposerNormalLookActivated(flag);
        }

        public void SetComposerNormalAngles(in Vector3 angles, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.LookAtComponent.SetComposerNormalLookAngles(angles);
        }

        public void SetComposerNormalDamping(float damping, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            var lookAtComponent = tcCam.LookAtComponent;
            lookAtComponent.SetComposerNormalDamping(damping);
        }

        public Vector3 GetNormalAngle(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return Vector3.zero;
            }

            return tcCam.LookAtComponent.model.normalLookAngles;
        }

        public Transform GetTransform(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return null;
            }

            return tcCam.TargetorModel.LookAtTarget;
        }

        public bool HasTarget(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return false;
            }

            return tcCam.TargetorModel.LookAtTarget != null;
        }

        public void SetComposer(TCLookAtComposerModel composer, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            composer.deadZoneNormalizedW = composer.deadZoneNormalizedW < 0 ? 0 : composer.deadZoneNormalizedW;
            composer.deadZoneNormalizedH = composer.deadZoneNormalizedH < 0 ? 0 : composer.deadZoneNormalizedH;
            composer.softZoneNormalizedW = composer.softZoneNormalizedW < 0 ? 0 : composer.softZoneNormalizedW;
            composer.softZoneNormalizedH = composer.softZoneNormalizedH < 0 ? 0 : composer.softZoneNormalizedH;
            tcCam.LookAtComponent.SetComposerModel(composer);
        }

        public void SetComposerType(TCLookAtComposerType composerType, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            var targeterModel = tcCam.TargetorModel;
            if (composerType == TCLookAtComposerType.LookAtTarget && !targeterModel.HasLookAtTarget) {
                Debug.LogWarning("SetLookAtComposerType: LookAtTarget but no lookAtTarget");
                return;
            }
            if (!targeterModel.HasLookAtTarget || !targeterModel.HasFollowTarget) {
                Debug.LogWarning("SetLookAtComposerType: LookAtAndFollowTarget but no lookAtTarget or followTarget");
                return;
            }

            tcCam.LookAtComponent.SetComposerType(composerType);

            //- Reset 
            var followCom = tcCam.FollowComponent;
            followCom.ResetOffset();

            //- Reset 
            var infoCom = tcCam.BeforeInfo;
            infoCom.SetRotation(Quaternion.identity);
        }

        public TCLookAtComposerType GetComposerType(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return TCLookAtComposerType.None;
            }

            return tcCam.LookAtComponent.model.composerModel.composerType;
        }

        public Vector2 GetDeadZoneLT(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return Vector2.zero;
            }

            return tcCam.LookAtComponent.model.composerModel.GetDeadZoneLT(tcCam.AfterInfo.ScreenWidth, tcCam.AfterInfo.ScreenHeight);
        }

        public Vector2 GetDeadZoneRB(int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) {
                return Vector2.zero;
            }

            return tcCam.LookAtComponent.model.composerModel.GetDeadZoneRB(tcCam.AfterInfo.ScreenWidth, tcCam.AfterInfo.ScreenHeight);
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