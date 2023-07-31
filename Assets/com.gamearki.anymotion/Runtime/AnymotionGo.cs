using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace GameArki.Anymotion {

    [RequireComponent(typeof(Animator))]
    public class AnymotionGo : MonoBehaviour {

        Animator animator;
        PlayableGraph graph;
        AnimationPlayableOutput output;

        // Layer
        Dictionary<int, AnymotionLayerEntity> layerDict;

        public void Ctor() {

            layerDict = new Dictionary<int, AnymotionLayerEntity>();
            animator = GetComponent<Animator>();

            graph = PlayableGraph.Create();
            output = AnimationPlayableOutput.Create(graph, "Anymotion", animator);
            graph.SetTimeUpdateMode(DirectorUpdateMode.Manual);

        }

        public void Tick(float dt) {
            graph.Evaluate(dt);
            foreach (var layerEntity in layerDict.Values) {
                layerEntity.Tick(dt);
            }
        }

        public void SetSpeed(int layer, float speed) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.SetSpeed: layer {layer} not found");
                return;
            }
            layerEntity.SetSpeed(speed);
        }

        // - Get
        public float GetCurrentClipPassedTime(int layer) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.GetCurrentClipPassedTime: layer {layer} not found");
                return 0f;
            }
            return layerEntity.GetCurrentClipPassedTime();
        }

        public string GetCurrentStateName(int layer) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.GetCurrentStateName: layer {layer} not found");
                return null;
            }
            return layerEntity.GetCurrentStateName();
        }

        public AnimationClip GetCurrentClip(int layer) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.GetCurrentClip: layer {layer} not found");
                return null;
            }
            return layerEntity.GetCurrentClip();
        }

        public AnimationClip GetClipByState(int layer, string stateName) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.GetClipByState: layer {layer} not found");
                return null;
            }
            return layerEntity.GetClipByState(stateName);
        }

        public void SetDefaultState(int layer, string stateName) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.SetDefaultState: layer {layer} not found");
                return;
            }
            layerEntity.SetDefaultState(stateName);
        }

        public void Add(int layer, string stateName, AnimationClip clip, float fadeSec) {

            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                layerEntity = new AnymotionLayerEntity();
                layerDict.Add(layer, layerEntity);
            }
            layerEntity.Add(new AnymotionTransModel() {
                stateName = stateName,
                clip = clip,
                fadeSec = fadeSec,
            });

        }

        public void EndAdd(int layer) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.Setup: layer {layer} not found");
                return;
            }
            layerEntity.EndAdd(ref graph, ref output);
        }

        public void PlayDefault(int layer, bool isRestartSameState) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.PlayDefault: layer {layer} not found");
                return;
            }
            layerEntity.PlayDefault(isRestartSameState);
            if (!graph.IsPlaying()) {
                graph.Play();
            }
        }

        public void Play(int layer, string stateName, bool isRestartSameState, float startTime = 0) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.Play: layer {layer} not found");
                return;
            }
            layerEntity.PlayNormal(stateName, isRestartSameState, startTime);
            if (!graph.IsPlaying()) {
                graph.Play();
            }
        }


        public void EnterCrossFade(int layer, string stateName, AnymotionCrossFadeSameStateStrategyType sameStateStrategy = AnymotionCrossFadeSameStateStrategyType.Default) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.CrossFade: layer {layer} not found");
                return;
            }
            layerEntity.CrossFade(stateName, sameStateStrategy);
        }

        public void EnterCrossFade(int layer, string stateName, float fadeSec, AnymotionCrossFadeSameStateStrategyType sameStateStrategy = AnymotionCrossFadeSameStateStrategyType.Default) {
            if (!layerDict.TryGetValue(layer, out var layerEntity)) {
                Debug.LogError($"AnymotionGo.CrossFade: layer {layer} not found");
                return;
            }
            layerEntity.CrossFade(stateName, fadeSec, sameStateStrategy);
        }

        public void TearDown() {
            graph.Destroy();
        }

    }

}
