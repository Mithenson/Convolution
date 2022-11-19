using System.Threading.Tasks;
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
			_gameSceneReference.UnloadScene();
			await _gameSceneReference.LoadSceneAsync(LoadSceneMode.Additive).Task;
		}
	}
}