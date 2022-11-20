using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Convolution.DevKit.MiniGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Convolution.Orchestration
{
	public class QuickPlayInstaller : BootstrapInstaller
	{
		#region Nested types

		private enum LoadMode
		{
			BuiltIn,
			FromMod
		}

		#endregion

		private const string CatalogIdentifier = "catalog";
		private const string CatalogExtension = ".json";
		private const string DllExtension = ".dll";
		
		[SerializeField]
		private LoadMode _loadMode;
		
		[SerializeField]
		[ShowIf(nameof(_loadMode), LoadMode.BuiltIn)]
		private MiniGameConfiguration _gameConfiguration;

		#if UNITY_EDITOR
		[CustomValueDrawer(nameof(DrawModFolder))]
		#endif
		[SerializeField]
		[ShowIf(nameof(_loadMode), LoadMode.FromMod)]
		private string _modFolder;

		public override async void Initialize()
		{
			base.Initialize();

			var gameConfiguration = default(MiniGameConfiguration);

			switch (_loadMode)
			{
				case LoadMode.BuiltIn:
					gameConfiguration = _gameConfiguration;
					break;

				case LoadMode.FromMod:
				{
					var modFiles = Directory.GetFiles(_modFolder);

					var dllPaths = new List<string>();
					var catalogPath = default(string);
					
					for (var i = 0; i < modFiles.Length; i++)
					{
						var path = modFiles[i];
						var extension = Path.GetExtension(path);

						if (extension == CatalogExtension && Path.GetFileName(path).StartsWith(CatalogIdentifier))
							catalogPath = path;
						else if (extension == DllExtension)
							dllPaths.Add(path);
					}

					foreach (var dllPath in dllPaths)
						Assembly.LoadFile(dllPath);

					await Addressables.LoadContentCatalogAsync(catalogPath).Task;
					gameConfiguration = await Addressables.LoadAssetAsync<MiniGameConfiguration>(new List<string>() { "Configuration" }).Task;
					
					break;
				}
			}
            
			var gameArgs = Container.Resolve<GameArgs>();
			gameArgs.GameConfiguration = gameConfiguration;

			var gameSceneReference = Container.ResolveId<SceneReference>(SceneType.Game);
			await gameSceneReference.LoadSceneAsync(LoadSceneMode.Additive).Task;
		}

		#if UNITY_EDITOR

		private string DrawModFolder(string folder)
		{
			if (string.IsNullOrEmpty(folder))
				folder = Application.persistentDataPath;

			using (var _ = new UnityEditor.EditorGUILayout.HorizontalScope())
			{
				UnityEditor.EditorGUILayout.LabelField(new GUIContent("Mod Folder"), GUILayout.Width(UnityEditor.EditorGUIUtility.labelWidth));
				
				if (GUILayout.Button(folder))
					folder = UnityEditor.EditorUtility.OpenFolderPanel("Pick mod folder", folder, string.Empty);
			}
		
			return folder;
		}
		
		#endif
	}
}