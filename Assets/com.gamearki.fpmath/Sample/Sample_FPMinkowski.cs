using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.FPMath.Sample {

    [ExecuteInEditMode]
    public class Sample_FPMinkowski : MonoBehaviour {

        [SerializeField] Vector2[] aVectors;
        [SerializeField] Vector2[] bVectors;
        Vector2[] results;

        void Awake() {
            results = new Vector2[10000];
        }

        // Update is called once per frame
        void Update() {
        }

        void OnDrawGizmos() {
            if (aVectors == null || bVectors == null) return;
            // Draw A
            Gizmos.color = Color.green;
            for (int i = 0; i < aVectors.Length; i++) {
                Vector2 curP = aVectors[i];
                Vector2 nextP = aVectors[(i + 1) % aVectors.Length];
                Gizmos.DrawLine(curP, nextP);
            }
            // Draw B
            Gizmos.color = Color.green;
            for (int i = 0; i < bVectors.Length; i++) {
                Vector2 curP = bVectors[i];
                Vector2 nextP = bVectors[(i + 1) % bVectors.Length];
                Gizmos.DrawLine(curP, nextP);
            }

            // Draw Sum
            int sumLength = FPMinkowski2D.Sum_CulledPolygon(aVectors, bVectors, results);
            Gizmos.color = Color.yellow;
            for (int i = 0; i < sumLength; i++) {
                Vector2 point = results[i];
                Vector2 next = results[(i + 1) % sumLength];
                Gizmos.DrawLine(point, next);
            }

            // Draw Diff
            int resultLength = FPMinkowski2D.Diff_CulledPolygon(aVectors, bVectors, results);
            Gizmos.color = Color.red;
            for (int i = 0; i < resultLength; i++) {
                Vector2 point = results[i];
                Vector2 next = results[(i + 1) % resultLength];
                Gizmos.DrawLine(point, next);
            }

            Gizmos.DrawSphere(Vector2.zero, 1f);
        }
    }
}
