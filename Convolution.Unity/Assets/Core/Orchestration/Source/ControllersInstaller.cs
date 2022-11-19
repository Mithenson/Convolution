using Convolution.Controllers;
using Convolution.Interaction;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class ControllersInstaller : Installer<ControllerGrid, ControllersInstaller>
	{
		private readonly ControllerGrid _controllerGrid;
		
		public ControllersInstaller(ControllerGrid controllerGrid) => _controllerGrid = controllerGrid;

		public override void InstallBindings()
		{
			Container.Bind<ControllerRepository>().ToSelf().AsSingle();
			Container.Bind<ControllerInputBridgeService>().ToSelf().AsSingle();
			
			Container.Bind<ControllerGrid>().ToSelf().FromInstance(_controllerGrid).AsSingle();
		}
	}
}