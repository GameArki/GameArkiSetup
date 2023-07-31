using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.PlatformerCamera.Sample {

    public class PFSample : MonoBehaviour {

        PFCore pfCore;

        GameObject tar;

        void Awake() {

            tar = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tar.name = "FollowTF";

            pfCore = new PFCore();
            pfCore.Initialize(Camera.main);
            _ = pfCore.SetterAPI.SpawnByMain(5);
            pfCore.SetterAPI.Confiner_Set_Current(true, new Vector2(-20, -20), new Vector2(20, 20));

            pfCore.SetterAPI.Follow_Current(tar.transform, new Vector3(0, 0, -10), EasingType.OutExpo, 2f, EasingType.Linear, 1f);
        }

        void OnGUI() {
            if (GUI.Button(new Rect(10, 10, 100, 30), "ShakeOnce")) {
                pfCore.SetterAPI.ShakeOnce_Current(new PFShakeStateModel(new Vector2(0.25f, 0.25f), EasingType.OutExpo, 50f, 0.5f));
            }
        }

        void Update() {

            var dir = Vector2.zero;
            float speed = 10f;

            if (Input.GetKey(KeyCode.A)) {
                dir.x = -speed;
            } else if (Input.GetKey(KeyCode.D)) {
                dir.x = speed;
            }

            if (Input.GetKey(KeyCode.W)) {
                dir.y = speed;
            } else if (Input.GetKey(KeyCode.S)) {
                dir.y = -speed;
            }

            if (dir != Vector2.zero) {
                pfCore.SetterAPI.Move_Current(dir * Time.deltaTime);
            }

        }

        void LateUpdate() {
            float dt = Time.deltaTime;
            pfCore.Tick(dt);
        }

        void OnDrawGizmos() {
            pfCore?.Editor_DrawGizmos();
        }

    }

}