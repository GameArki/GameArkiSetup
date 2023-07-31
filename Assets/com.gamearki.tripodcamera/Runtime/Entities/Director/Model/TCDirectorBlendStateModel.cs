using System.Collections.Generic;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCDirectorBlendStateModel {

        bool isEntering;
        public bool IsEntering => isEntering;
        public void SetIsEntering(bool value) => isEntering = value;

        EasingType easingType;
        public EasingType EasingType => easingType;
        public void SetEasingType(EasingType value) => easingType = value;

        float duration;
        public float Duration => duration;
        public void SetDuration(float value) => duration = value;

        TCInfoModel baseTCCameraInfo;
        public TCInfoModel BaseTCCameraInfo => baseTCCameraInfo;
        public void SetBaseTCCameraInfo(in TCInfoModel value) => baseTCCameraInfo = value;

        TCCameraEntity targetTCCamera;
        public TCCameraEntity TargetTCCamera => targetTCCamera;
        public void SetTargetTCCamera(TCCameraEntity value) => targetTCCamera = value;

        public float time;

        public void Reset() {
            this.isEntering = false;
            this.easingType = EasingType.Immediate;
            this.duration = 0;
            this.baseTCCameraInfo = default;
            this.targetTCCamera = null;
            this.time = 0;
        }

    }

}