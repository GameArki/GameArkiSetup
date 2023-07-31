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
using UnityEngine;
using UnityEditor;
using System.Reflection;
using UnityEngine.UIElements;

namespace GameArki.MenuTool {

    internal static class ToolbarCallback {

        static ScriptableObject m_currentToolbar;

        /// <summary>
        /// Callback for toolbar OnGUI method.
        /// </summary>
        public static event Action OnToolbarGUILeft;
        public static event Action OnToolbarGUIRight;

        internal static void Initialize() {

            if (m_currentToolbar != null) {
                return;
            }

            // Find toolbar
            Type m_toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
            var toolbars = Resources.FindObjectsOfTypeAll(m_toolbarType);
            m_currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
            if (m_currentToolbar == null) {
                Debug.LogError("Toolbar not found");
                return;
            }

            var root = m_currentToolbar.GetType().GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
            var rawRoot = root.GetValue(m_currentToolbar);
            var mRoot = rawRoot as VisualElement;
            RegisterCallback(mRoot, "ToolbarZoneLeftAlign", OnToolbarGUILeft);
            RegisterCallback(mRoot, "ToolbarZoneRightAlign", OnToolbarGUIRight);

            // Get first child which 'happens' to be toolbar IMGUIContainer
            Type m_guiViewType = typeof(Editor).Assembly.GetType("UnityEditor.GUIView");
            PropertyInfo m_windowBackend = m_guiViewType.GetProperty("windowBackend", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var windowBackend = m_windowBackend.GetValue(m_currentToolbar);
            Type m_iWindowBackendType = typeof(Editor).Assembly.GetType("UnityEditor.IWindowBackend");
            PropertyInfo m_viewVisualTree = m_iWindowBackendType.GetProperty("visualTree", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var visualTree = (VisualElement)m_viewVisualTree.GetValue(windowBackend, null);
            var container = (IMGUIContainer)visualTree[0];

            // (Re)attach handler
            // FieldInfo m_imguiContainerOnGui = typeof(IMGUIContainer).GetField("m_OnGUIHandler", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            // var handler = (Action)m_imguiContainerOnGui.GetValue(container);
            // handler -= OnGUI;
            // handler += OnGUI;
            // m_imguiContainerOnGui.SetValue(container, handler);

        }

        static void RegisterCallback(VisualElement mRoot, string rootName, Action cb) {

            var toolbarZone = mRoot.Q(rootName);

            var parent = new VisualElement() {
                style = {
                    flexGrow = 1,
                    flexDirection = FlexDirection.Row,
                }
            };
            var container = new IMGUIContainer();
            container.onGUIHandler += () => {
                cb?.Invoke();
            };
            parent.Add(container);
            toolbarZone.Add(parent);

        }

    }
}
