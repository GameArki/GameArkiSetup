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

        public TCCameraFOVStateComponent() {
        }

        public void Reset() {
            this.index = 0;
            this.time = 0;
            this.exitTime = 0;
            this.isExiting = false;
            this.resOffset = 0;
        }

        public void Enter(TCFOVStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {
            if (args.Length == 0) return;

            Reset();

            this.modelArray = args;
            this.isExitReset = isExitReset;
            this.exitEasing = exitEasing;
            this.exitDuration = exitDuration;
        }

        public void Tick(float dt) {
            if (isExiting && isExitReset) {
                Exiting(dt);
                return;
            }

            Execute(dt);
        }

        void Execute(float dt) {
            if (modelArray == null || index >= modelArray.Length) return;

            time += dt;

            var curModel = modelArray[index];
            if (curModel.isInherit) resOffset = EasingHelper.Ease1D(curModel.easingType, time, curModel.duration, resOffset_inherit, curModel.offset);
            else resOffset = EasingHelper.Ease1D(curModel.easingType, time, curModel.duration, 0, curModel.offset);

            if (time >= curModel.duration) {
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
                    OnEndHandle?.Invoke();
                    exitStartOffset = resOffset;
                    isExiting = true;
                }
            }
        }

        void Exiting(float dt) {
            exitTime += dt;
            resOffset = EasingHelper.Ease1D(exitEasing, exitTime, exitDuration, exitStartOffset, 0);

            if (exitTime >= exitDuration) {
                exitTime = 0;
                isExiting = false;
            }
        }

        public float GetFOVOffset() {
            return resOffset;
        }

    }

}