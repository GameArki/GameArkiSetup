using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameArki.Setup.Editors {

#if GAMEARKI_DEV
    [CreateAssetMenu(fileName = "SetupSo", menuName = "GameArki/SetupSo", order = 0)]
#endif
    public class SetupSo : ScriptableObject {

        public List<SetupModel> all;

    }

    [CustomEditor(typeof(SetupSo))]
    public class SetupSoInspector : Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (GUILayout.Button("Scan")) {
                var so = target as SetupSo;
                Scan(so);
            }
        }

        void Scan(SetupSo so) {
            // Scan `Environment.CurrentDirectory/Assets` + which are start with `com.gamearki`
            var rootDir = Environment.CurrentDirectory;
            var dirs = Directory.GetDirectories(rootDir, "com.gamearki*", SearchOption.AllDirectories);
            so.all = new List<SetupModel>();
            for (int i = 0; i < dirs.Length; i++) {
                var dir = dirs[i];
                var dirName = Path.GetFileName(dir);
                if (dirName == "com.gamearki.framework.setup") {
                    continue;
                }
                var setupModel = new SetupModel();
                setupModel.rootDir = dirName;
                // Find all dirs
                var childDirs = Directory.GetDirectories(dir);
                if (childDirs.Length > 0) {
                    setupModel.dirs = new List<string>();
                }
                for (int j = 0; j < childDirs.Length; j += 1) {
                    var childDir = childDirs[j];
                    var childDirName = Path.GetFileName(childDir);
                    setupModel.dirs.Add(childDirName);
                }

                // Find package.json
                var packageJsonPath = Path.Combine(dir, "package.json");
                Debug.Log(packageJsonPath);
                if (File.Exists(packageJsonPath)) {
                    var packageJson = File.ReadAllText(packageJsonPath);
                    var json = JsonUtility.FromJson<SetupPackageJsonModel>(packageJson);
                    setupModel.packageName = json.displayName;
                    setupModel.version = json.version;
                    setupModel.desc = json.description;
                }

                so.all.Add(setupModel);
            }

        }

    }

}