using GameArki.FPEasing;
using GameArki.TripodCamera.Domain;
using UnityEngine;

namespace GameArki.TripodCamera.API {

    public class TCFollowAPI : ITCFollowAPI {

        TCFollowDomain followDomain;

        public TCFollowAPI() { }

        public void Inject(TCFollowDomain followDomain) {
            this.followDomain = followDomain;
        }

        void ITCFollowAPI.ChangeOffset(in Vector3 offset, int id) {
            followDomain.ChangeOffset(offset, id);
        }

        // void ITCFollowAPI.ChangeTarget(Transform target, int id) {
        //     followDomain.ChangeTarget(target, id);
        // }

        Vector3 ITCFollowAPI.GetNormalOffset(int id) {
            return followDomain.GetNormalOffset(id);
        }

        // Transform ITCFollowAPI.GetTransform(int id) {
        //     return followDomain.GetTransform(id);
        // }

        bool ITCFollowAPI.HasTarget(int id) {
            return followDomain.HasTarget(id);
        }

        void ITCFollowAPI.SetEasing(EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical, int id) {
            followDomain.SetEasing(easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical, id);
        }

        void ITCFollowAPI.SetInit(in Vector3 offset, EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical, int id) {
            followDomain.SetInit(offset, easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical, id);
        }

        void ITCFollowAPI.TickFollowPos(in Vector3 pos, in Quaternion rot, int id) {
            followDomain.TickFollowPos(pos, rot, id);
        }

        void ITCFollowAPI.SetFollowType(TCFollowType followType, int id) {
            followDomain.SetFollowType(followType, id);
        }

        void ITCFollowAPI.ManualRounding_Horizontal(float degreeY, float duration, EasingType exitEasingType, float exitDuration) {
            followDomain.ManualRounding_Horizontal(degreeY, duration, exitEasingType, exitDuration);
        }

        void ITCFollowAPI.ManualRounding_Vertical(float degreeX, float duration, EasingType exitEasingType, float exitDuration) {
            followDomain.ManualRounding_Vertical(degreeX, duration, exitEasingType, exitDuration);
        }

        public void CancelFollow(int id) {
            followDomain.CancelFollow(id);
        }
    }

}