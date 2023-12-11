using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.FPMath {

    public static class FPMinkowski2D {

        /// <summary> return the count of the result polygon </summary>
        public static int Sum_CulledPolygon(Span<Vector2> aConvex, Span<Vector2> bConvex, Vector2[] culledPolygon) {

            int resCount = 0;

            int aLen = aConvex.Length;
            int bLen = bConvex.Length;

            int aBottomIndex = BottomIndex(aConvex);
            int bBottomIndex = BottomIndex(bConvex);
            culledPolygon[resCount++] = aConvex[aBottomIndex] + bConvex[bBottomIndex];
            int aCurIndex = (aBottomIndex + 1) % aLen;
            int bCurIndex = (bBottomIndex + 1) % bLen;

            while (resCount < aLen + bLen) {

                culledPolygon[resCount] = aConvex[aCurIndex] + bConvex[bCurIndex];
                resCount += 1;

                int aNextIndex = (aCurIndex + 1) % aLen;
                int bNextIndex = (bCurIndex + 1) % bLen;

                float cross = Cross(aConvex[aNextIndex] - aConvex[aCurIndex], bConvex[bNextIndex] - bConvex[bCurIndex]);

                if (cross >= 0 && aCurIndex != aBottomIndex) {
                    aCurIndex = aNextIndex;
                }
                if (cross <= 0 && bCurIndex != bBottomIndex) {
                    bCurIndex = bNextIndex;
                }

            }

            return resCount;
        }

        /// <summary> return the count of the result polygon </summary>
        public static int Diff_CulledPolygon(Span<Vector2> aVectors, Span<Vector2> bVectors, Vector2[] culledPolygon) {

            Span<Vector2> bReversed = stackalloc Vector2[bVectors.Length];
            for (int i = 0; i < bVectors.Length; i += 1) {
                bReversed[i] = -bVectors[i];
            }

            return Sum_CulledPolygon(aVectors, bReversed, culledPolygon);

        }

        static float Cross(Vector2 a, Vector2 b) {
            return a.x * b.y - a.y * b.x;
        }

        static int BottomIndex(Span<Vector2> vectors) {
            int res = 0;
            for (int i = 0; i < vectors.Length; i += 1) {
                if (vectors[i].y < vectors[res].y || (vectors[i].y == vectors[res].y && vectors[i].x < vectors[res].x)) {
                    res = i;
                }
            }
            return res;
        }

        // public static int Sum_FullPolygon(Vector2[] aVectors, Vector2[] bVectors, Vector2[] fullPolygon) {
        //     int resIndex = 0;
        //     for (int i = 0; i < aVectors.Length; i += 1) {
        //         for (int j = 0; j < bVectors.Length; j += 1) {
        //             fullPolygon[resIndex] = aVectors[i] + bVectors[j];
        //             resIndex += 1;
        //         }
        //     }
        //     return resIndex;
        // }

    }

}
