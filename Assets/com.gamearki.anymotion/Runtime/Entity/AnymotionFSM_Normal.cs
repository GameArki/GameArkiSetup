using UnityEngine;

namespace GameArki.Anymotion {

    internal class AnymotionFSM_Normal {

        internal bool isEntering;
        internal float time;
        internal float duration;
        internal AnymotionTransModel currentTrans;

        internal void Enter(float time, float duration, AnymotionTransModel trans) {
            this.duration = duration;
            this.currentTrans = trans;
            this.time = time;
            this.isEntering = true;
        }

    }

}