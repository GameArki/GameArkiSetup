using System.Collections.Generic;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCDirectorFSMComponent {

        TCDirectorFSMState fsmState;
        public TCDirectorFSMState FSMState => fsmState;

        TCDirectorBlendStateModel blendStateModel;
        public TCDirectorBlendStateModel BlendStateModel => blendStateModel;

        public TCDirectorFSMComponent() {
            fsmState = TCDirectorFSMState.None;
            blendStateModel = new TCDirectorBlendStateModel();
        }

        public void EnterNone() {
            fsmState = TCDirectorFSMState.None;
        }

        public void EnterBlend(EasingType easingType, float duration, in TCInfoModel baseTCCameraInfo, TCCameraEntity targetTCCamera) {
            fsmState = TCDirectorFSMState.Blending;
            var stateModel = blendStateModel;
            stateModel.Reset();
            stateModel.SetIsEntering(true);
            stateModel.SetEasingType(easingType);
            stateModel.SetDuration(duration);
            stateModel.SetBaseTCCameraInfo(in baseTCCameraInfo);
            stateModel.SetTargetTCCamera(targetTCCamera);
        }

    }

}