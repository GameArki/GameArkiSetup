using UnityEngine;

namespace GameArki.TripodCamera.Entities {

    public class TCTargetorModel {

        //- Follow
        Vector3? followPosition = null;
        public Vector3 FollowPosition => followPosition ?? Vector3.zero;
        public void SetFollowPos(Vector3? value) => followPosition = value;
        Quaternion followRotation;
        public Quaternion FollowRotation => followRotation;
        public void SetFollowRotaion(Quaternion value) => followRotation = value;

        public bool HasFollowTarget => followPosition != null;
        public Vector3 FollowTargetPos => followPosition ?? Vector3.zero;
        public Vector3 FollowTargetForward => followRotation * Vector3.forward;

        //- LookAt 
        Vector3? lookAtTarget;
        public Vector3 LookAtTarget => lookAtTarget ?? Vector3.zero;
        public void SetLookAtTarget(Vector3? value) => lookAtTarget = value;

        public bool HasLookAtTarget => lookAtTarget != null;
        public Vector3 LookAtTargetPos => lookAtTarget ?? Vector3.zero;

    }

}