using System;
using UnityEngine;

namespace GameArki.Anymotion {

    [Serializable]
    public class AnymotionTransitionTM {

        public string stateName;
        public AnimationClip clip;
        public float fadeInSec;
        public bool isDefault;

    }

}