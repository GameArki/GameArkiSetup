using System;

namespace GameArki.GenGen {

    public static class GGCycleHelper {

        // 正弦波衰减
        public static float SinWaveReduction(float t, float duaraion, float amplitude, float frequency, float waveOffset) {
            float timePercent = t / duaraion;
            float amp = (1 - timePercent) * amplitude;
            return amp * MathF.Sin(frequency * t + waveOffset);
        }

        // 取模
        public static float Modulo(float v, float max) {
            return v % max;
        }

    }

}