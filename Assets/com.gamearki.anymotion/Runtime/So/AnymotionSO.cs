using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.Anymotion {

    [CreateAssetMenu(fileName = "so_anymotion_", menuName = "GameArki/so_anymotion", order = 0)]
    public class AnymotionSO : ScriptableObject {

        public AnymotionTransitionTM[] transitions;

    }

}
