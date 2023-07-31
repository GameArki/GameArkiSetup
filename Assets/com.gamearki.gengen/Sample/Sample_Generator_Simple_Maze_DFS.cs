using System;
using UnityEngine;

namespace GameArki.GenGen.Sample {

    public class Sample_Generator_Simple_Maze_DFS : MonoBehaviour {

        GGSimpleMazeDFSGenerator generator;
        [SerializeField] Vector2Int size;

        void Start() {
            generator = new GGSimpleMazeDFSGenerator();
            generator.Input(size.x, size.y, 0, 0);
            generator.GenInstant();
        }

        void Update() {

            if (Input.GetKeyDown(KeyCode.Space)) {
                generator.GenByStep();
            }

        }

        void OnDrawGizmos() {

            if (generator == null) {
                return;
            }

            ReadOnlySpan<int> res = generator.GetMap();
            if (res.Length == 0) {
                return;
            }

            GizmosDrawHelper.DrawMaze(res, generator.CurPos, generator.Size, new Vector2(0.3f, 0.3f));

        }

    }

}