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

        ManifestEntity manifest;

        PackageDrawer drawer;

        void OnEnable() {
            drawer = new PackageDrawer();
            LoadManifest();
        }

        void OnDisable() {
            manifest.Clear();
        }

        void LoadManifest() {

            manifest = new ManifestEntity();
            manifest.ReadFromManifest();

        }

        void OnGUI() {
            var gamearkiPackages = SetupPackageCollection.packages;
            for (int i = 0; i < gamearkiPackages.Length; i += 1) {
                var pkg = gamearkiPackages[i];
                drawer.GUI_DrawOne(manifest, pkg);
            }
        }

    }

    public class PackageDrawer {

        Dictionary<string, bool> titleToggles = new Dictionary<string, bool>();
        Dictionary<string, bool> versionToggles = new Dictionary<string, bool>();
        Dictionary<string, bool> assemblyToggles = new Dictionary<string, bool>();
        Dictionary<string, bool> dependencyToggles = new Dictionary<string, bool>();

        public void GUI_DrawOne(ManifestEntity manifest, SetupPackageModel pkg) {

            string title = pkg.title;
            string pkgName = pkg.name;

            bool hasInstalled = manifest.HasDependency(pkgName);
            string installedVersion = hasInstalled ? manifest.GetVersion(pkgName) : "";

            if (!titleToggles.ContainsKey(title)) {
                titleToggles.Add(title, true);
            }
            if (!versionToggles.ContainsKey(title)) {
                versionToggles.Add(title, false);
            }
            if (!assemblyToggles.ContainsKey(title)) {
                assemblyToggles.Add(title, false);
            }
            if (!dependencyToggles.ContainsKey(title)) {
                dependencyToggles.Add(title, false);
            }

            var w_100 = GUILayout.Width(100);

            GUILayout.BeginVertical(GUI.skin.box);
            titleToggles[title] = EditorGUILayout.Foldout(titleToggles[title], title);
            if (titleToggles[title]) {

                var w_40 = GUILayout.Width(40);

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                GUILayout.Label(pkg.desc);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                dependencyToggles[title] = EditorGUILayout.Foldout(dependencyToggles[title], "依赖外部库");
                GUILayout.EndHorizontal();
                if (dependencyToggles[title]) {
                    for (int i = 0; i < pkg.dependencies.Length; i += 1) {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(40);
                        GUILayout.Label(pkg.dependencies[i]);
                        GUILayout.EndHorizontal();
                    }
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                assemblyToggles[title] = EditorGUILayout.Foldout(assemblyToggles[title], "内部程序集");
                GUILayout.EndHorizontal();
                if (assemblyToggles[title]) {
                    for (int i = 0; i < pkg.assemblies.Length; i += 1) {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(40);
                        GUILayout.Label(pkg.assemblies[i]);
                        GUILayout.EndHorizontal();
                    }
                }

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                versionToggles[title] = EditorGUILayout.Foldout(versionToggles[title], "其他版本列表");
                GUILayout.EndHorizontal();
                if (versionToggles[title]) {
                    for (int i = 0; i < pkg.versions.Length; i += 1) {
                        var version = pkg.versions[i];
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(40);
                        bool hasClickInstall;
                        if (hasInstalled) {
                            if (installedVersion != version) {
                                hasClickInstall = GUILayout.Button("更新", w_40);
                            } else {
                                hasClickInstall = false;
                                if (GUILayout.Button("卸载", w_40)) {
                                    GUI_ClickUninstall(manifest, pkg);
                                }
                            }
                        } else {
                            hasClickInstall = GUILayout.Button("安装", w_40);
                        }
                        if (hasClickInstall) {
                            GUI_ClickInstall(manifest, pkg, version);
                        }
                        GUILayout.Label(version);
                        GUILayout.EndHorizontal();
                    }
                }

            }

            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            if (GUILayout.Button("查看文档", w_100)) {
                Application.OpenURL(pkg.docuUrl);
            }

            if (!hasInstalled) {
                GUILayout.Space(10);
                if (GUILayout.Button("安装最新版", w_100)) {
                    GUI_ClickInstall(manifest, pkg, "main");
                }
            } else {
                GUILayout.Space(10);
                if (GUILayout.Button("卸载", w_100)) {
                    GUI_ClickUninstall(manifest, pkg);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        void GUI_ClickInstall(ManifestEntity manifest, SetupPackageModel pkg, string version) {
            var pkgName = pkg.name;
            bool has = manifest.HasDependency(pkgName);
            string value = pkg.BakeValue(version);
            if (has) {
                manifest.UpdateDependency(pkgName, value);
            } else {
                manifest.AddDependency(pkgName, value);
            }
            manifest.SaveManifest();
            UnityEditor.PackageManager.Client.Resolve();
        }

        void GUI_ClickUninstall(ManifestEntity manifest, SetupPackageModel pkg) {
            var pkgName = pkg.name;
            manifest.RemoveDependency(pkgName);
            manifest.SaveManifest();
            UnityEditor.PackageManager.Client.Resolve();
        }

    }

}