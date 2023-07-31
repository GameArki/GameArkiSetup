using System;
using UnityEngine;

namespace GameArki.TripodCamera.Template {

    [CreateAssetMenu(fileName = "so_tc_config", menuName = "GameArki/TripodCamera/TCConfig")]
    public class TCCameraSO : ScriptableObject {

        [SerializeField] public TCCameraTM tm;

    }

}