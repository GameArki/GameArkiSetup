#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GameArki.TripodCamera.EditorTool {

    [CustomPropertyDrawer(typeof(TripodCamera.TCWayPoint))]
    public class TCWayPointEI : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            float fieldWidth = position.width / 4f;

            EditorGUIUtility.labelWidth = 14f;

            Rect xRect = new Rect(position.x, position.y, fieldWidth, position.height);
            Rect yRect = new Rect(position.x + fieldWidth, position.y, fieldWidth, position.height);
            Rect zRect = new Rect(position.x + fieldWidth * 2f, position.y, fieldWidth, position.height);
            Rect rRect = new Rect(position.x + fieldWidth * 3f, position.y, fieldWidth, position.height);

            SerializedProperty xProp = property.FindPropertyRelative("x");
            SerializedProperty yProp = property.FindPropertyRelative("y");
            SerializedProperty zProp = property.FindPropertyRelative("z");
            SerializedProperty rProp = property.FindPropertyRelative("r");

            EditorGUI.PropertyField(xRect, xProp);
            EditorGUI.PropertyField(yRect, yProp);
            EditorGUI.PropertyField(zRect, zProp);
            EditorGUI.PropertyField(rRect, rProp);

            EditorGUI.EndProperty();
        }

    }

}

#endif