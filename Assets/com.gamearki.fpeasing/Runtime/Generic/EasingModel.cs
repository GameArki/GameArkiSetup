using System;

namespace GameArki.FPEasing {

    [Serializable]
    public struct EasingModel {
        public EasingType type;
        public float timeWeight;
        public float maxValuePercentReduce;
        public bool isFlipValue;
    }

}