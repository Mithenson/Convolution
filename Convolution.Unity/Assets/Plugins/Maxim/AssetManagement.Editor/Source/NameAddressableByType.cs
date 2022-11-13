using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Maxim.AssetManagement.Editor
{
	[Serializable]
	public sealed class NameAddressableByType : IAddressableNamingStrategy
	{
		public string GetName(GUID guid)
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
			
			return asset.GetType().Name;
		}
	}
}