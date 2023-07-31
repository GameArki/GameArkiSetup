using System;
using UnityEngine;

namespace GameArki.GenGen.Sample {

    public class Sample_Generator_Simple_Maze_Eller : MonoBehaviour {

        GGPerfectMazeEllerGenerator generator;

        [SerializeField] Vector2Int size;
        [SerializeField] int setTypeCount;

        [SerializeField] float intervalMax;

        bool isStart;
        float interval;
        
        void Start() {
            generator = new GGPerfectMazeEllerGenerator();
            generator.Input(size.x, size.y, setTypeCount);
            // generator.GenInstant();
        }

        void Update() {

            if (Input.GetKeyDown(KeyCode.Space)) {
                isStart = !isStart;
            }

            if (isStart) {
                interval += Time.deltaTime;
                if (interval > intervalMax) {
                    interval = 0;
                    generator.GenByStep();
                }
            }
        }

        static Color[] colors = new Color[] {
            Color.yellow,
            Color.red,
            Color.blue,
            Color.cyan,
            Color.magenta,
            Color.gray,
            Color.white,
        };
        void OnDrawGizmos() {
            if (generator == null) {
                return;
            }

            ReadOnlySpan<int> map = generator.GetMap();
            var mapSize = generator.Size;
            Vector2 cubeSize = new Vector2(0.3f, 0.3f);
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

            var curLine = generator.GetLine();
            for (int i = 0; i < curLine.Length; i += 1) {
                var value = curLine[i];
                GizmosDrawHelper.DrawCube(new Vector2(i * 2, -1), new Vector2(0.3f, 0.3f), colors[value.SetIndex]);
            }

            var nextLine = generator.GetNextLine();
            for (int i = 0; i < nextLine.Length; i += 1) {
                var value = nextLine[i];
                GizmosDrawHelper.DrawCube(new Vector2(i * 2, -2), new Vector2(0.3f, 0.3f), colors[value.SetIndex]);
            }

            color = Color.green;
            Vec2Int curPos = generator.Pos;
            GizmosDrawHelper.DrawCube(new Vector2(curPos.x, curPos.y), cubeSize, color);

        }

    }

}