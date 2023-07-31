using UnityEngine;

namespace GameArki.FPEasing {

    public static class WaveHelper {

        public static float SinWave(float t, float amplitude, float frequency, float waveOffset) {
            return amplitude * Mathf.Sin(frequency * t + waveOffset);
        }

        public static float SinWaveReduction(float t, float duaraion, float amplitude, float frequency, float waveOffset) {
            float timePercent = t / duaraion;
            float amp = (1 - timePercent) * amplitude;
            return amp * Mathf.Sin(frequency * t + waveOffset);
        }

        public static float SinWaveReductionEasing(EasingType easingType, float t, float duaraion, float amplitude, float frequency, float waveOffset) {
            float timePercent = t / duaraion;
            timePercent = EasingHelper.Ease1D(easingType, t, duaraion, 0, timePercent);
            float amp = (1 - timePercent) * amplitude;
            return amp * Mathf.Sin(frequency * t + waveOffset);
        }
        
    }

}