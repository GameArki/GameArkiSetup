using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath.NET;
using GameArki.FPPhysics2D;

namespace GameArki.Sample {

    public class FPPhysics2DSampleApp : MonoBehaviour {

        GameObject[] allGo;
        FPRigidbody2DEntity[] allRB;

        FPSpace2D space2D;

        FPRigidbody2DEntity role => allRB[0];

        // Start is called before the first frame update
        void Awake() {

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            Console.SetOut(new UnityTextWriter());

            space2D = new FPSpace2D(new FPVector2(0, -981 * FP64.EN2), new FPVector2(1000, 500), 8);

            allGo = new GameObject[6];
            allRB = new FPRigidbody2DEntity[6];
            
            for (int i = 0; i < allGo.Length; i += 1) {
                allGo[i] = new GameObject("go" + i.ToString());
            }

            allRB[0] = FPRigidbody2DFactory.CreateBoxRB(new FPVector2(1, 3), 0, new FPVector2(1, 1));
            allRB[0].SetGravityScale(0);

            for (int i = 0; i < allRB.Length; i += 1) {
                var rb = allRB[i];
                if (rb != null) {
                    space2D.Add(rb);
                }
            }

            for (int i = 0; i < 10; i += 1) {
                for (int j = 0; j < 3; j += 1) {
                    var cell = FPRigidbody2DFactory.CreateBoxRB(new FPVector2(i, j), 0, new FPVector2(1, 1));
                    cell.SetStatic(true);
                    space2D.Add(cell);
                }
            }

            /*
            role.OnTriggerEnterHandle += (ev) => {
                System.Console.WriteLine("Trigger Enter");
            };

            bool stay1 = false;
            role.OnTriggerStayHandle += (ev) => {
                if (stay1) {
                    return;
                }
                System.Console.WriteLine("Trigger Stay");
                stay1 = true;
            };

            role.OnTriggerExitHandle += (ev) => {
                System.Console.WriteLine("Trigger Exit");
                stay1 = false;
            };

            role.OnCollisionEnterHandle += (ev) => {
                System.Console.WriteLine("Coll Enter");
            };

            bool stay2 = false;
            role.OnCollisionStayHandle += (ev) => {
                if (stay2) {
                    return;
                }
                System.Console.WriteLine("Coll Stay");
                stay2 = true;
            };

            role.OnCollisionExitHandle += (ev) => {
                System.Console.WriteLine("Coll Exit");
                stay2 = false;
            };
            */
            

        }

        // Update is called once per frame
        void Update() {
            
            var velo = role.LinearVelocity;
            velo.y -= 1;
            if (Input.GetKeyDown(KeyCode.Space)) {
                velo.y = 12;
            }

            if (Input.GetKey(KeyCode.A)) {
                velo.x = -5;
            } else if (Input.GetKey(KeyCode.D)) {
                velo.x = 5;
            } else {
                velo.x = 0;
            }

            role.SetLinearVelocity(velo);

            space2D.Tick(16 * FP64.EN3);
            
        }

        void OnDrawGizmos() {
            space2D?.GizmosDrawAllRigidbody();
        }

    }

    public class UnityTextWriter : TextWriter {

        public override Encoding Encoding => Encoding.UTF8;

        StringBuilder sb;

        public UnityTextWriter() {
            this.sb = new StringBuilder();
        }

        public override void Write(string value) { sb.Append(value); }
        public override void Write(ulong value) { sb.Append(value); }
        public override void Write(long value) { sb.Append(value); }
        public override void Write(uint value) { sb.Append(value); }
        public override void Write(int value) { sb.Append(value); }
        public override void Write(char value) { sb.Append(value); }
        public override void Write(float value) { sb.Append(value); }
        public override void Write(double value) { sb.Append(value); }
        public override void Write(decimal value) { sb.Append(value); }
        public override void Write(bool value) { sb.Append(value); }
        public override void Write(object value) { sb.Append(value); }

        public override void Flush() {
            Debug.Log(sb.ToString());
            sb.Clear();
        }

        public override void WriteLine(string value) { Debug.Log(value); }
        public override void WriteLine(ulong value) { Debug.Log(value); }
        public override void WriteLine(long value) { Debug.Log(value); }
        public override void WriteLine(uint value) { Debug.Log(value); }
        public override void WriteLine(int value) { Debug.Log(value); }
        public override void WriteLine(char value) { Debug.Log(value); }
        public override void WriteLine(float value) { Debug.Log(value); }
        public override void WriteLine(double value) { Debug.Log(value); }
        public override void WriteLine(decimal value) { Debug.Log(value); }
        public override void WriteLine(bool value) { Debug.Log(value); }
        public override void WriteLine(object value) { Debug.Log(value); }

    }

}
