using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraShakeStateComponent {

        // Args
        TCShakeStateModel[] modelArray;
        int index;

        Vector3 resOffset;
        float time;

        public TCCameraShakeStateComponent() { }

        public void Enter(TCShakeStateModel[] modelArray) {
            this.modelArray = modelArray;
            this.index = 0;
            this.time = 0;
        }

        public void Tick(float dt) {

            if (modelArray == null || index >= modelArray.Length) {
                return;
            }

            time += dt;

            var cur = modelArray[index];
            float x = WaveHelper.SinWaveReductionEasing(cur.easingType, time, cur.duration, cur.amplitudeOffset.x, cur.frequency, 0);
            float y = WaveHelper.SinWaveReductionEasing(cur.easingType, time, cur.duration, cur.amplitudeOffset.y, cur.frequency, 0);
            float z = WaveHelper.SinWaveReductionEasing(cur.easingType, time, cur.duration, cur.amplitudeOffset.z, cur.frequency, 0);
            resOffset = new Vector3(x, y, z);

            if (time >= cur.duration) {
                time = 0;
                index += 1;
                resOffset = Vector3.zero;
            }

        }

        public Vector3 GetShakeOffset() {
            return resOffset;
        }

    }

}