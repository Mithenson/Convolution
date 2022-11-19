using System;
using Convolution.MiniGames.Source;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Convolution.Controllers.Editor
{
    public class ControllerPlacementDrawer : OdinValueDrawer<ControllerPlacement>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var prefabProperty = Property.NextProperty(true, true);
            prefabProperty.Draw();
            
            var positionProperty = prefabProperty.NextProperty(false, true);
            positionProperty.Draw();
            
            var inputChannelProperty = positionProperty.NextProperty(false, true);
            var outerType = Property.Parent?.ParentType;
            
            if (typeof(MiniGameConfiguration).IsAssignableFrom(outerType))
            {
                var gameType = outerType.BaseType.GetGenericArguments()[0];
                var enumType =  gameType.BaseType.GetGenericArguments()[2];

                var enumValue = (Enum)Enum.ToObject(enumType, (ushort)inputChannelProperty.ValueEntry.WeakSmartValue);
                enumValue = EditorGUILayout.EnumPopup(inputChannelProperty.Label, enumValue);

                inputChannelProperty.ValueEntry.WeakSmartValue = Convert.ChangeType(enumValue, typeof(ushort));
            }
            else
            {
                inputChannelProperty.Draw();
            }
        }
    }
}
