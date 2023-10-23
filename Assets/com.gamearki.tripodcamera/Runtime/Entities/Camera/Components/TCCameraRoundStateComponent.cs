using System;
using GameArki.FPEasing;
using UnityEngine;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraRoundStateComponent {

        TCTargetorModel targetorModel;
        public void SetTargetorModel(TCTargetorModel targetorModel) => this.targetorModel = targetorModel;

        // Args
        TCRoundStateModel[] modelArray;
        bool isExitReset;
        EasingType exitEasing;
        float exitDuration;

        // Temp
        int index;
        Vector2 resOffset;
        Vector2 resOffset_inherit;
        float time;

        Vector2 exitStartOffset;
        float exitTime;

        public event Action OnEndHandle;

        bool isActivated;
        public bool IsActivated => isActivated;

        bool isExiting;

        public TCCameraRoundStateComponent() { }

        public void Enter(TCRoundStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {
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

            this.isActivated = true;
            this.isExiting = false;
        }

        public void Tick(float dt) {

            if (isExiting) {
                if (isExitReset) {
                    Exiting(dt);
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
                resOffset = EasingHelper.Ease2D(cur.easingType, time, cur.duration, resOffset_inherit, cur.offset);
            } else {
                resOffset = EasingHelper.Ease2D(cur.easingType, time, cur.duration, Vector2.zero, cur.offset);
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
                        resOffset = Vector2.zero;
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

        void Exiting(float dt) {
            exitTime += dt;
            resOffset = EasingHelper.Ease2D(exitEasing, exitTime, exitDuration, exitStartOffset, Vector2.zero);

            if (exitTime >= exitDuration) {
                exitTime = 0;
                isExiting = false;
                isActivated = false;
            }
        }

        public Vector3 GetRoundOffset(Vector3 pos) {
            if (modelArray == null) {
                return Vector3.zero;
            }

            var tarPos = targetorModel.FollowTargetPos;
            Vector3 dir = pos - tarPos;
            float length = dir.magnitude;
            dir.Normalize();

            // Round Dir
            float angleX = -resOffset.x;
            float angleY = resOffset.y;
            var up = dir;
            up.x = 0;
            up.z = 0;
            var right = dir;
            right.y = 0;
            right = Quaternion.Euler(0, 90, 0) * right;
            Quaternion rotX = Quaternion.AngleAxis(angleX, up);
            Quaternion rotY = Quaternion.AngleAxis(angleY, right);
            dir = rotX * rotY * dir;
            var offset = (tarPos + dir * length) - pos;

            return offset;
        }

    }

}