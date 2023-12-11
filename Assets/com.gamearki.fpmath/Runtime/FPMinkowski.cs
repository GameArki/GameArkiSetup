using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.FPMath {

    public static class FPMinkowski2D {

        public static int Sum_CulledPolygon(Vector2[] aVectors, Vector2[] bVectors, Vector2[] culledPolygon) {

            int resCount = 0;

            // A ∪ B
            int aLen = aVectors.Length;
            int bLen = bVectors.Length;

            int aBottomIndex = BottomIndex(aVectors);
            int bBottomIndex = BottomIndex(bVectors);
            culledPolygon[resCount++] = aVectors[aBottomIndex] + bVectors[bBottomIndex];
            int aCurIndex = (aBottomIndex + 1) % aLen;
            int bCurIndex = (bBottomIndex + 1) % bLen;

            while (resCount < aLen + bLen) {

                culledPolygon[resCount] = aVectors[aCurIndex] + bVectors[bCurIndex];
                resCount += 1;

                int aNextIndex = (aCurIndex + 1) % aLen;
                int bNextIndex = (bCurIndex + 1) % bLen;

                float cross = Cross(aVectors[aNextIndex] - aVectors[aCurIndex], bVectors[bNextIndex] - bVectors[bCurIndex]);

                if (cross >= 0 && aCurIndex != aBottomIndex) {
                    aCurIndex = aNextIndex;
                }
                if (cross <= 0 && bCurIndex != bBottomIndex) {
                    bCurIndex = bNextIndex;
                }

            }

            return resCount;
        }

        static float Cross(Vector2 a, Vector2 b) {
            return a.x * b.y - a.y * b.x;
        }

        static int BottomIndex(Vector2[] vectors) {
            int res = 0;
            for (int i = 0; i < vectors.Length; i += 1) {
                if (vectors[i].y < vectors[res].y || (vectors[i].y == vectors[res].y && vectors[i].x < vectors[res].x)) {
                    res = i;
                }
            }
            return res;
        }

        public static int Sum_FullPolygon(Vector2[] aVectors, Vector2[] bVectors, Vector2[] fullPolygon) {
            int resIndex = 0;
            for (int i = 0; i < aVectors.Length; i += 1) {
                for (int j = 0; j < bVectors.Length; j += 1) {
                    fullPolygon[resIndex] = aVectors[i] + bVectors[j];
                    resIndex += 1;
                }
            }
            return resIndex;
        }

    }

}
