using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Convolution.Orchestration.Editor
{
    public class SceneReferenceDrawer : OdinValueDrawer<SceneReference>
    {
        private const string GuidPropertyName = "m_AssetGUID";
        
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var guidProperty = Property.FindChild(child => child.Name == GuidPropertyName, false);
            var guid = guidProperty.ValueEntry.WeakSmartValue as string;
            var entry = default(AddressableAssetEntry);
            
            if (!string.IsNullOrEmpty(guid))
                entry = AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry((string)guidProperty.ValueEntry.WeakSmartValue);

            var asset = EditorGUILayout.ObjectField(label, entry?.MainAsset, typeof(SceneAsset), false);
            var path = AssetDatabase.GetAssetPath(asset);

            guid = AssetDatabase.AssetPathToGUID(path);
            guidProperty.ValueEntry.WeakSmartValue = guid;
        }
    }
}
