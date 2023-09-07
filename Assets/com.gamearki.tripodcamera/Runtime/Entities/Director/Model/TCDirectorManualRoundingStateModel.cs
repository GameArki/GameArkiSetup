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

        float exitDuration;
        public float ExitDuration => exitDuration;
        public void SetExitDuration(float v) => exitDuration = v;

        Vector3 roundingEulerAngles;
        public Vector3 RoundingEulerAngles => roundingEulerAngles;

        float exitTime;
        public float ExitTime => exitTime;
        public void AddExitTime(float v) => exitTime += v;

        public bool IsExiting() => duration <= 0 && exitTime < exitDuration;
        public bool IsExitingOver() => duration <= 0 && exitTime >= exitDuration;

        public void Enter(float duration) {
            this.isEntering = true;
            this.exitTime = 0;
            this.roundingEulerAngles = Vector3.zero;
            this.duration = duration;
        }

        public void AddRoundingEulerAngles(in Vector3 v) {
            roundingEulerAngles += v;
            roundingEulerAngles.x %= 360;
            roundingEulerAngles.y %= 360;
            roundingEulerAngles.z %= 360;
            roundingEulerAngles.x = Mathf.Clamp(roundingEulerAngles.x, -60, 60);
        }

    }

}