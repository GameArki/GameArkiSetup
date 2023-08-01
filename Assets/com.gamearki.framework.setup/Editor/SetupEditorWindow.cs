using System;
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
        string hostPrefix = "github.com/GameArki/";
        string outputDir = "Plugins";

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

            GUILayout.BeginHorizontal();
            GUILayout.Label("Host Prefix: ", GUILayout.Width(80));
            hostPrefix = GUILayout.TextField(hostPrefix, GUILayout.Width(200));
            GUILayout.Label("Example: github.com/GameArki/", GUILayout.Width(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Output Dir: ", GUILayout.Width(80));
            outputDir = GUILayout.TextField(outputDir, GUILayout.Width(200));
            GUILayout.Label("Example: Plugins. It'll join Assets/", GUILayout.Width(200));
            GUILayout.EndHorizontal();

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
            GUILayout.BeginHorizontal();
            GUILayout.Label("导入: ", GUILayout.Width(40));
            try {
                for (int i = 0; i < model.dirs.Count; i += 1) {
                    var dir = model.dirs[i];
                    string packageName = model.packageName;
                    if (GUILayout.Button(dir, GUILayout.Width(100))) {
                        try {
                            string exe = "cmd";
                            packageName = packageName.Replace("GameArki.", "");
                            const string SETUP_GIT = "GameArkiSetup.git";
                            string args = "git clone " + "ssh://git@" + hostPrefix + SETUP_GIT;
                            UnityEditor.EditorUtility.DisplayProgressBar("Setup", exe + " " + args, 0.1f);
                            SetupProcess.StartProcess(exe, args);

                            // Copy Src Dir To Dest Dir
                            string output = Path.Combine(Application.dataPath, outputDir, packageName, dir);
                            if (Directory.Exists(output)) {
                                Directory.Delete(output, true);
                            }
                            Directory.CreateDirectory(output);

                            string gitRoot = Path.Combine(Environment.CurrentDirectory, "GameArkiSetup");
                            string src = Path.Combine(gitRoot, "Assets", model.rootDir, dir);
                            UnityEditor.EditorUtility.DisplayProgressBar("Copy Files", src + " to " + output, 0.1f);
                            string[] files = Directory.GetFiles(src, "*", SearchOption.AllDirectories);
                            foreach (var file in files) {
                                string dest = file.Replace(src, output);
                                string dirName = Path.GetDirectoryName(dest);
                                if (!Directory.Exists(dirName)) {
                                    Directory.CreateDirectory(dirName);
                                }
                                File.Copy(file, dest);
                            }

                            if (Directory.Exists(gitRoot)) {
                                Directory.Delete(gitRoot, true);
                            }
                        } finally {
                            UnityEditor.EditorUtility.ClearProgressBar();
                        }

                    }
                }
            } finally {
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }

            GUILayout.Space(5);

        }

    }

}