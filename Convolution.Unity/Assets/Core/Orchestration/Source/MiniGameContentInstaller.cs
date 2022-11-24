using System.Collections.Generic;
using System.IO;
using System.Linq;
using Convolution.Core.EmbeddedMiniGames;
using Convolution.DevKit.MiniGames;
using Cysharp.Threading.Tasks;
using Maxim.AssetManagement.Addressables;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class MiniGameContentInstaller : Installer<AddressableLabel, MiniGameContentInstaller>
	{
		private const string DefinitionFileName = "definition.json";

		private readonly AddressableLabel _embeddedMiniGameConfigurationLabel;
		
		public MiniGameContentInstaller(AddressableLabel embeddedMiniGameConfigurationLabel) => _embeddedMiniGameConfigurationLabel = embeddedMiniGameConfigurationLabel;
		
		public override async UniTask InstallBindings()
		{
			var modsDirectory = Path.Combine(Application.persistentDataPath, "Mods");
			var miniGameContents = new List<IMiniGameContent>();
			
			if (!Directory.Exists(modsDirectory))
			{
				Directory.CreateDirectory(modsDirectory);
			}
			else
			{
				foreach (var modDirectory in Directory.EnumerateDirectories(modsDirectory))
				{
					var files = Directory.GetFiles(modDirectory);
					
					var definitionFile = files.FirstOrDefault(file => Path.GetFileName(file) == DefinitionFileName);
					if (definitionFile == default)
					{
						Debug.LogError($"he mod at `Directory={modDirectory}` doesn't have a `File={DefinitionFileName}`.");
						continue;
					}
					
					var definitionJson = File.ReadAllText(definitionFile);
					var definition = JsonConvert.DeserializeObject<MiniGameDefinition>(definitionJson);
					
					miniGameContents.Add(new ModdedMiniGameContent(definition, modDirectory));
				}
			}

			var embeddedConfigurationsHandle = Addressables.LoadAssetsAsync<IEmbeddedMiniGameConfiguration>(
				new List<string>() { _embeddedMiniGameConfigurationLabel.Name }, 
				null, 
				Addressables.MergeMode.Intersection);

			var embeddedConfigurations = await embeddedConfigurationsHandle.ToUniTask();
			foreach (var embeddedConfiguration in embeddedConfigurations)
				miniGameContents.Add(new EmbeddedMiniGameContent(embeddedConfiguration));

			Addressables.Release(embeddedConfigurationsHandle);

			Container.Bind<IReadOnlyList<IMiniGameContent>>().FromInstance(miniGameContents).AsSingle();
		}
	}
}