using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCDirectorManualRoundingStateModel {

        bool isEntering;
        public bool IsEntering => isEntering;
        public void SetIsEnteringFalse() => isEntering = false;

        float duration;
        public void SetDuration(float v) => duration = v;
        public void ReduceDuration(float v) => duration -= v;

        EasingType exitEasingType;
        public EasingType ExitEasingType => exitEasingType;
        public void SetExitEasingType(EasingType v) => exitEasingType = v;

        Vector3 roundingEulerAngles;
        public Vector3 RoundingEulerAngles => roundingEulerAngles;

        public bool IsOver() => duration <= 0;

        public void Enter(float duration) {
            this.isEntering = true;
            this.roundingEulerAngles = Vector3.zero;
            this.duration = duration;
        }

        public void AddRoundingEulerAngles(in Vector3 v) {
            roundingEulerAngles += v;
            roundingEulerAngles.x = Mathf.Clamp(roundingEulerAngles.x, -60, 60);
            roundingEulerAngles.y %= 360;
            roundingEulerAngles.z = 0;
        }

    }

}