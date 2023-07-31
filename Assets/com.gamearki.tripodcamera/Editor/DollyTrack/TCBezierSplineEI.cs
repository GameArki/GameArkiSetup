#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameArki.TripodCamera.EditorTool {

    [CustomPropertyDrawer(typeof(TCBezierSplineEM))]
    public class TCBezierSplineEI : PropertyDrawer {

        const float fieldHeight = 18f;
        const float fieldSpace = 5f;
        readonly float fieldOffset = fieldHeight + fieldSpace;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            var labelWidth = position.width * 0.2f;
            var fieldWidth = position.width * 0.8f;
            
            EditorGUIUtility.labelWidth = labelWidth;

            Rect singleLineLabelRect = new Rect(position.x, position.y, fieldWidth, fieldHeight);
            Rect singleLineRect = new Rect(position.x + labelWidth, position.y, fieldWidth, fieldHeight);

            // Show isScenePositionHandleEnabled property in the left,and vertically center
            Rect isScenePositionHandleEnabledRect = new Rect(position.x - 25, position.y, labelWidth, fieldHeight);
            SerializedProperty isScenePositionHandleEnabledProp = property.FindPropertyRelative("isScenePositionHandleEnabled");
            EditorGUI.PropertyField(isScenePositionHandleEnabledRect, isScenePositionHandleEnabledProp, GUIContent.none);

            SerializedProperty startProp = property.FindPropertyRelative("start");
            EditorGUI.PrefixLabel(singleLineLabelRect, new GUIContent("Start"));
            EditorGUI.PropertyField(singleLineRect, startProp, GUIContent.none);

            singleLineLabelRect.y += fieldOffset;
            singleLineRect.y += fieldOffset;
            SerializedProperty endProp = property.FindPropertyRelative("end");
            EditorGUI.PrefixLabel(singleLineLabelRect, new GUIContent("End"));
            EditorGUI.PropertyField(singleLineRect, endProp, GUIContent.none);

            singleLineLabelRect.y += fieldOffset;
            singleLineRect.y += fieldOffset;
            SerializedProperty c1Prop = property.FindPropertyRelative("c1");
            EditorGUI.PrefixLabel(singleLineLabelRect, new GUIContent("C1"));
            EditorGUI.PropertyField(singleLineRect, c1Prop, GUIContent.none);

            singleLineLabelRect.y += fieldOffset;
            singleLineRect.y += fieldOffset;
            SerializedProperty c2Prop = property.FindPropertyRelative("c2");
            EditorGUI.PrefixLabel(singleLineLabelRect, new GUIContent("C2"));
            EditorGUI.PropertyField(singleLineRect, c2Prop, GUIContent.none);

            singleLineLabelRect.y += fieldOffset;
            singleLineRect.y += fieldOffset;
            SerializedProperty durationProp = property.FindPropertyRelative("duration");
            EditorGUI.PrefixLabel(singleLineLabelRect, new GUIContent("Duration"));
            EditorGUI.PropertyField(singleLineRect, durationProp, GUIContent.none);

            singleLineLabelRect.y += fieldOffset;
            singleLineRect.y += fieldOffset;
            SerializedProperty timeEasingTypeProp = property.FindPropertyRelative("timeEasingType");
            EditorGUI.PrefixLabel(singleLineLabelRect, new GUIContent("TimeEasingType"));
            EditorGUI.PropertyField(singleLineRect, timeEasingTypeProp, GUIContent.none);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return fieldOffset * 6f;
        }
    }
}
#endif
