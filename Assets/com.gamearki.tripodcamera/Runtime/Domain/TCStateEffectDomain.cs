using GameArki.FPEasing;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Facades;

namespace GameArki.TripodCamera.Domain {

    public class TCStateEffectDomain {

        TCContext context;

        public TCStateEffectDomain() { }

        public void Inject(TCContext context) {
            this.context = context;
        }

        public void ApplyShake(TCShakeStateModel[] mods, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.ShakeStateComponent.Enter(mods);
        }

        public void ApplyMovement(TCMovementStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.MovementStateCom.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        public void ApplyRound(TCRoundStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;

            tcCam.RoundStateCom.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        public void ApplyRotation(TCRotateStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.RotateStateComponent.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        public void ApplyPush(TCPushStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.PushStateCom.Enter(mods, isExitReset, exitEasing, exitDuration);
        }

        public void ApplyFOV(TCFOVStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration, int id) {
            if (!_TryGetTCCameraByID(id, out var tcCam)) return;
            tcCam.FOVStateComponent.Enter(mods, isExitReset, exitEasing, exitDuration);
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