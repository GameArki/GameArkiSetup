using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.CodeEditor;
using VSCodeEditor;

namespace GameArki.MenuTool {

    public static class CSProjGenerator {

        [MenuItem(nameof(GameArki) + "/重新生成 CSProj")]
        public static void CleanCSProj() {

            List<string> files = FindAllFileWithExt(Environment.CurrentDirectory, "*.csproj");
            foreach (var file in files) {
                File.Delete(file);
            }
            Debug.Log("消除 CSProj 成功: " + files.Count.ToString());

            IExternalCodeEditor codeEditor = CodeEditor.CurrentEditor;
            VSCodeScriptEditor vSCodeScriptEditor = codeEditor as VSCodeScriptEditor;
            vSCodeScriptEditor.SyncAll();

            FieldInfo info = vSCodeScriptEditor.GetType().GetField("m_ProjectGeneration", BindingFlags.NonPublic | BindingFlags.Instance);
            IGenerator generator = info.GetValue(vSCodeScriptEditor) as IGenerator;
            generator.Sync();

            Debug.Log("重新生成了 CSProj");
        }

        static List<string> FindAllFileWithExt(string rootPath, string ext) {

            List<string> fileList = new List<string>();

            DirectoryInfo directoryInfo = new DirectoryInfo(rootPath);
            FileInfo[] allFiles = directoryInfo.GetFiles(ext);
            for (int i = 0; i < allFiles.Length; i += 1) {
                var file = allFiles[i];
                fileList.Add(file.FullName);
            }

            DirectoryInfo[] childrenDirs = directoryInfo.GetDirectories();
            for (int i = 0; i < childrenDirs.Length; i += 1) {
                var dir = childrenDirs[i];
                fileList.AddRange(FindAllFileWithExt(dir.FullName, ext));
            }

            return fileList;

        }

    }
}