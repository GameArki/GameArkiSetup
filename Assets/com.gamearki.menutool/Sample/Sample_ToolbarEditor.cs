using UnityEngine;
using UnityEditor;

namespace GameArki.MenuTool.Sample {

    [InitializeOnLoad]
    public static class Sample_ToolbarEditor {

        static Sample_ToolbarEditor() {

            ToolbarEditorCore.Initialize();

            ToolbarEditorCore.RegisterLeftGUIDraw(() => {
                bool has = GUILayout.Button("左1", GUILayout.Width(100));
                if (has) {
                    Debug.Log("左1");
                }

                has = GUILayout.Button("左2", GUILayout.Width(100));
                if (has) {
                    Debug.Log("左2");
                }
            });

            ToolbarEditorCore.RegisterRightGUIDraw(() => {
                bool has = GUILayout.Button("右1", GUILayout.Width(100));
                if (has) {
                    Debug.Log("右1");
                }

                has = GUILayout.Button("右2", GUILayout.Width(100));
                if (has) {
                    Debug.Log("右2");
                }
            });

        }

    }

}