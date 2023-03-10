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

        ManifestEntity manifest;

        SetupPackageCollection collection;

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
            collection = new SetupPackageCollection();
            manifest.ReadFromManifest();

        }

        Vector2 scrollPos;
        void OnGUI() {

            var gamearkiPackages = collection.packages;

            using (new GUILayout.HorizontalScope()) {
                GUILayout.FlexibleSpace();
                var content = new GUIContent("GameArki Setup", "GameArki Setup");
                GUILayout.Label(content, GUILayout.ExpandWidth(true), GUILayout.Height(24));
                GUILayout.FlexibleSpace();
            }
            GUILayout.BeginHorizontal();
            GUILayout.Label("如遇到问题, 请选阅读文档: ", GUILayout.ExpandWidth(false));
            if (GUILayout.Button("文档", GUILayout.ExpandWidth(false), GUILayout.Width(40))) {
                Application.OpenURL("https://www.github.com/gamearki/GameArkiSetup");
            }
            GUILayout.EndHorizontal();

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            for (int i = 0; i < gamearkiPackages.Length; i += 1) {
                var pkg = gamearkiPackages[i];
                drawer.GUI_DrawOne(manifest, gamearkiPackages, pkg);
            }
            GUILayout.EndScrollView();

            if (drawer.IsDirty) {
                UnityEditor.PackageManager.Client.Resolve();
                drawer.SetDirty(false);
            }
        }

    }

    public class PackageDrawer {

        bool isDirty;
        public bool IsDirty => isDirty;
        public void SetDirty(bool value) => isDirty = value;

        Dictionary<string, bool> titleToggles = new Dictionary<string, bool>();
        Dictionary<string, bool> versionToggles = new Dictionary<string, bool>();
        Dictionary<string, bool> assemblyToggles = new Dictionary<string, bool>();
        Dictionary<string, bool> dependencyToggles = new Dictionary<string, bool>();

        public PackageDrawer() {
            this.isDirty = false;
        }

        public void GUI_DrawOne(ManifestEntity manifest, SetupPackageModel[] allPkgs, SetupPackageModel pkg) {

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
            titleToggles[title] = EditorGUILayout.Foldout(titleToggles[title], title + " - " + pkg.desc);
            if (titleToggles[title]) {

                var w_40 = GUILayout.Width(40);

                GUILayout.BeginHorizontal();
                GUILayout.Space(20);
                dependencyToggles[title] = EditorGUILayout.Foldout(dependencyToggles[title], "依赖列表");
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
                            if (installedVersion != version.version) {
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
                            GUI_ClickInstall(manifest, allPkgs, pkg, version.version);
                        }
                        GUILayout.Label(version.version + version.suffix);
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
                    GUI_ClickInstall(manifest, allPkgs, pkg, "main");
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

        void GUI_ClickInstall(ManifestEntity manifest, SetupPackageModel[] allPkgs, SetupPackageModel pkg, string version) {
            var pkgName = pkg.name;
            bool has = manifest.HasDependency(pkgName);
            string value = pkg.BakeValue(version);
            if (has) {
                manifest.UpdateDependency(pkgName, value);
            } else {
                manifest.AddDependency(pkgName, value);
            }
            manifest.SaveManifest();
            this.isDirty = true;
        }

        void GUI_ClickUninstall(ManifestEntity manifest, SetupPackageModel pkg) {
            var pkgName = pkg.name;
            manifest.RemoveDependency(pkgName);
            manifest.SaveManifest();
            this.isDirty = true;
        }

    }

}