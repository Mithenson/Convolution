using Convolution.DevKit.MiniGames;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class GameContext
	{
		private readonly SceneReference _menuSceneReference;
		private readonly SceneReference _gameSceneReference;
		
		private IMiniGameContent _miniGameContent;
		
		public GameContext(
			[Inject(Id = SceneType.Menu)] SceneReference menuSceneReference,
			[Inject(Id = SceneType.Game)] SceneReference gameSceneReference)
		{
			_menuSceneReference = menuSceneReference;
			_gameSceneReference = gameSceneReference;
		}
		
		public IMiniGameConfiguration Configuration { get; private set; }
		
		public async UniTask Start(IMiniGameContent miniGameContent)
		{
			await _menuSceneReference.UnloadScene().ToUniTask();
			_menuSceneReference.Reset();
			
			_miniGameContent = miniGameContent;

			Configuration = await _miniGameContent.Load();
			await _gameSceneReference.LoadSceneAsync(LoadSceneMode.Additive);
		}

		public async UniTask Restart()
		{
			await _gameSceneReference.UnloadScene().ToUniTask();
			_gameSceneReference.Reset();
			
			await _gameSceneReference.LoadSceneAsync(LoadSceneMode.Additive).Task;
		}

		public async UniTask Leave()
		{
			await _gameSceneReference.UnloadScene().ToUniTask();
			_gameSceneReference.Reset();

			Configuration = null;
			await _miniGameContent.Unload();

			_miniGameContent = null;
			await _menuSceneReference.LoadSceneAsync(LoadSceneMode.Additive).Task;
		}
	}
}