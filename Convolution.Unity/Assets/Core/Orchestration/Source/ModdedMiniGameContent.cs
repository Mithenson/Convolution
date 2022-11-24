using System;
using System.Collections.Generic;
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
		private const string DllFileExtension = ".dll";
		
		private readonly MiniGameDefinition _definition;
		private readonly string _directory;

		private AppDomain _domain;
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
			var dllPaths = new List<string>();
			var catalogPath = default(string);

			foreach (var file in Directory.EnumerateFiles(_directory))
			{
				var extension = Path.GetExtension(file);
            
				if (extension == CatalogFileExtension && Path.GetFileName(file).StartsWith(CatalogFileStartIdentifier))
					catalogPath = file;
				else if (extension == DllFileExtension)
					dllPaths.Add(file);
			}

			if (dllPaths.Count == 0)
				throw new InvalidDataException($"The mod at `Directory={_directory}` was expected to have at least 1 .dll to load.");

			if (catalogPath == default)
				throw new InvalidDataException($"The mod at `Directory={_directory}` was expected to have an addressable .json catalog.");

			_domain = AppDomain.CreateDomain(_definition.Name);

			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					_domain.Load(assembly.GetName());
				}
				catch
				{
					continue;
				}
			}
			
			foreach (var dllPath in dllPaths)
			{
				using var dllStream = new FileStream(dllPath, FileMode.Open);
				
				var dllBuffer = new byte[(int)dllStream.Length];
				await dllStream.ReadAsync(dllBuffer, 0, dllBuffer.Length);

				var pdbPath = $"{dllPath.Remove(dllPath.Length - DllFileExtension.Length)}.pdb";
				using var pdbStream = new FileStream(pdbPath, FileMode.Open);
				
				var pdbBuffer = new byte[(int)pdbStream.Length];
				await pdbStream.ReadAsync(pdbBuffer, 0, pdbBuffer.Length);

				_domain.Load(dllBuffer, pdbBuffer);
			}

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
			
			AppDomain.Unload(_domain);
			return UniTask.CompletedTask;
		}
	}
}