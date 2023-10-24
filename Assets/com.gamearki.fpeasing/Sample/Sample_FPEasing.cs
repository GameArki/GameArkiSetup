using UnityEngine;

namespace GameArki.FPEasing.Sample {

    public class Sample_FPEasing : MonoBehaviour {

        Vector2[] points;

        int a;

        void Awake() {
            points = new Vector2[360];
            a = 10;
        }

        float rest;
        void Update() {
            rest += Time.deltaTime;
            for (; rest > 0.01f; rest -= 0.01f) {
                Fix(0.01f);
            }
        }

        float t;
        void Fix(float fixdt) {

            // t: x
            // v: y
            float v = FunctionHelper.EaseInSine(t);

            int index = (int)(t * 360);
            points[index] = new Vector2(t, v);

            t += fixdt;
            if (t > 1) {
                t = 0;
            }

            Debug.Assert(a == 10);

        }

        void OnGUI() {
            if (points == null) {
                return;
            }

            for (int i = 0; i < points.Length; i += 1) {
                Vector2 point = points[i];
                GUI.Label(new Rect(100, 100 + i * 15, 1000, 100), "x: " + point.x + "                    y: " + point.y);
            }
        }

        void OnDrawGizmos() {
            if (points == null) {
                return;
            }

            Gizmos.color = Color.red;
            for (int i = 0; i < points.Length; i += 1) {
                Vector2 point = points[i];
                Gizmos.DrawSphere(point, 0.1f);
            }
        }

    }
}