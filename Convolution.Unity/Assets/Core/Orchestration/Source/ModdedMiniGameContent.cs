using System.IO;
using Convolution.DevKit.MiniGames;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Convolution.Orchestration
{
	public sealed class ModdedMiniGameContent : IMiniGameContent
	{
		private const string CatalogFileStartIdentifier = "catalog";
		private const string CatalogFileExtension = ".json";

		private readonly MiniGameDefinition _definition;
		private readonly string _directory;

		private AsyncOperationHandle<IResourceLocator> _catalogHandle;
		private AsyncOperationHandle<MiniGameConfiguration> _configurationHandle;
		
		public ModdedMiniGameContent(MiniGameDefinition definition, string directory)
		{
			_definition = definition;
			_directory = directory;
		}

		public MiniGameDefinition Definition => _definition;

		public async UniTask<IMiniGameConfiguration> Load()
		{
			var catalogPath = default(string);
			foreach (var file in Directory.EnumerateFiles(_directory))
			{
				var extension = Path.GetExtension(file);
            
				if (extension == CatalogFileExtension && Path.GetFileName(file).StartsWith(CatalogFileStartIdentifier))
					catalogPath = file;
			}
			
			if (catalogPath == default)
				throw new InvalidDataException($"The mod at `Directory={_directory}` was expected to have an addressable .json catalog.");

			_catalogHandle = Addressables.LoadContentCatalogAsync(catalogPath);
			await _catalogHandle.Task;

			_configurationHandle = Addressables.LoadAssetAsync<MiniGameConfiguration>($"{_definition.Name}Configuration");
			return await _configurationHandle.Task;
		}

		public UniTask Unload()
		{
			Addressables.Release(_configurationHandle);
			Addressables.RemoveResourceLocator(_catalogHandle.Result);
			Addressables.Release(_catalogHandle);
			
			return UniTask.CompletedTask;
		}
	}
}