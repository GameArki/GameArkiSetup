/*
based on follow license:

MIT License

Copyright (c) 2019 Seyed Morteza Kamali

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameArki.MenuTool {

    public static class ToolbarEditorCore {

        static List<Action> LeftToolbarGUI;
        static List<Action> RightToolbarGUI;

        static ToolbarEditorCore() {
            LeftToolbarGUI = new List<Action>();
            RightToolbarGUI = new List<Action>();
        }

        public static void RegisterLeftGUIDraw(Action gui) {
            LeftToolbarGUI.Add(gui);
        }

        public static void RegisterRightGUIDraw(Action gui) {
            RightToolbarGUI.Add(gui);
        }

        public static void Initialize() {
            EditorApplication.update += Init;
            EditorApplication.update -= ToolbarCallback.Initialize;
            EditorApplication.update += ToolbarCallback.Initialize;
            EditorApplication.playModeStateChanged += OnChangePlayMode;
        }

        static void OnChangePlayMode(PlayModeStateChange state) {
            if (state == PlayModeStateChange.EnteredPlayMode) {
                EditorApplication.playModeStateChanged -= OnChangePlayMode;
                InitElements();
            }
        }

        static void Init() {
            EditorApplication.update -= Init;
            InitElements();
        }

        static void InitElements() {
            ToolbarCallback.OnToolbarGUILeft -= GUILeft;
            ToolbarCallback.OnToolbarGUILeft += GUILeft;
            ToolbarCallback.OnToolbarGUIRight -= GUIRight;
            ToolbarCallback.OnToolbarGUIRight += GUIRight;
        }

        static void GUILeft() {
            GUILayout.BeginHorizontal();
            foreach (var handler in LeftToolbarGUI) {
                handler();
            }
            GUILayout.EndHorizontal();
        }

        static void GUIRight() {
            GUILayout.BeginHorizontal();
            foreach (var handler in RightToolbarGUI) {
                handler();
            }
            GUILayout.EndHorizontal();
        }
    }
}
