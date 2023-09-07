using System.Collections.Generic;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCDirectorFSMComponent {

        TCDirectorFSMState fsmState;
        public TCDirectorFSMState FSMState => fsmState;

        TCDirectorFSMState lastFSMState;
        public TCDirectorFSMState LastFSMState => lastFSMState;

        TCDirectorBlendStateModel blendStateModel;
        public TCDirectorBlendStateModel BlendStateModel => blendStateModel;

        TCDirectorManualRoundingStateModel manualRoundingStateModel;
        public TCDirectorManualRoundingStateModel ManualRoundingStateModel => manualRoundingStateModel;

        public TCDirectorFSMComponent() {
            fsmState = TCDirectorFSMState.None;
            blendStateModel = new TCDirectorBlendStateModel();
            manualRoundingStateModel = new TCDirectorManualRoundingStateModel();
        }

        public void EnterNone() {
            lastFSMState = fsmState;
            fsmState = TCDirectorFSMState.None;
        }

        public void EnterBlend(EasingType easingType, float duration, in TCInfoModel baseTCCameraInfo, TCCameraEntity targetTCCamera) {
            var stateModel = blendStateModel;
            stateModel.Enter(easingType, duration, baseTCCameraInfo, targetTCCamera);
            lastFSMState = fsmState;
            fsmState = TCDirectorFSMState.Blending;
        }

        public void EnterManualRounding(float duration) {
            var stateModel = manualRoundingStateModel;
            stateModel.Enter(duration);
            lastFSMState = fsmState;
            fsmState = TCDirectorFSMState.ManualRounding;
        }

    }

}