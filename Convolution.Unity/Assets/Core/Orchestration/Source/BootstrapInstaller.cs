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
        
		public override void InstallBindings()
		{
			InputsInstaller.Install(Container, _inputs);
			Container.Bind<GameArgs>().ToSelf().AsSingle();
		}

		public virtual void Initialize() => Container.Resolve<InputsService>().Enable();
	}
}