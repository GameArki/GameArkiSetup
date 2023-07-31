using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;

namespace GameArki.Setup.Editors {

    public static class SetupEditorWindowMenu {

        [MenuItem("GameArki/SetupWindow")]
        public static void ShowWindow() {
            SetupEditorWindow window = EditorWindow.GetWindow<SetupEditorWindow>("GameArki Setup");
            window.Show();
        }

    }

    // 1. 比对项目中的 manifest.json 和 
    public class SetupEditorWindow : EditorWindow {

        SetupSo so;
        Vector2 scrollPos;

        void OnEnable() {
            so = AssetDatabase.LoadAssetAtPath<SetupSo>("Assets/com.gamearki.framework.setup/Editor/SetupResource/SetupSo.asset");
            if (so == null) {
                Debug.LogError("Can't find SetupSo.asset");
            }
        }

        void OnDisable() {

        }

        void OnGUI() {
            if (so == null) {
                return;
            }

            scrollPos = GUILayout.BeginScrollView(scrollPos);

            // One SetupModel
            for (int i = 0; i < so.all.Count; i += 1) {
                var model = so.all[i];
                GUI_DrawOneSetupModel(model);
            }

            GUILayout.EndScrollView();
        }

        void GUI_DrawOneSetupModel(SetupModel model) {

            GUILayout.Box("fdsaf", GUILayout.ExpandWidth(true), GUILayout.Height(2));
            GUILayout.BeginVertical();
            // FontSize: 20
            GUILayout.Label("包名: " + model.packageName + " " + model.version, new GUIStyle() { fontSize = 16, normal = new GUIStyleState() { textColor = Color.white }, contentOffset = new Vector2(6, 0) });
            GUILayout.Label("描述: " + model.desc);

            // One SetupModel's dirs
            for (int i = 0; i < model.dirs.Count; i += 1) {
                var dir = model.dirs[i];
                GUI_DrawOneDir(dir);
            }
            GUILayout.EndVertical();

            GUILayout.Space(5);

        }

        void GUI_DrawOneDir(string dir) {
            // Align: Right
            GUILayout.BeginHorizontal();

            // One SetupModel's dir
            GUILayout.Button(dir, GUILayout.Width(100));

            GUILayout.EndHorizontal();
        }

    }

}