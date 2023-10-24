using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraMovementStateComponent {

        // Args
        TCMovementStateModel[] modelArray;
        bool isExitReset;
        EasingType exitEasing;
        float exitDuration;

        // Temp
        int index;
        Vector3 resOffset;
        Vector3 resOffset_inherit;
        float time;

        Vector3 exitStartOffset;
        float exitTime;

        public event Action OnEndHandle;

        bool isExiting;

        bool isActivated;
        public bool IsActivated => isActivated;

        public TCCameraMovementStateComponent() { }

        public void Enter(TCMovementStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {

            if (args.Length == 0) return;
            var args_0 = args[0];
            if (args_0.isInherit) {
                resOffset_inherit = resOffset;
                args_0.offset += resOffset;
                args[0] = args_0;
            }

            this.modelArray = args;

            this.isExitReset = isExitReset;
            this.exitEasing = exitEasing;
            this.exitDuration = exitDuration;

            this.index = 0;
            this.time = 0;
            this.exitTime = 0;
            this.isExiting = false;
            this.isActivated = true;
        }

        public void Tick(float dt) {
            if (isExiting) {
                if (isExitReset) {
                    TickExiting(dt);
                } else {
                    isActivated = false;
                    isExiting = false;
                }

                return;
            }

            Execute(dt);
        }

        void Execute(float dt) {

            if (modelArray == null || index >= modelArray.Length) {
                return;
            }

            time += dt;

            var cur = modelArray[index];
            if (cur.isInherit) {
                resOffset = EasingHelper.Ease3D(cur.easingType, time, cur.duration, resOffset_inherit, cur.offset);
            } else {
                resOffset = EasingHelper.Ease3D(cur.easingType, time, cur.duration, Vector3.zero, cur.offset);
            }

            if (time >= cur.duration) {
                time = 0;
                index += 1;

                bool hasNext = index < modelArray.Length;
                if (hasNext) {
                    var next = modelArray[index];
                    if (next.isInherit) {
                        next.offset += resOffset;
                        resOffset_inherit = resOffset;
                    } else {
                        resOffset = Vector3.zero;
                    }
                } else {
                    if (OnEndHandle != null) {
                        OnEndHandle.Invoke();
                    }
                    exitStartOffset = resOffset;
                    isExiting = true;
                }

            }
        }

        void TickExiting(float dt) {
            exitTime += dt;
            resOffset = EasingHelper.Ease2D(exitEasing, exitTime, exitDuration, exitStartOffset, Vector3.zero);

            if (exitTime >= exitDuration) {
                exitTime = 0;
                isActivated = false;
                isExiting = false;
            }

        }

        public Vector3 GetMoveOffset() {
            return resOffset;
        }

    }

}