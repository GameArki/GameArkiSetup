using UnityEngine;

namespace GameArki.Anymotion {

    internal class AnymotionFSM_Crossfading {

        internal bool isEntering;
        internal float time;
        internal float duration;
        internal AnymotionTransModel currentTrans;
        internal AnymotionTransModel nextTrans;
        internal AnymotionCrossFadeSameStateStrategyType sameStateStrategyType;
        
        internal bool isSameState;

        internal void Enter(float duration, AnymotionTransModel currentTrans, AnymotionTransModel nextTrans, AnymotionCrossFadeSameStateStrategyType sameStateStrategyType) {
            this.duration = duration;
            this.currentTrans = currentTrans;
            this.nextTrans = nextTrans;
            this.isEntering = true;
            this.sameStateStrategyType = sameStateStrategyType;
            this.isSameState = currentTrans.stateName == nextTrans.stateName;
            this.time = 0;
        }

    }

}