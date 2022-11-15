using Convolution.Controllers;
using Convolution.Interaction;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class ControllersInstaller : Installer<ControllersInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<ControllerRepository>().ToSelf().AsSingle();
			Container.Bind<ControllerInputBridgeService>().ToSelf().AsSingle();
		}
	}
}