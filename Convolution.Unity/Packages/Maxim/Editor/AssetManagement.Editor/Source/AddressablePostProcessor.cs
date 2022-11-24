using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	public class AddressablePostProcessor : AssetPostprocessor
	{
		private const string CollectionLabel = "Addressable";
		private const string RootDirectory = "Assets";

		private static readonly string _filterForCollectorSearch = $"t:{nameof(AddressableCollector)}";
		private static readonly Dictionary<string, AddressableCollector> _collectorsByPath = new Dictionary<string, AddressableCollector>();
		private static readonly Queue<string> _pathsForReimport = new Queue<string>();

		[InitializeOnLoadMethod]
		private static void Initialize()
		{
			foreach (var collectorPath in AssetDatabase.FindAssets(_filterForCollectorSearch).Select(AssetDatabase.GUIDToAssetPath))
			{
				var collector = AssetDatabase.LoadAssetAtPath<AddressableCollector>(collectorPath);
				_collectorsByPath.Add(collectorPath, collector);
			}
			
			EditorApplication.update += Tick;
		}

		private static void Tick()
		{
			if (_pathsForReimport.Count == 0)
				return;
			
			AssetDatabase.StartAssetEditing();

			try
			{
				while (_pathsForReimport.Count > 0)
					AssetDatabase.ImportAsset(_pathsForReimport.Dequeue());
			}
			finally
			{
				AssetDatabase.StopAssetEditing();
			}
		}
		
		private static void OnPostprocessAllAssets(
			string[] importedAssetPaths, 
			string[] deletedAssetPaths, 
			string[] movedAssetPaths, 
			string[] movedAssetPreviousPaths)
		{
			var collectorByDirectory = new Dictionary<string, AddressableCollector>();
			var manualEntryPathToCollector = new Dictionary<string, AddressableCollector>();
			
			foreach (var kvp in _collectorsByPath)
			{
				var directory = PathUtilities.GetDirectory(kvp.Key);
				if (collectorByDirectory.ContainsKey(directory))
				{
					Debug.LogError(
						$"More than one collectors are under the `Directory={directory}`. " + 
						$"Only `{nameof(AddressableCollector)}={kvp.Value.name}` will be taken into account during the collection process " + 
						$"aside manual entries.");
				}
				else
				{
					collectorByDirectory.Add(directory, kvp.Value);
				}
					
				foreach (var manualEntry in kvp.Value.ManualEntries)
				{
					var path = AssetDatabase.GetAssetPath(manualEntry);
					manualEntryPathToCollector.Add(path, kvp.Value);
				}
			}
			
			foreach (var path in importedAssetPaths)
			{
				var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
				if (assetType == typeof(AddressableCollector) && ! _collectorsByPath.ContainsKey(path))
				{
					var collector = AssetDatabase.LoadAssetAtPath<AddressableCollector>(path);
					_collectorsByPath.Add(path, collector);
					
					continue;
				}
				
				TryCollect(manualEntryPathToCollector, collectorByDirectory, path);
			}

			for (var i = 0; i < movedAssetPaths.Length; i++)
			{
				var movedAssetPath = movedAssetPaths[i];
				
				var assetType = AssetDatabase.GetMainAssetTypeAtPath(movedAssetPath);
				if (assetType == typeof(AddressableCollector))
				{
					_collectorsByPath.Remove(movedAssetPreviousPaths[i]);
					
					var collector = AssetDatabase.LoadAssetAtPath<AddressableCollector>(movedAssetPath);
					_collectorsByPath.Add(movedAssetPath, collector);

					continue;
				}
				
				TryCollect(manualEntryPathToCollector, collectorByDirectory, movedAssetPath);
			}

			var directoriesForAssetSearch = new string[1];
			foreach (var path in deletedAssetPaths)
			{
				if (!_collectorsByPath.Remove(path))
					continue;

				directoriesForAssetSearch[0] = PathUtilities.GetDirectory(path);
				
				var guids = AssetDatabase.FindAssets(string.Empty, directoriesForAssetSearch);
				foreach (var guid in guids)
				{
					var pathForReimport = AssetDatabase.GUIDToAssetPath(guid);
					_pathsForReimport.Enqueue(pathForReimport);
				}
			}
		}

		private static void TryCollect(
			Dictionary<string, AddressableCollector> manualEntryPathToCollector, 
			Dictionary<string, AddressableCollector> collectorsByDirectory,
			string path)
		{
			var guid = AssetDatabase.GUIDFromAssetPath(path);
			
			if (manualEntryPathToCollector.TryGetValue(path, out var collector))
			{
				collector.CreateOrAddAddressableEntry(guid);
				return;
			}
			
			var labels = AssetDatabase.GetLabels(guid);
			if (!labels.Contains(CollectionLabel))
				return;
				
			var directory = PathUtilities.GetDirectory(path);
			do
			{
				if (collectorsByDirectory.TryGetValue(directory, out collector))
					break;
				
				directory = PathUtilities.GetParentDirectory(directory);
			}
			while (directory != null);

			if (collector == null)
			{
				Debug.LogWarning(
					$"The asset at `{nameof(path)}={path}` was marked with the \"{CollectionLabel}\" label " + 
					$"but no {nameof(AddressableCollector)} was found for it.");

				AddressableAssetSettingsDefaultObject.Settings.RemoveAssetEntry(guid.ToString());
				return;
			}
			
			collector.CreateOrAddAddressableEntry(guid);
		}
	}
}
