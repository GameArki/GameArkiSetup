using GameArki.FPEasing;
using GameArki.TripodCamera.Domain;

namespace GameArki.TripodCamera.API {

    public class TCStateEffectAPI : ITCStateEffectAPI {

        TCStateEffectDomain stateEffectDomain;

        public TCStateEffectAPI() { }

        public void Inject(TCStateEffectDomain stateEffectDomain) {
            this.stateEffectDomain = stateEffectDomain;
        }

        void ITCStateEffectAPI.ApplyFOV(TCFOVStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            stateEffectDomain.ApplyFOV(args, isExitReset, exitEasingType, exitDuration, id);
        }

        void ITCStateEffectAPI.ApplyMovement(TCMovementStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            stateEffectDomain.ApplyMovement(args, isExitReset, exitEasingType, exitDuration, id);
        }

        void ITCStateEffectAPI.ApplyPush(TCPushStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            stateEffectDomain.ApplyPush(args, isExitReset, exitEasingType, exitDuration, id);
        }

        void ITCStateEffectAPI.ApplyRotation(TCRotateStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            stateEffectDomain.ApplyRotation(args, isExitReset, exitEasingType, exitDuration, id);
        }

        void ITCStateEffectAPI.ApplyRound(TCRoundStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration, int id) {
            stateEffectDomain.ApplyRound(args, isExitReset, exitEasingType, exitDuration, id);
        }

        void ITCStateEffectAPI.ApplyShake(TCShakeStateModel[] args, int id) {
            stateEffectDomain.ApplyShake(args, id);
        }
    }

}