using System;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {


    public class TCCameraFOVStateComponent {

        // Args
        TCFOVStateModel[] modelArray;
        bool isExitReset;
        EasingType exitEasing;
        float exitDuration;

        // Temp
        int index;
        float resOffset;
        float resOffset_inherit;
        float time;

        float exitStartOffset;
        bool isExiting;
        float exitTime;

        public event Action OnEndHandle;

        public TCCameraFOVStateComponent() { }

        public void Enter(TCFOVStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {

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
        }

        public void Tick(float dt) {

            if (isExiting && isExitReset) {
                Exiting(dt);
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
                resOffset = EasingHelper.Ease1D(cur.easingType, time, cur.duration, resOffset_inherit, cur.offset);
            } else {
                resOffset = EasingHelper.Ease1D(cur.easingType, time, cur.duration, 0, cur.offset);
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
                        resOffset = 0;
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

            if (resOffset == 0) {
                return;
            }

            exitTime += dt;
            resOffset = EasingHelper.Ease1D(exitEasing, exitTime, exitDuration, exitStartOffset, 0);

            if (exitTime >= exitDuration) {
                exitTime = 0;
            }

        }

        public float GetFOVOffset() {
            return resOffset;
        }

    }

}