using System;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Template {

    [Serializable]
    public struct TCCameraTM {

        public int id;

        // ==== Normal ====
        public bool needSet_Follow;
        public TCFollowTM followTM;

        public bool needSet_LookAt;
        public TCLookAtTM lookAtTM;

        // ==== State ====
        public bool needSet_DollyTrack;
        public TCDollyTrackStateTM dollyTrackStateTM;
        public bool isExitReset_DollyTrack;
        public EasingType exitEasing_DollyTrack;
        public float exitDuration_DollyTrack;

        public bool needSet_Shake;
        public TCShakeStateTM[] shakeStateTMArray;
        public bool isExitReset_Shake;
        public EasingType exitEasing_Shake;
        public float exitDuration_Shake;

        public bool needSet_Movement;
        public TCMovementStateTM[] movementStateTMArray;
        public bool isExitReset_Movement;
        public EasingType exitEasing_Movement;
        public float exitDuration_Movement;

        public bool needSet_Round;
        public TCRoundStateTM[] roundStateTMArray;
        public bool isExitReset_Round;
        public EasingType exitEasing_Round;
        public float exitDuration_Round;

        public bool needSet_Rotate;
        public TCRotateStateTM[] rotateStateTMArray;
        public bool isExitReset_Rotate;
        public EasingType exitEasing_Rotate;
        public float exitDuration_Rotate;

        public bool needSet_Push;
        public TCPushStateTM[] pushStateTMArray;
        public bool isExitReset_Push;
        public EasingType exitEasing_Push;
        public float exitDuration_Push;

        public bool needSet_FOV;
        public TCFOVStateTM[] fovStateTMArray;
        public bool isExitReset_FOV;
        public EasingType exitEasing_FOV;
        public float exitDuration_FOV;

        public bool needSet_Misc;
        public TCMiscTM miscTM;

    }

}