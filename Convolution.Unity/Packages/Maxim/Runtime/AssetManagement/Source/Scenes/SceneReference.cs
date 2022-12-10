using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AssetManagement.Source.Scenes
{
	[Serializable]
	public sealed class SceneReference
	{
		[SerializeField]
		private AssetReference _reference;

		#if UNITY_EDITOR

		public static string ReferenceFieldName => nameof(_reference);
		
		#endif

		public AsyncOperationHandle<SceneInstance> LoadSceneAsync(
			LoadSceneMode loadMode = LoadSceneMode.Single,
			bool activateOnLoad = true,
			int priority = 100)
		{
			return _reference.LoadSceneAsync(loadMode, activateOnLoad, priority);
		}

		public AsyncOperationHandle<SceneInstance> UnloadScene() => _reference.UnLoadScene();
	}
}