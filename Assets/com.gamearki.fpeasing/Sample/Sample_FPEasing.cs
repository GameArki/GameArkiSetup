using UnityEngine;

namespace GameArki.FPEasing.Sample {

    public class Sample_FPEasing : MonoBehaviour {

        Vector2[] points;

        void Awake() {
            points = new Vector2[360];
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
            float v = FunctionHelper.EaseInOutBounce(t);

            int index = (int)(t * 360);
            points[index] = new Vector2(t, v);

            t += fixdt;
            if (t > 1) {
                t = 0;
            }
        }

        void OnGUI() {
            if (points == null) {
                return;
            }

            int step = 0;
            string s = "";
            for (int i = 0; i < points.Length; i += 10) {
                Vector2 point = points[i];
                if (point.x != 0) {
                    GUI.Label(new Rect(100, 100 + step * 15, 1000, 100),
                        "x: " + point.x + "                    y: " + point.y);
                    s += "Test_RunFunction(func," + point.x + "f," + point.y + "f);\n\n";
                    step++;
                }
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