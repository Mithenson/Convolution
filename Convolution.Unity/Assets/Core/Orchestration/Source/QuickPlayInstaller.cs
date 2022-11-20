using Convolution.DevKit.MiniGames;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Convolution.Orchestration
{
	public class QuickPlayInstaller : BootstrapInstaller
	{
		[SerializeField]
		private MiniGameConfiguration _gameConfiguration;

		public override async void Initialize()
		{
			base.Initialize();
            
			var gameArgs = Container.Resolve<GameArgs>();
			gameArgs.GameConfiguration = _gameConfiguration;

			var gameSceneReference = Container.ResolveId<SceneReference>(SceneType.Game);
			await gameSceneReference.LoadSceneAsync(LoadSceneMode.Additive).Task;
		}
	}
}