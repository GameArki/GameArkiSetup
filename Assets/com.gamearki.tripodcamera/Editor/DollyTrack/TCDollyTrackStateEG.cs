#if UNITY_EDITOR

using UnityEngine;
using GameArki.TripodCamera.Template;

namespace GameArki.TripodCamera.EditorTool {

    public class TCDollyTrackStateEG : MonoBehaviour {

        public TCCameraSO so;
        public TCDollyTrackStateEM em;

        // Inspector
        public Transform bindDollyTF;
        public float trackWidth = 1f;
        public float t;
        public bool isPlaying;

    }

}

#endif