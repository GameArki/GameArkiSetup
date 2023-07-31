using UnityEngine;

namespace GameArki.TripodCamera.Entities {

    public class TCTargetorModel {

        //- Follow
        Transform followTarget;
        public Transform FollowTarget => followTarget;
        public void SetFollowTarget(Transform value) => followTarget = value;

        public bool HasFollowTarget => followTarget != null;
        public Vector3 FollowTargetPos => followTarget.transform.position;
        public Vector3 FollowTargetForward => followTarget.transform.forward;

        //- LookAt 
        Transform lookAtTarget;
        public Transform LookAtTarget => lookAtTarget;
        public void SetLookAtTarget(Transform value) => lookAtTarget = value;

        public bool HasLookAtTarget => lookAtTarget != null;
        public Vector3 LookAtTargetPos => lookAtTarget.transform.position;

    }

}