using System;
using UnityEngine;

namespace GameArki.GenGen.Sample {

    public class Sample_Cycle_Modulo : MonoBehaviour {

        [SerializeField] bool active;

        [SerializeField] Vector2Int board = new Vector2Int(50, 50);
        [SerializeField] float cycleDuration;

        void Awake() {
            Console.SetOut(new UnityTextWriter());
        }

        void OnDrawGizmos() {

            if (!active) {
                return;
            }

            Vector2 size = new Vector2(0.1f, 0.1f);
            for (int x = 0; x < board.x; x += 1) {
                for (int y = 0; y < board.y; y += 1) {
                    float c = GGCycleHelper.Modulo(x * y, cycleDuration);
                    Gizmos.color = new Color(c, c, c, 1);
                    Vector3 pos = new Vector3(x * size.x, y * size.y, 0);
                    Gizmos.DrawCube(pos, size);
                }
            }

        }

    }

}