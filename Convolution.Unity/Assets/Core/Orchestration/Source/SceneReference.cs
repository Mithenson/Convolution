using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Convolution.Orchestration
{
	[Serializable]
	public sealed class SceneReference
	{
		[SerializeField]
		private AssetReference _reference;

		public string Guid => _reference?.AssetGUID;

		public AsyncOperationHandle<SceneInstance> LoadSceneAsync(
			LoadSceneMode loadMode = LoadSceneMode.Single,
			bool activateOnLoad = true,
			int priority = 100)
		{
			return _reference.LoadSceneAsync(loadMode, activateOnLoad, priority);
		}

		public AsyncOperationHandle<SceneInstance> UnloadScene() => _reference.UnLoadScene();
		
		public void Reset() => _reference.ReleaseAsset();
	}
}