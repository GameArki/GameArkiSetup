using GameArki.FPEasing;
using GameArki.TripodCamera.Domain;
using UnityEngine;

namespace GameArki.TripodCamera.API {

    public class TCStrategyAPI : ITCStrategyAPI {

        TCStrategyDomain stategyDomain;

        public TCStrategyAPI() { }

        public void Inject(TCStrategyDomain stategyDomain) {
            this.stategyDomain = stategyDomain;
        }

        void ITCStrategyAPI.QuitAutoFacing(int id) {
            stategyDomain.QuitAutoFacing(id);
        }

        void ITCStrategyAPI.SetAutoFacing(EasingType easingType, float duration, float minAngleDiff, float sameForwardBreakTime, int id) {
            stategyDomain.SetAutoFacing(easingType, duration, minAngleDiff, sameForwardBreakTime, id);
        }

        void ITCStrategyAPI.SetLookLimitActivated(bool activated, int id) {
            stategyDomain.SetLookLimitActivated(activated, id);
        }

        void ITCStrategyAPI.SetMaxLookDownDegree(float degree, int id) {
            stategyDomain.SetMaxLookDownDegree(degree, id);
        }

        void ITCStrategyAPI.SetMaxLookUpDegree(float degree, int id) {
            stategyDomain.SetMaxLookUpDegree(degree, id);
        }

        void ITCStrategyAPI.SetMaxMoveSpeed(float speed, int id) {
            stategyDomain.SetMaxMoveSpeed(speed, id);
        }

        void ITCStrategyAPI.SetMaxMoveSpeedLimitActivated(bool activated, int id) {
            stategyDomain.SetMaxMoveSpeedLimitActivated(activated, id);
        }
    }

}