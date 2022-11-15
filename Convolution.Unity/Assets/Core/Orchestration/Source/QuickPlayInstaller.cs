using Convolution.MiniGames.Source;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Convolution.Orchestration
{
	public class QuickPlayInstaller : BootstrapInstaller
	{
		[SerializeField]
		private MiniGameConfiguration _gameConfiguration;
        
		[SerializeField]
		private SceneReference _gameSceneReference;

		public override async void Initialize()
		{
			base.Initialize();
            
			var gameArgs = Container.Resolve<GameArgs>();
			gameArgs.GameConfiguration = _gameConfiguration;
            
			await _gameSceneReference.LoadSceneAsync(LoadSceneMode.Additive).Task;
		}
	}
}