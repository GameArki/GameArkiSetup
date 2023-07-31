using System;
using UnityEngine;

namespace GameArki.GenGen.Sample {

    public static class GizmosDrawHelper {

        public static void DrawCube(Vector3 pos, Vector3 size, Color color) {
            Gizmos.color = color;
            Gizmos.DrawCube(new Vector3(pos.x * size.x, pos.y * size.y), size);
        }

        public static void DrawMaze(ReadOnlySpan<int> map, Vec2Int curPos, Vec2Int mapSize, Vector2 cubeSize) {
            Vec2Int pos;
            Color color = Color.black;
            for (int i = 0; i < map.Length; i += 1) {
                int value = map[i];
                pos = Vec2Int.FromArrayIndex(i, mapSize.x);
                Vector2 drawPos = new Vector2(pos.x, pos.y);
                if (value == GGSimpleMazeDFSGenerator.NODE_WALL) {
                    color = Color.black;
                } else if (value == GGSimpleMazeDFSGenerator.NODE_ROAD) {
                    color = Color.white;
                }
                GizmosDrawHelper.DrawCube(drawPos, cubeSize, color);
            }

            color = Color.green;
            GizmosDrawHelper.DrawCube(new Vector2(curPos.x, curPos.y), cubeSize, color);
        }

    }

}