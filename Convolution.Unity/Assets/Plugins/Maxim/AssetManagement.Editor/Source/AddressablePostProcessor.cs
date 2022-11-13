using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	public class AddressablePostProcessor : AssetPostprocessor
	{
		private const string CollectionLabel = "Addressable";	
		
		static private void OnPostprocessAllAssets(
			string[] importedAssetPaths, 
			string[] deletedAssetPaths, 
			string[] movedAssetPaths, 
			string[] movedAssetPreviousPaths)
		{
			foreach (var path in importedAssetPaths)
			{
				var guid = AssetDatabase.GUIDFromAssetPath(path);
				var labels = AssetDatabase.GetLabels(guid);

				if (!labels.Contains(CollectionLabel))
					continue;
				
				var directory = Path.GetDirectoryName(path);
				var filterForCollectorSearch = $"t:{nameof(AddressableCollector)}";
				var directoriesForCollectorSearch = new string[1];
				
				var collectorGuid = default(string);
				do
				{
					directoriesForCollectorSearch[0] = directory;
					var collectorGuids = AssetDatabase.FindAssets(filterForCollectorSearch, directoriesForCollectorSearch);

					if (collectorGuids.Length == 0)
					{
						directory = Directory.GetParent(directory)?.FullName;
						continue;
					}

					if (collectorGuids.Length > 1)
					{
						Debug.LogError(
							$"More than one {nameof(AddressableCollector)} was found in `{nameof(directory)}={directory}` " + 
							$"for the asset at `{nameof(path)}={path}`.");
						
						break;
					}

					collectorGuid = collectorGuids[0];
					break;
				}
				while (Directory.Exists(directory));

				if (string.IsNullOrEmpty(collectorGuid))
				{
					Debug.LogWarning(
						$"The asset at `{nameof(path)}={path}` was marked with the \"{CollectionLabel}\" label " + 
						$"but no {nameof(AddressableCollector)} was found for it.");
					
					continue;
				}

				var collectorPath = AssetDatabase.GUIDToAssetPath(collectorGuid);
				var collector = AssetDatabase.LoadAssetAtPath<AddressableCollector>(collectorPath);
				
				collector.CreateOrAddAddressableEntry(guid);
			}
		}
	}
}
