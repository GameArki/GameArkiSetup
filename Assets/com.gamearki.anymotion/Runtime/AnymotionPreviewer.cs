using System;
using UnityEngine;

namespace GameArki.Anymotion {

    [ExecuteAlways]
    public class AnymotionPreviewer : MonoBehaviour {

        // Model
        [SerializeField] AnymotionGo anymotionGo;

        [SerializeField] AnymotionSO anymotionSO;
        int layer = 1;
        float speed = 1f;

        void OnEnable() {
            AwakeOnce();
        }

        void Update() {

            // if (Input.GetKey(KeyCode.W)) {
            //     anymotionGo.EnterCrossFade(layer, "Run", AnymotionCrossFadeSameStateStrategyType.NothingTodo);
            // } else if (Input.GetKeyUp(KeyCode.D)) {
            //     anymotionGo.EnterCrossFade(layer, "Idle", AnymotionCrossFadeSameStateStrategyType.NothingTodo);
            // }

            // Step 6: 更新
            if (anymotionGo != null) {
                anymotionGo.Tick(Time.deltaTime); // 每帧更新
            }
        }

        void AwakeOnce() {
            if (anymotionGo == null || anymotionSO == null) {
                Debug.LogError("anymotionGo or anymotionSO is null");
                return;
            }
            AnymotionUtil.InitializeAndPlayDefault(layer, anymotionGo, anymotionSO);
        }

        void OnGUI() {

#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying) {
                return;
            }
#endif

            bool isNull = false;
            if (anymotionGo == null) {
                // color red
                GUILayout.Label("anymotionGo is null", new GUIStyle() { normal = new GUIStyleState() { textColor = Color.red } });
                isNull = true;
            }
            if (anymotionSO == null) {
                GUILayout.Label("anymotionSO is null", new GUIStyle() { normal = new GUIStyleState() { textColor = Color.red } });
                isNull = true;
            }

            if (isNull) {
                return;
            }

            var all = anymotionSO.transitions;
            int group = 0;
            GUILayout.BeginHorizontal();
            const int MAX_SLOT = 6;
            for (int i = 0; i < all.Length; i += 1) {
                var trans = all[i];
                string stateName = trans.stateName;
                if (i % MAX_SLOT == 0) {
                    group += 1;
                    GUILayout.BeginVertical();
                }
                if (GUILayout.Button("Play " + stateName)) {
                    // 后续调用: 播放 State
                    anymotionGo.Play(layer, stateName, true);
                }
                if (GUILayout.Button("CrossFade " + stateName)) {
                    // 后续调用: 过渡 State
                    anymotionGo.EnterCrossFade(layer, stateName, AnymotionCrossFadeSameStateStrategyType.Default);
                }
                if (i % MAX_SLOT == MAX_SLOT - 1) {
                    group -= 1;
                    GUILayout.EndVertical();
                }
            }
            if (group > 0) {
                GUILayout.EndHorizontal();
            }
            GUILayout.EndHorizontal();

            speed = GUILayout.HorizontalSlider(speed, 0f, 2f);
            if (GUILayout.Button("切换速度")) {
                anymotionGo.SetSpeed(layer, speed); // 恢复正常速度
            }

        }

        void OnApplicationQuit() {
            anymotionGo.TearDown(); // 释放
        }

    }

}