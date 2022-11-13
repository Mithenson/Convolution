using System;
using System.IO;
using UnityEditor;

namespace Maxim.AssetManagement.Editor
{
	[Serializable]
	public sealed class NameAddressableByFileName : IAddressableNamingStrategy
	{
		public string GetName(GUID guid)
		{
			var path = AssetDatabase.GUIDToAssetPath(guid);
			return Path.GetFileNameWithoutExtension(path);
		}
	}
}