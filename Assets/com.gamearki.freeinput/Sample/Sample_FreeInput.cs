using UnityEngine;

namespace GameArki.FreeInput.Test {

    public class Sample_FreeInput : MonoBehaviour {

        FreeInputCore core;
        ushort moveBindID = 0;
        public Transform role;

        void Awake() {
            core = new FreeInputCore();
        }

        void Update() {
            if (core.Getter.GetPressing(moveBindID)) {
                Debug.Log($"前进");
                role.transform.position += Vector3.forward * UnityEngine.Time.deltaTime;
            }

            var curKeyCode = core.Getter.GetCurrentDownKeyCode();
            if (curKeyCode != KeyCode.None) {
                Debug.Log($"当前输入按键 Down: {curKeyCode}");
            }
            curKeyCode = core.Getter.GetCurrentPressingKeyCode();
            if (curKeyCode != KeyCode.None) {
                Debug.Log($"当前输入按键 Pressing: {curKeyCode}");
            }
            curKeyCode = core.Getter.GetCurrentUpKeyCode();
            if (curKeyCode != KeyCode.None) {
                Debug.Log($"当前输入按键 Up: {curKeyCode}");
            }
        }

        void OnGUI() {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            if (GUILayout.Button("绑定 W")) core.Setter.Bind(moveBindID, KeyCode.W);
            if (GUILayout.Button("绑定 UpArrow")) core.Setter.Bind(moveBindID, KeyCode.UpArrow);
            if (GUILayout.Button("绑定 Keypad8")) core.Setter.Bind(moveBindID, KeyCode.Keypad8);
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            if (GUILayout.Button("解除绑定 W")) core.Setter.Unbind(moveBindID, KeyCode.W);
            if (GUILayout.Button("解除绑定 UpArrow")) core.Setter.Unbind(moveBindID, KeyCode.UpArrow);
            if (GUILayout.Button("解除绑定 Keypad8")) core.Setter.Unbind(moveBindID, KeyCode.Keypad8);
            if (GUILayout.Button("解除所有绑定")) core.Setter.UnbindAll();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            if (GUILayout.Button("换绑 W ---> U  ")) core.Setter.Rebind(moveBindID, KeyCode.W, KeyCode.U);
            if (GUILayout.Button("换绑 U ---> W  ")) core.Setter.Rebind(moveBindID, KeyCode.U, KeyCode.W);
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }

    }

}