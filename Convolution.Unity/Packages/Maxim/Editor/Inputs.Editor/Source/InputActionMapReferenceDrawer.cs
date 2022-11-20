using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Maxim.Inputs.Editor
{
    [CustomPropertyDrawer(typeof(InputActionMapReference))]
    public class InputActionMapReferenceDrawer : PropertyDrawer
    {
	    public override void OnGUI(Rect rect, SerializedProperty iterator, GUIContent label)
	    {
		    EditorGUI.BeginProperty(rect, label, iterator);
		    
		    var assetProperty = iterator.FindPropertyRelative(InputActionMapReference.AssetFieldName);
		    rect.height = EditorGUIUtility.singleLineHeight;

		    EditorGUI.PropertyField(rect, assetProperty);

		    if (!(assetProperty.objectReferenceValue is InputActionAsset asset))
		    {
			    EditorGUI.EndProperty();
			    return;
		    }

		    var idProperty = iterator.FindPropertyRelative(InputActionMapReference.IdFieldName);
		    rect.y = rect.yMax + EditorGUIUtility.standardVerticalSpacing;
		    rect.height = EditorGUIUtility.singleLineHeight;
		    
		    if (asset.actionMaps.Count == 0)
		    {
			    EditorGUI.HelpBox(rect, "No maps are defined.", MessageType.Warning);
			    idProperty.stringValue = string.Empty;
			    
			    EditorGUI.EndProperty();
			    return;
		    }
            
		    var id = !string.IsNullOrEmpty(idProperty.stringValue) ? Guid.Parse(idProperty.stringValue) : Guid.Empty;
			
		    var selectedIndex = asset.actionMaps.IndexOf(map => map.id == id);
		    if (selectedIndex == -1)
			    selectedIndex = 0;

		    selectedIndex = EditorGUI.Popup(rect, label, selectedIndex, asset.actionMaps.Select(map => new GUIContent(map.name)).ToArray());
		    idProperty.stringValue = asset.actionMaps[selectedIndex].id.ToString();
		    
		    EditorGUI.EndProperty();
	    }

	    public override float GetPropertyHeight(SerializedProperty iterator, GUIContent label)
	    {
		    var assetProperty = iterator.FindPropertyRelative(InputActionMapReference.AssetFieldName);

		    if (assetProperty.objectReferenceValue != null)
			    return EditorGUIUtility.singleLineHeight * 2.0f + EditorGUIUtility.standardVerticalSpacing;
		    else
			    return EditorGUIUtility.singleLineHeight;
	    }
    }
}
