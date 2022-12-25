using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;

namespace GameArki.Setup {

    public class ManifestEntity {

        Dictionary<string, string> dependencies;

        public ManifestEntity() {
            this.dependencies = new Dictionary<string, string>();
        }

        public bool HasDependency(string key) {
            return dependencies.ContainsKey(key);
        }

        public void ForEachAllDependencies(Action<string, string> action) {
            foreach (var item in dependencies) {
                action(item.Key, item.Value);
            }
        }

        public string GetVersion(string key) {
            string value = "";
            bool has = dependencies.TryGetValue(key, out value);
            if (has) {
                var arr= value.Split('#');
                if (arr.Length > 1) {
                    string version = arr[1];
                    return version;
                }
            }
            return "";
        }

        public void AddDependency(string key, string value) {
            dependencies.Add(key, value);
        }

        public void RemoveDependency(string key) {
            dependencies.Remove(key);
        }

        public void UpdateDependency(string key, string value) {
            dependencies[key] = value;
        }

        public void Clear() {
            dependencies.Clear();
        }

        // ==== Manage ====
        string GetManifestPath() {
            var assetsPath = Application.dataPath;
            DirectoryInfo dir = new DirectoryInfo(assetsPath);
            var root = dir.Parent.FullName;
            var manifestPath = Path.Combine(root, "Packages/manifest.json");
            return manifestPath;
        }

        enum Status { None, KeyStart, KeyEnd, ValueStart, ValueEnd }
        public void ReadFromManifest() {

            var txt = File.ReadAllText(GetManifestPath());

            dependencies.Clear();

            txt = txt.Split("\"dependencies\"")[1];
            txt = Regex.Replace(txt, @"\s", "");

            var status = Status.None;
            int cur = 0;
            string tmpKey = "";
            for (int i = 0; i < txt.Length; i += 1) {
                var c = txt[i];
                // 开始或结束
                if (c == '\"') {
                    if (status == Status.None || status == Status.ValueEnd) {
                        status = Status.KeyStart;
                        cur = i + 1;
                    } else if (status == Status.KeyStart) {
                        status = Status.KeyEnd;
                        tmpKey = txt.Substring(cur, i - cur);
                        cur = i + 1;
                    } else if (status == Status.KeyEnd) {
                        status = Status.ValueStart;
                        cur = i + 1;
                    } else if (status == Status.ValueStart) {
                        status = Status.ValueEnd;
                        var tmpValue = txt.Substring(cur, i - cur);
                        dependencies.Add(tmpKey, tmpValue);
                        cur = i + 1;
                    }
                }
            }
        }

        public void SaveManifest() {
            string str = "{\r\n"
                       + "  \"dependencies\": {\r\n";
            int count = 0;
            foreach (var item in dependencies) {
                str += "    \"" + item.Key + "\": \"" + item.Value + "\",\r\n";
                if (count == dependencies.Count - 1) {
                    str = str.Substring(0, str.Length - 3);
                    str += "\r\n";
                }
                count += 1;
            }
            str += "  }\r\n"
                 + "}\r\n";
            File.WriteAllText(GetManifestPath(), str);
        }

    }

}