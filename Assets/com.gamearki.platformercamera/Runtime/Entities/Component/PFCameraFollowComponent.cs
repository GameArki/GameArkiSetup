using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.PlatformerCamera.Entities {

    public class PFCameraFollowComponent {

        Transform followTF;
        public Transform FollowTF => followTF;
        public void SetTarget(Transform value) => followTF = value;

        Vector3 offset;
        public Vector3 Offset => offset;
        public void SetOffset(Vector3 value) => offset = value;

        EasingType easeingYType;
        float easeingYDuration;

        EasingType easeingXType;
        float easeingXDuration;

        // ==== Temp ====
        [SerializeField] Vector3 easeingCurPos;
        [SerializeField] Vector3 easeingStartPos;
        [SerializeField] Vector3 easeingDstPos;
        [SerializeField] float easeingYTime;
        [SerializeField] float easeingXTime;

        public PFCameraFollowComponent() { }

        public void InitFollow(Transform target, Vector3 offset, EasingType easingXType, float easingXDuration, EasingType easingYType, float easeingYDuration) {
            this.followTF = target;
            this.offset = offset;
            this.easeingXType = easingXType;
            this.easeingXDuration = easingXDuration;
            this.easeingYType = easingYType;
            this.easeingYDuration = easeingYDuration;
        }

        public void TickEasing(float dt) {

            if (followTF == null) {
                return;
            }

            if (easeingYDuration == 0) {
                easeingCurPos = followTF.position;
                return;
            }

            if (easeingDstPos != followTF.position) {
                easeingStartPos = easeingCurPos;
                easeingDstPos = followTF.position;
                easeingXTime = 0;
                easeingYTime = 0;
            }

            if (easeingXTime < easeingXDuration) {
                easeingXTime += dt;
                easeingCurPos.x = EasingHelper.Ease1D(easeingXType, easeingXTime, easeingXDuration, easeingStartPos.x, easeingDstPos.x);
            }

            if (easeingYTime < easeingYDuration) {
                easeingYTime += dt;
                easeingCurPos.y = EasingHelper.Ease1D(easeingYType, easeingYTime, easeingYDuration, easeingStartPos.y, easeingDstPos.y);
            }

        }

        public bool HasTarget() {
            return followTF != null;
        }

        public void MoveOffset(Vector3 diff) {
            this.offset += diff;
        }

        public Vector3 GetFollowPos() {
            if (followTF == null) {
                return Vector3.zero;
            }
            return easeingCurPos + offset;
        }

    }

}