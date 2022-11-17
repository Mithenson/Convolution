using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

namespace Maxim.AssetManagement.Editor
{
	public class AddressablePostProcessor : AssetPostprocessor
	{
		private const string CollectionLabel = "Addressable";
		private const string RootDirectory = "Assets";

		private static readonly string FilterForCollectorSearch = $"t:{nameof(AddressableCollector)}";
		private static readonly HashSet<string> CollectorPaths = new HashSet<string>();
		private static readonly Queue<string> PathsForReimport = new Queue<string>();

		[InitializeOnLoadMethod]
		private static void Initialize()
		{
			CollectorPaths.AddRange(AssetDatabase.FindAssets(FilterForCollectorSearch).Select(AssetDatabase.GUIDToAssetPath));
			EditorApplication.update += Tick;
		}

		private static void Tick()
		{
			if (PathsForReimport.Count == 0)
				return;
			
			AssetDatabase.StartAssetEditing();

			try
			{
				while (PathsForReimport.Count > 0)
					AssetDatabase.ImportAsset(PathsForReimport.Dequeue());
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
			foreach (var path in importedAssetPaths)
			{
				var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
				if (assetType == typeof(AddressableCollector))
				{
					CollectorPaths.Add(path);
					continue;
				}
				
				TryCollect(path);
			}

			for (var i = 0; i < movedAssetPaths.Length; i++)
			{
				var movedAssetPath = movedAssetPaths[i];
				
				var assetType = AssetDatabase.GetMainAssetTypeAtPath(movedAssetPath);
				if (assetType == typeof(AddressableCollector))
				{
					CollectorPaths.Remove(movedAssetPreviousPaths[i]);
					CollectorPaths.Add(movedAssetPath);

					continue;
				}
				
				TryCollect(movedAssetPath);
			}

			var directoriesForAssetSearch = new string[1];
			foreach (var path in deletedAssetPaths)
			{
				if (!CollectorPaths.Remove(path))
					continue;

				directoriesForAssetSearch[0] = PathUtilities.GetDirectory(path);
				
				var guids = AssetDatabase.FindAssets(string.Empty, directoriesForAssetSearch);
				foreach (var guid in guids)
				{
					var pathForReimport = AssetDatabase.GUIDToAssetPath(guid);
					PathsForReimport.Enqueue(pathForReimport);
				}
			}
		}

		private static void TryCollect(string path)
		{
			var guid = AssetDatabase.GUIDFromAssetPath(path);
			var labels = AssetDatabase.GetLabels(guid);

			if (!labels.Contains(CollectionLabel))
				return;
				
			var directory = PathUtilities.GetDirectory(path);
			var directoriesForCollectorSearch = new string[1];
				
			var collectorGuid = default(string);
			do
			{
				directoriesForCollectorSearch[0] = directory;
				
				var collectorGuids = AssetDatabase.FindAssets(FilterForCollectorSearch, directoriesForCollectorSearch).Where(candidate =>
				{
					var candidatePath = AssetDatabase.GUIDToAssetPath(candidate);
					var candidateDirectory = PathUtilities.GetDirectory(candidatePath);

					return candidateDirectory == directory;

				}).ToArray();
				
				if (collectorGuids.Length == 0)
				{
					directory = PathUtilities.GetParentDirectory(directory);
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
			while (directory != null);

			if (string.IsNullOrEmpty(collectorGuid))
			{
				Debug.LogWarning(
					$"The asset at `{nameof(path)}={path}` was marked with the \"{CollectionLabel}\" label " + 
					$"but no {nameof(AddressableCollector)} was found for it.");

				AddressableAssetSettingsDefaultObject.Settings.RemoveAssetEntry(guid.ToString());
				return;
			}

			var collectorPath = AssetDatabase.GUIDToAssetPath(collectorGuid);
			var collector = AssetDatabase.LoadAssetAtPath<AddressableCollector>(collectorPath);
				
			collector.CreateOrAddAddressableEntry(guid);
		}

		#region Utilities



		#endregion
	}
}
