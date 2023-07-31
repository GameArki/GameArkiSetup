using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace GameArki.Anymotion {

    public class AnymotionLayerEntity {

        Dictionary<string, AnymotionTransModel> allTrans;
        AnimationMixerPlayable mixerPlayable;
        AnymotionTransModel defaultModel;

        AnymotionStatus status;
        AnymotionFSM_Normal normalStateModel;
        AnymotionFSM_Crossfading crossfadingStateModel;

        float speed;

        public AnymotionLayerEntity() {
            speed = 1f;
            status = AnymotionStatus.Normal;
            normalStateModel = new AnymotionFSM_Normal();
            crossfadingStateModel = new AnymotionFSM_Crossfading();
            allTrans = new Dictionary<string, AnymotionTransModel>(30);
        }

        public void Tick(float dt) {
            if (status == AnymotionStatus.Normal) {
                TickNormal(dt);
            } else if (status == AnymotionStatus.Crossfading) {
                TickCrossFade(dt);
            }
        }

        public void SetSpeed(float value) {
            this.speed = value;
            mixerPlayable.SetSpeed(value);
        }

        public AnimationClip GetCurrentClip() {
            if (status == AnymotionStatus.Normal) {
                return normalStateModel.currentTrans.clip;
            } else if (status == AnymotionStatus.Crossfading) {
                return crossfadingStateModel.currentTrans.clip;
            }
            return null;
        }

        public AnimationClip GetClipByState(string stateName) {
            bool has = allTrans.TryGetValue(stateName, out var model);
            if (!has) {
                Debug.LogError($"AnymotionLayerEntity.GetClipByState: stateName {stateName} not found");
                return null;
            }
            return model.clip;
        }

        public string GetCurrentStateName() {
            if (status == AnymotionStatus.Normal) {
                return normalStateModel.currentTrans.stateName;
            } else if (status == AnymotionStatus.Crossfading) {
                return crossfadingStateModel.currentTrans.stateName;
            }
            return null;
        }

        public float GetCurrentClipPassedTime() {
            if (status == AnymotionStatus.Normal) {
                return normalStateModel.time;
            } else if (status == AnymotionStatus.Crossfading) {
                return crossfadingStateModel.time;
            }
            return 0f;
        }

        public void Add(AnymotionTransModel transModel) {
            allTrans.Add(transModel.stateName, transModel);
        }

        public void SetDefaultState(string stateName) {
            bool has = allTrans.TryGetValue(stateName, out var model);
            if (!has) {
                Debug.LogError($"AnymotionLayerEntity.SetDefaultState: stateName {stateName} not found");
                return;
            }
            defaultModel = model;
        }

        public void EndAdd(ref PlayableGraph graph, ref AnimationPlayableOutput output) {

            mixerPlayable = AnimationMixerPlayable.Create(graph, allTrans.Count);
            output.SetSourcePlayable(mixerPlayable);

            int index = 0;
            foreach (var kv in allTrans) {
                var model = kv.Value;
                model.index = index;
                var clipPlayable = AnimationClipPlayable.Create(graph, model.clip);
                graph.Connect(clipPlayable, 0, mixerPlayable, model.index);
                index += 1;
            }

        }

        bool TryGetState(string stateName, out AnymotionTransModel model) {
            if (string.IsNullOrEmpty(stateName)) {
                Debug.LogError($"AnymotionLayerEntity.Play: stateName {stateName} is null or empty");
                model = null;
                return false;
            }
            bool has = allTrans.TryGetValue(stateName, out model);
            if (!has) {
                Debug.LogError($"AnymotionLayerEntity.Play: stateName {stateName} not found");
            }
            return true;
        }

        public void PlayDefault(bool isRestartSameState) {
            if (defaultModel == null) {
                Debug.LogError($"AnymotionLayerEntity.PlayDefault: defaultModel is null");
                return;
            }
            PlayNormal(defaultModel.stateName, isRestartSameState);
        }

        public void PlayNormal(string nextStateName, bool isRestartSameState, float startTime = 0) {

            if (string.IsNullOrEmpty(nextStateName)) {
                Debug.LogError($"AnymotionLayerEntity.Play: stateName {nextStateName} is null or empty");
                return;
            }

            bool has = allTrans.TryGetValue(nextStateName, out var nextModel);
            if (!has) {
                Debug.LogError($"AnymotionLayerEntity.Play: stateName {nextStateName} not found");
                return;
            }

            // Set weight
            EnterNormal(nextModel, startTime, isRestartSameState);
        }

        public void CrossFade(string stateName, float fadeSec, AnymotionCrossFadeSameStateStrategyType sameStateStrategy) {
            if (!TryGetState(stateName, out var nextModel)) {
                Debug.LogError($"AnymotionLayerEntity.CrossFade: stateName {stateName} not found");
                return;
            }
            CrossFade(nextModel, fadeSec, sameStateStrategy);
        }

        public void CrossFade(string stateName, AnymotionCrossFadeSameStateStrategyType sameStateStrategy) {
            if (!TryGetState(stateName, out var nextModel)) {
                Debug.LogError($"AnymotionLayerEntity.CrossFade: stateName {stateName} not found");
                return;
            }
            CrossFade(nextModel, nextModel.fadeSec, sameStateStrategy);
        }

        void CrossFade(AnymotionTransModel nextModel, float fadeSec, AnymotionCrossFadeSameStateStrategyType sameStateStrategy) {

            if (status == AnymotionStatus.Crossfading) {
                EnterCrossFade(crossfadingStateModel.nextTrans, nextModel, fadeSec, sameStateStrategy);
            } else if (status == AnymotionStatus.Normal) {
                EnterCrossFade(normalStateModel.currentTrans, nextModel, fadeSec, sameStateStrategy);
            }

        }

        public void Stop() {
            if (status == AnymotionStatus.Normal) {
                normalStateModel.time = 0f;
            } else if (status == AnymotionStatus.Crossfading) {
                crossfadingStateModel.time = 0f;
            }
        }

        public void Pause() {
            mixerPlayable.Pause();
        }

        // FSM
        void EnterNormal(AnymotionTransModel nextModel, float startTime, bool isRestartSameState) {

            if (normalStateModel.currentTrans != null && (normalStateModel.currentTrans.stateName == nextModel.stateName) && !isRestartSameState) {
                return;
            }

            status = AnymotionStatus.Normal;
            normalStateModel.Enter(startTime, nextModel.clip.length, nextModel);

            if (isRestartSameState) {
                mixerPlayable.GetInput(nextModel.index).SetTime(startTime);
            }
            mixerPlayable.SetInputWeight(nextModel.index, 1f);

            foreach (var kv in allTrans) {
                var transModel = kv.Value;
                if (transModel.stateName != nextModel.stateName) {
                    mixerPlayable.SetInputWeight(transModel.index, 0f);
                }
            }

        }

        void EnterCrossFade(AnymotionTransModel curTrans, AnymotionTransModel nextTrans, float duration, AnymotionCrossFadeSameStateStrategyType sameStateStrategy) {
            if (curTrans.stateName == nextTrans.stateName && sameStateStrategy == AnymotionCrossFadeSameStateStrategyType.NothingTodo) {
                return;
            }
            status = AnymotionStatus.Crossfading;
            crossfadingStateModel.Enter(duration, curTrans, nextTrans, sameStateStrategy);
            TickCrossFade(0);
        }

        void TickNormal(float dt) {

            var stateModel = normalStateModel;
            if (stateModel.isEntering) {
                stateModel.isEntering = false;
            }

            ref float time = ref stateModel.time;
            if (time >= stateModel.duration) {
                var cur = stateModel.currentTrans;
                // 如果非循环, 则切换到默认状态
                if (!cur.clip.isLooping && defaultModel.stateName != cur.stateName) {
                    // 该参数不会生效, 因为 stateName 一定不相同
                    var nostrategy = AnymotionCrossFadeSameStateStrategyType.Default;
                    EnterCrossFade(cur, defaultModel, defaultModel.fadeSec, nostrategy);
                } else {
                    // Nothing to do
                    // It's looping or default stateName
                }
            } else {
                time += dt * speed;
            }
        }

        void TickCrossFade(float dt) {

            var stateModel = crossfadingStateModel;
            if (stateModel.isEntering) {
                stateModel.isEntering = false;
            }

            ref float time = ref stateModel.time;
            if (time >= stateModel.duration) {
                if (!stateModel.nextTrans.clip.isLooping) {
                    mixerPlayable.GetInput(stateModel.nextTrans.index).SetTime(0);
                }
                EnterNormal(stateModel.nextTrans, time, stateModel.sameStateStrategyType.IsRestartSameState());
            } else {
                time += dt * speed;

                if (stateModel.isSameState) {
                    // 相同状态
                    var strategy = stateModel.sameStateStrategyType;
                    foreach (var kv in allTrans) {
                        var transModel = kv.Value;
                        int index = transModel.index;
                        if (transModel.stateName == stateModel.currentTrans.stateName) {
                            if (strategy == AnymotionCrossFadeSameStateStrategyType.Cut) {
                                // 直接切换
                                mixerPlayable.GetInput(index).SetTime(0);
                            } else if (strategy == AnymotionCrossFadeSameStateStrategyType.FastToStart) {
                                // 按 fadeInSec 的一半时间快速切换到开始
                            } else if (strategy == AnymotionCrossFadeSameStateStrategyType.FadeToStartHalf) {
                                // 按 fadeInSec 的一定时间淡入到开始
                            } else if (strategy == AnymotionCrossFadeSameStateStrategyType.NothingTodo) {
                                // Nothing to do
                            }
                            mixerPlayable.SetInputWeight(index, 1f);
                        } else {
                            mixerPlayable.SetInputWeight(index, 0f);
                        }
                    }

                } else {
                    // 非相同状态
                    float percent = time / stateModel.duration;
                    percent = Mathf.Clamp01(percent);
                    foreach (var kv in allTrans) {
                        var transModel = kv.Value;
                        int index = transModel.index;
                        if (transModel.stateName == stateModel.currentTrans.stateName) {
                            mixerPlayable.SetInputWeight(index, 1f - percent);
                        } else if (transModel.stateName == stateModel.nextTrans.stateName) {
                            mixerPlayable.SetInputWeight(index, percent);
                        } else {
                            mixerPlayable.SetInputWeight(index, 0f);
                        }
                    }
                }
            }

        }

    }

}
