using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;

namespace GameArki.Setup {

    public static class SetupEditorWindowMenu {

        [MenuItem("GameArki/SetupWindow")]
        public static void ShowWindow() {
            SetupEditorWindow window = EditorWindow.GetWindow<SetupEditorWindow>("GameArki Setup");
            window.Show();
        }

    }

    // 1. 比对项目中的 manifest.json 和 
    public class SetupEditorWindow : EditorWindow {

        void OnEnable() {

        }

        void OnDisable() {

        }

        void OnGUI() {

        }

    }

}