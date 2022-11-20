using System;
using Convolution.DevKit.MiniGames;
using UnityEditor;
using UnityEngine;

namespace Convolution.DevKit.Controllers.Editor
{
    [CustomPropertyDrawer(typeof(ControllerPlacement))]
    public class ControllerPlacementDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty iterator, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, iterator);
            
            rect.height = (EditorGUIUtility.singleLineHeight);
            
            var chosenSelectionModeProperty = iterator.FindPropertyRelative(ControllerPlacement.ChosenSelectionModeFieldName);
            EditorGUI.PropertyField(rect, chosenSelectionModeProperty);
            
            rect.y = rect.yMax + EditorGUIUtility.standardVerticalSpacing;

            switch ((ControllerPlacement.SelectionMode)chosenSelectionModeProperty.intValue)
            {
                case ControllerPlacement.SelectionMode.BuiltIn:
                    var pickedBuiltInTypeProperty = iterator.FindPropertyRelative(ControllerPlacement.PickedBuiltInTypeFieldName);
                    EditorGUI.PropertyField(rect, pickedBuiltInTypeProperty);
                    break;

                case ControllerPlacement.SelectionMode.Custom:
                    var pickedPrefabProperty = iterator.FindPropertyRelative(ControllerPlacement.PickedPrefabFieldName);
                    EditorGUI.PropertyField(rect, pickedPrefabProperty);
                    break;
            }
            
            rect.y = rect.yMax + EditorGUIUtility.standardVerticalSpacing;
            
            var positionProperty = iterator.FindPropertyRelative(ControllerPlacement.PositionFieldName);
            EditorGUI.PropertyField(rect, positionProperty);
            
            rect.y = rect.yMax + EditorGUIUtility.standardVerticalSpacing;
            
            var inputChannelProperty = iterator.FindPropertyRelative(ControllerPlacement.InputChannelFieldName);
            if (typeof(MiniGameConfiguration).IsAssignableFrom(fieldInfo.DeclaringType))
            {
                var gameType = fieldInfo.DeclaringType.GetGenericArguments()[0];
                var enumType =  gameType.BaseType.GetGenericArguments()[2];

                var enumValue = (Enum)Enum.ToObject(enumType, (ushort)inputChannelProperty.intValue);
                enumValue = EditorGUI.EnumPopup(rect, new GUIContent(inputChannelProperty.displayName), enumValue);

                inputChannelProperty.intValue = (int)Convert.ChangeType(enumValue, typeof(int));
            }
            else
            {
                EditorGUI.PropertyField(rect, inputChannelProperty);
            }
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty iterator, GUIContent label) => EditorGUIUtility.singleLineHeight * 4.0f + EditorGUIUtility.standardVerticalSpacing * 3.0f;
    }
}
