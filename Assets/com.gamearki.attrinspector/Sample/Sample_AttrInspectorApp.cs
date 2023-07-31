using System;
using UnityEngine;

namespace GameArki.AttrInspector.Sample {

    public class Sample_AttrInspectorApp : MonoBehaviour {

        [ATSlider(nameof(myMin), nameof(myMax))]
        public float mySlider;

        protected float myMin = 2;
        protected float myMax = 5;

        [ATButton("Test1")]
        public void Test1() {
            Debug.Log(1);
        }

        [ATButton("Test2")]
        public void Test2() {
            Debug.Log(2);
        }

        [ATEnumToggleButton("敌人类型")]
        public EnemyType enemytype;

    }

    public enum EnemyType {
        //  战士 = 1,
        //  法师 = 2,
        //  坦克 = 3,
        //  射手 = 4,
        //  辅助 = 5,
         战士 = 1<<0,
         法师 = 1<<1,
         坦克 = 1<<2,
         射手 = 1<<3,
         辅助 = 1<<4,
    }

}
