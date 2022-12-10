using AssetManagement.Source.Scenes;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	[CustomPropertyDrawer(typeof(SceneReference))]
	public sealed class SceneReferenceDrawer : PropertyDrawer
	{
		private static readonly string _guidPropertyPath = $"{SceneReference.ReferenceFieldName}.m_AssetGUID";
		
		public override void OnGUI(Rect rect, SerializedProperty root, GUIContent label)
		{
			EditorGUI.BeginProperty(rect, label, root);
			
			var guidProperty = root.FindPropertyRelative(_guidPropertyPath);
			var guid = guidProperty.stringValue;
			
			var entry = default(AddressableAssetEntry);
			if (!string.IsNullOrEmpty(guid))
				entry = AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(guid);
			
			var asset = EditorGUI.ObjectField(rect, label, entry?.MainAsset, typeof(SceneAsset), false);
			var path = AssetDatabase.GetAssetPath(asset);
			
			guidProperty.stringValue = AssetDatabase.AssetPathToGUID(path);
			
			EditorGUI.EndProperty();
		}
	}
}