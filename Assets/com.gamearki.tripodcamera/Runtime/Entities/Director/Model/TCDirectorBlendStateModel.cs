using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCDirectorBlendStateModel {

        bool isEntering;
        public bool IsEntering => isEntering;
        public void SetIsEnteringFalse() => isEntering = false;

        EasingType easingType;
        public EasingType EasingType => easingType;

        float duration;
        public float Duration => duration;

        TCInfoModel baseTCCameraInfo;
        public TCInfoModel BaseTCCameraInfo => baseTCCameraInfo;

        TCCameraEntity targetTCCamera;
        public TCCameraEntity TargetTCCamera => targetTCCamera;

        public float time;

        public void Enter(EasingType easingType, float duration, in TCInfoModel baseTCCameraInfo, TCCameraEntity targetTCCamera) {
            this.isEntering = true;
            this.easingType = easingType;
            this.duration = duration;
            this.baseTCCameraInfo = baseTCCameraInfo;
            this.targetTCCamera = targetTCCamera;
            this.time = 0;
        }

    }

}