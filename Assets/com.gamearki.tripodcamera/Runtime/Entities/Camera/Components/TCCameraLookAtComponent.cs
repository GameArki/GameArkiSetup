using GameArki.FPEasing;
using UnityEngine;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraLookAtComponent {

        TCTargetorModel targetorModel;
        public void SetTargetorModel(TCTargetorModel targetorModel) => this.targetorModel = targetorModel;

        bool isLookAtEnabled;
        public void SetLookAtEnable(bool enable) => this.isLookAtEnabled = enable;

        public TCLookAtModel model;

        //- Composer
        public void SetComposerModel(in TCLookAtComposerModel model) => this.model.composerModel = model;
        public void SetComposerType(TCLookAtComposerType composerType) => model.composerModel.composerType = composerType;
        public void SetComposerNormalDamping(float damping) => model.composerModel.normalDamping = damping;
        public void SetComposerNormalLookAngles(Vector3 angles) => model.composerModel.normalLookAngles = angles;
        public void SetComposerNormalLookActivated(bool activated) => model.composerModel.normalLookActivated = activated;

        // ==== Temp ====
        EasingElement easingElement;

        public TCCameraLookAtComponent() {
            easingElement = new EasingElement();
        }

        public void SetEasing(EasingType easingType_horizontal, float duration_horizontal, EasingType easingType_vertical, float duration_vertical) {
            this.model.easingType_horizontal = easingType_horizontal;
            this.model.duration_horizontal = duration_horizontal;

            this.model.easingType_vertical = easingType_vertical;
            this.model.duration_vertical = duration_vertical;
        }

        Vector3 lastFollowTargetPos;
        public void Tick(TCInfoModel infoModel, float dt) {
            if (!targetorModel.HasLookAtTarget) {
                return;
            }

            var dstTargetPos = targetorModel.LookAtTargetPos;
            var curFollowTargetPos = targetorModel.FollowTargetPos;
            if (lastFollowTargetPos != curFollowTargetPos) {
                lastFollowTargetPos = curFollowTargetPos;
                var curCamPos = infoModel.Position;
                var dis = Vector3.Distance(curCamPos, dstTargetPos);
                var newStartTargetPos = infoModel.Position + infoModel.Rotation * Vector3.forward * dis;
                easingElement.Reset(newStartTargetPos);
            }

            easingElement.TickEasing(dt,
                                     model.easingType_horizontal,
                                     model.duration_horizontal,
                                     model.easingType_vertical,
                                     model.duration_vertical,
                                     dstTargetPos);
        }

        [System.Obsolete]
        public void OffsetAdd(Vector3 offset) { }

        public bool CanLookAt() {
            return isLookAtEnabled && targetorModel.HasLookAtTarget;
        }

        public void SetNormalLookAngles(Vector3 angles) {
            this.model.normalLookAngles = angles;
        }

        // public void ChangeTarget(Transform target) {
        //     targetorModel.SetLookAtTarget(target);
        // }

        [System.Obsolete]
        public void ChangeOffset(Vector3 offset) { }

        public Vector3 GetTargetEasedPos() {
            return easingElement.EaseValue;
        }

        [System.Obsolete]
        public Quaternion GetLookAtRotation_LockOn(Vector3 lockOnPos, Vector3 pos, Quaternion rot) {
            var fwd = lockOnPos - pos;
            var z = rot.eulerAngles.z;
            rot = Quaternion.LookRotation(fwd);
            rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, z);
            return rot;
        }

    }

}