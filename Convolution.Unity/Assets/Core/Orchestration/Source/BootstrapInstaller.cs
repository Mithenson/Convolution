using Cysharp.Threading.Tasks;
using Maxim.AssetManagement.Addressables;
using Maxim.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace Convolution.Orchestration
{
	public class BootstrapInstaller : MonoInstaller, IInitializableInstaller
	{
		[SerializeField]
		private InputActionAsset _inputs;

		[SerializeField]
		private AddressableLabel _embeddedMiniGameConfigurationLabel;

		[SerializeField]
		private SceneReference _menuSceneReference;
		
		[SerializeField]
		private SceneReference _gameSceneReference;
        
		public override async UniTask InstallBindings()
		{
			await InputsInstaller.Install(Container, _inputs);
			
			Container.Bind<SceneReference>().WithId(SceneType.Menu).FromInstance(_menuSceneReference).AsCached();
			Container.Bind<SceneReference>().WithId(SceneType.Game).FromInstance(_gameSceneReference).AsCached();

			await MiniGameContentInstaller.Install(Container, _embeddedMiniGameConfigurationLabel);
			Container.Bind<GameContext>().ToSelf().AsSingle();
		}

		public virtual async UniTask Initialize()
		{
			Container.Resolve<InputsService>().Enable();
			await _menuSceneReference.LoadSceneAsync(LoadSceneMode.Additive);
		}
	}
}