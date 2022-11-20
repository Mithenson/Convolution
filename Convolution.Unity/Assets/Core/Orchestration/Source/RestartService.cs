using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class RestartService
	{
		private readonly SceneReference _gameSceneReference;
		
		public RestartService([Inject(Id = SceneType.Game)]SceneReference gameSceneReference) => _gameSceneReference = gameSceneReference;

		public async Task Restart()
		{
			await _gameSceneReference.UnloadScene().Task;
			_gameSceneReference.Reset();
			
			await _gameSceneReference.LoadSceneAsync(LoadSceneMode.Additive).Task;
		}
	}
}