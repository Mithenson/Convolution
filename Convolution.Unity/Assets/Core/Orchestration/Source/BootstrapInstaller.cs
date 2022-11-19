using Maxim.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Convolution.Orchestration
{
	public class BootstrapInstaller : MonoInstaller
	{
		[SerializeField]
		private InputActionAsset _inputs;
		
		[SerializeField]
		private SceneReference _gameSceneReference;
        
		public override void InstallBindings()
		{
			InputsInstaller.Install(Container, _inputs);
			Container.Bind<GameArgs>().ToSelf().AsSingle();

			Container.Bind<SceneReference>().WithId(SceneType.Game).FromInstance(_gameSceneReference).AsSingle();
			Container.BindInterfacesAndSelfTo<RestartService>().AsSingle();
		}

		public virtual void Initialize() => Container.Resolve<InputsService>().Enable();
	}
}