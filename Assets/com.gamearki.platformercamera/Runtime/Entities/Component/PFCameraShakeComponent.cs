using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.PlatformerCamera.Entities {

    public class PFCameraShakeStateComponent {

        // Args
        PFShakeStateModel[] arr;
        int index;

        // Temp
        Vector3 resOffset;
        float time;

        public PFCameraShakeStateComponent() { }

        public void ShakeOnce(PFShakeStateModel arg) {
            this.arr = new PFShakeStateModel[] { arg };
            SetShake(arr);
        }

        public void SetShake(PFShakeStateModel[] args) {
            this.arr = args;
            this.index = 0;
            this.time = 0;
            resOffset = Vector3.zero;
        }

        public void TickEasing(float dt) {

            if (arr == null || index >= arr.Length) {
                return;
            }

            time += dt;

            var cur = arr[index];
            float x = WaveHelper.SinWaveReductionEasing(cur.easingType, time, cur.duration, cur.amplitudeOffset.x, cur.frequency, 0);
            float y = WaveHelper.SinWaveReductionEasing(cur.easingType, time, cur.duration, cur.amplitudeOffset.y, cur.frequency, 0);
            resOffset.x = x;
            resOffset.y = y;

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