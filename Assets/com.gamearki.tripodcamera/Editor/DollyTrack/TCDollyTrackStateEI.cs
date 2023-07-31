using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameArki.TripodCamera.EditorTool {

    [CustomEditor(typeof(TCDollyTrackStateEG))]
    public class TCDollyTrackStateEI : Editor {

        const float sceneDollyRefreshInterval = 1f / 144f;
        readonly Color controlPointColor1 = new Color(0.33f, 0.8f, 0.66f);
        readonly Color controlPointColor2 = new Color(1.0f, 0.5f, 0f);

        TCDollyTrackStateEG dollyTrack;

        SerializedProperty soProperty;
        SerializedProperty emProperty;

        SerializedProperty bindDollyTFProperty;
        SerializedProperty trackWidthProperty;
        SerializedProperty tProperty;
        SerializedProperty isPlayingProperty;

        void OnEnable() {
            dollyTrack = (TCDollyTrackStateEG)target;
            soProperty = serializedObject.FindProperty("so");
            emProperty = serializedObject.FindProperty("em");

            bindDollyTFProperty = serializedObject.FindProperty("bindDollyTF");
            trackWidthProperty = serializedObject.FindProperty("trackWidth");
            tProperty = serializedObject.FindProperty("t");
            isPlayingProperty = serializedObject.FindProperty("isPlaying");
        }

        int cacheBezierSplineCount;
        public override void OnInspectorGUI() {
            serializedObject.Update();
            if (GUILayout.Button("Save")) {
                Save();
            }
            if (GUILayout.Button("Load")) {
                Load();
            }

            EditorGUILayout.PropertyField(soProperty, true);
            EditorGUILayout.PropertyField(emProperty, true);

            EditorGUILayout.PropertyField(bindDollyTFProperty);
            EditorGUILayout.PropertyField(trackWidthProperty);
            EditorGUILayout.PropertyField(tProperty);
            EditorGUILayout.PropertyField(isPlayingProperty);

            var bezier3D3Lines = dollyTrack.em.bezierSlineEMArray;
            if (bezier3D3Lines != null && bezier3D3Lines.Length != 0) {
                if (cacheBezierSplineCount == 0) {
                    cacheBezierSplineCount = bezier3D3Lines.Length;
                }

                var curBezierSplineCount = bezier3D3Lines.Length;
                if (curBezierSplineCount != cacheBezierSplineCount) {
                    dollyTrack.t = 0;
                    if (curBezierSplineCount >= 2 && curBezierSplineCount > cacheBezierSplineCount) {
                        var lastBezierSpline = bezier3D3Lines[curBezierSplineCount - 2];
                        var startPos = lastBezierSpline.start.position;
                        var endPos = lastBezierSpline.end.position;
                        var offset = endPos - startPos;

                        startPos += offset;
                        endPos += offset;
                        lastBezierSpline.start.SetPosition(startPos);
                        lastBezierSpline.end.SetPosition(endPos);
                        lastBezierSpline.c2 += offset;

                        var cOffset = endPos - lastBezierSpline.c2;
                        var cDirLen = cOffset.magnitude;
                        lastBezierSpline.c1 = startPos + cOffset;
                        bezier3D3Lines[curBezierSplineCount - 1] = lastBezierSpline;
                    }

                    cacheBezierSplineCount = curBezierSplineCount;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        void Save() {
            if (dollyTrack.so == null) return;
            dollyTrack.so.tm.needSet_DollyTrack = true;
            dollyTrack.so.tm.dollyTrackStateTM = TCEM2TMUtil.ToTCDollyTrackStateTM(dollyTrack.em);
            EditorUtility.SetDirty(dollyTrack.so);
            AssetDatabase.SaveAssets();
        }

        void Load() {
            if (dollyTrack.so == null) return;
            dollyTrack.em = TCTM2EMUtil.ToTCDollyTrackStateEM(dollyTrack.so.tm.dollyTrackStateTM);
        }

        void OnSceneGUI() {
            var bezier3D3Lines = dollyTrack.em.bezierSlineEMArray;
            if (bezier3D3Lines == null || bezier3D3Lines.Length == 0) {
                return;
            }

            DrawTrackPositionHandle();
            DrawTracksPoints();
            DrawTracks();
            DrawDolly();
            SceneView.RepaintAll();

            Event e = Event.current;
            bool pressedLeftCtrl = e.keyCode == KeyCode.LeftControl;
            bool pressedMouseLeft = e.type == EventType.MouseDown && e.button == 0;
        }

        bool[] wantMoveSplineIndexs = new bool[100];
        void DrawTrackPositionHandle() {
            var bezier3D3Lines = dollyTrack.em.bezierSlineEMArray;
            EditorGUI.BeginChangeCheck();

            var len = dollyTrack.em.bezierSlineEMArray.Length;
            var dollyTrackPos = dollyTrack.transform.position;
            for (int i = 0; i < len; i++) {
                var line = bezier3D3Lines[i];
                wantMoveSplineIndexs[i] = !line.isScenePositionHandleEnabled;
                if (!line.isScenePositionHandleEnabled) {
                    continue;
                }

                Vector3 startPos = bezier3D3Lines[i].start.position;
                PositionHandle(ref startPos);
                bezier3D3Lines[i].start.SetPosition(startPos);

                Vector3 endPos = bezier3D3Lines[i].end.position;
                PositionHandle(ref endPos);
                bezier3D3Lines[i].end.SetPosition(endPos);

                PositionHandle(ref bezier3D3Lines[i].c1);
                PositionHandle(ref bezier3D3Lines[i].c2);
            }

            var avgPos = Vector3.zero;
            int count = 0;
            for (int i = 0; i < len; i++) {
                var line = bezier3D3Lines[i];
                if (wantMoveSplineIndexs[i]) {
                    count++;
                    avgPos += (line.start.position + line.end.position + line.c1 + line.c2) / 4;
                }
            }
            avgPos /= count;
            Handles.SphereHandleCap(0, avgPos + dollyTrackPos, Quaternion.identity, 1f, EventType.Repaint);
            var avgOffset = PositionHandle(ref avgPos);
            if (avgOffset != Vector3.zero) {
                for (int i = 0; i < len; i++) {
                    if (!wantMoveSplineIndexs[i]) continue;

                    var line = bezier3D3Lines[i];
                    bezier3D3Lines[i].start.SetPosition(line.start.position + avgOffset);
                    bezier3D3Lines[i].end.SetPosition(line.end.position + avgOffset);
                    bezier3D3Lines[i].c1 = line.c1 + avgOffset;
                    bezier3D3Lines[i].c2 = line.c2 + avgOffset;
                }
            }

            Vector3 PositionHandle(ref Vector3 pos) {
                Vector3 startPos_new = Handles.PositionHandle(pos + dollyTrackPos, Quaternion.identity);
                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(dollyTrack, "Move Waypoint");
                    startPos_new -= dollyTrackPos;
                    var offset = startPos_new - pos;
                    pos.x = startPos_new.x;
                    pos.y = startPos_new.y;
                    pos.z = startPos_new.z;
                    EditorUtility.SetDirty(dollyTrack);
                    return offset;
                }

                return Vector3.zero;
            }
        }


        void DrawTracksPoints() {
            var cachedColor = Handles.color;

            var bezier3D3Lines = dollyTrack.em.bezierSlineEMArray;
            var len = bezier3D3Lines.Length;
            var dollyTrackPos = dollyTrack.transform.position;

            // Draw All Point
            for (int i = 0; i < len; i++) {
                var line = bezier3D3Lines[i];
                if (!line.isScenePositionHandleEnabled) {
                    continue;
                }
                var startPos = line.start.position;
                var endPos = line.end.position;
                var c1 = line.c1;
                var c2 = line.c2;
                startPos += dollyTrackPos;
                endPos += dollyTrackPos;
                c1 += dollyTrackPos;
                c2 += dollyTrackPos;

                SceneView sceneView = SceneView.lastActiveSceneView;
                var sceneCam = sceneView?.camera;
                var forward = sceneCam?.transform.forward ?? Vector3.forward;

                Handles.color = Color.green;
                Handles.DrawSolidDisc(startPos, forward, 0.1f);
                Handles.color = Color.red;
                Handles.DrawSolidDisc(endPos, forward, 0.1f);

                Handles.color = controlPointColor1;
                Handles.DrawSolidDisc(c1, forward, 0.1f);
                Handles.color = controlPointColor2;
                Handles.DrawSolidDisc(c2, forward, 0.1f);
            }

            Handles.color = cachedColor;
        }

        void DrawTracks() {
            var cachedColor = Handles.color;

            var bezier3D3Lines = dollyTrack.em.bezierSlineEMArray;
            var len = bezier3D3Lines.Length;
            var dollyTrackPos = dollyTrack.transform.position;

            // Draw All Track
            float step = 0.01f;
            var expand = dollyTrack.trackWidth * 0.5f;
            for (int i = 0; i < len; i++) {
                var line = bezier3D3Lines[i];
                for (float nextT = step; nextT <= 1; nextT += step) {
                    var curT = nextT - step;
                    var curWP = TCDollyTrackEMUtil.GetBezierWayPointLv3(line, curT);
                    var nextWP = TCDollyTrackEMUtil.GetBezierWayPointLv3(line, nextT);
                    var curP = curWP.position + dollyTrackPos;
                    var nextP = nextWP.position + dollyTrackPos;

                    if (wantMoveSplineIndexs[i]) {
                        Handles.color = Color.green;
                    } else {
                        Handles.color = Color.red;
                    }
                    var tangentDir = line.GetTangentDir(nextT);
                    var up = Quaternion.AngleAxis(curWP.r, tangentDir) * Vector3.up;
                    var leftExpand = Vector3.Cross(tangentDir, up) * expand;
                    var leftCurP = curP + leftExpand;
                    var leftNextP = nextP + leftExpand;
                    Handles.DrawLine(leftCurP, leftNextP);

                    var rightCurP = curP - leftExpand;
                    var rightNextP = nextP - leftExpand;
                    Handles.DrawLine(rightCurP, rightNextP);

                    Handles.DrawLine(leftCurP, rightCurP);
                    Handles.DrawLine(leftNextP, rightNextP);
                }
            }

        }

        float dollyDrawTime;
        void DrawDolly() {
            if (!dollyTrack.isPlaying) {
                dollyDrawTime = Time.realtimeSinceStartup;
            } else {
                var timeOffset = Time.realtimeSinceStartup - dollyDrawTime;
                if (timeOffset >= sceneDollyRefreshInterval) {
                    dollyDrawTime = Time.realtimeSinceStartup;
                    if (dollyTrack.isPlaying) {
                        dollyTrack.t += timeOffset;
                        var totalDuration = 0f;
                        foreach (var line in dollyTrack.em.bezierSlineEMArray) {
                            totalDuration += line.duration;
                        }
                        dollyTrack.t = dollyTrack.t > totalDuration ? 0 : dollyTrack.t;
                    }
                }
            }

            var dollyTrackPos = dollyTrack.transform.position;
            var bezier3D3Lines = dollyTrack.em.bezierSlineEMArray;
            var len = bezier3D3Lines.Length;

            var cachedColor = Handles.color;
            Handles.color = Color.blue;

            // Draw Dolly
            if (len > 0) {
                var dollyWayPoint = TCDollyTrackEMUtil.GetBezierWayPointLv3(bezier3D3Lines, dollyTrack.t, out int elementIndex, out float ratioT);

                var dollyLine = bezier3D3Lines[elementIndex];
                var dollyForward = dollyLine.GetTangentDir(ratioT);
                var dollyUp = Quaternion.AngleAxis(dollyWayPoint.r, dollyForward) * Vector3.up;

                var width = dollyTrack.trackWidth;
                var height = 1;
                var length = 1;
                var dollyPos = dollyWayPoint.position + dollyTrackPos + dollyUp * height * 0.5f;
                var rot = dollyForward == Vector3.zero ? Quaternion.identity : Quaternion.LookRotation(dollyForward, dollyUp);
                DrawCube(dollyPos, rot, width, height, length);

                // Update Bind Dolly Transform
                if (dollyTrack.bindDollyTF != null) {
                    dollyTrack.bindDollyTF.position = dollyPos;
                    dollyTrack.bindDollyTF.rotation = rot;
                }
            }

            Handles.color = cachedColor;
        }

        void DrawCube(Vector3 position, Quaternion rotation, float width, float height, float length) {
            var halfWidth = width * 0.5f;
            var halfHeight = height * 0.5f;
            var halfLength = length * 0.5f;

            var leftBottomBack = position + rotation * new Vector3(-halfWidth, -halfHeight, -halfLength);
            var rightBottomBack = position + rotation * new Vector3(halfWidth, -halfHeight, -halfLength);
            var rightUpperBack = position + rotation * new Vector3(halfWidth, halfHeight, -halfLength);
            var leftUpperBack = position + rotation * new Vector3(-halfWidth, halfHeight, -halfLength);
            var leftBottomFront = position + rotation * new Vector3(-halfWidth, -halfHeight, halfLength);
            var rightBottomFront = position + rotation * new Vector3(halfWidth, -halfHeight, halfLength);
            var rightUpperFront = position + rotation * new Vector3(halfWidth, halfHeight, halfLength);
            var leftUpperFront = position + rotation * new Vector3(-halfWidth, halfHeight, halfLength);

            // 绘制后面的面
            Handles.DrawLine(leftBottomBack, rightBottomBack);
            Handles.DrawLine(rightBottomBack, rightUpperBack);
            Handles.DrawLine(rightUpperBack, leftUpperBack);
            Handles.DrawLine(leftUpperBack, leftBottomBack);

            // 绘制前面的面
            Handles.DrawLine(leftBottomFront, rightBottomFront);
            Handles.DrawLine(rightBottomFront, rightUpperFront);
            Handles.DrawLine(rightUpperFront, leftUpperFront);
            Handles.DrawLine(leftUpperFront, leftBottomFront);

            // 绘制连接两个面的边
            Handles.DrawLine(leftBottomBack, leftBottomFront);
            Handles.DrawLine(rightBottomBack, rightBottomFront);
            Handles.DrawLine(rightUpperBack, rightUpperFront);
            Handles.DrawLine(leftUpperBack, leftUpperFront);
        }

    }
}