using Convolution.DevKit.Controllers;
using Convolution.Interaction;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class ControllersInstaller : Installer<ControllerGrid, BuiltInControllerPrefabRepository, ControllersInstaller>
	{
		private readonly ControllerGrid _controllerGrid;
		private readonly BuiltInControllerPrefabRepository _builtInControllerPrefabRepository;
		
		public ControllersInstaller(ControllerGrid controllerGrid, BuiltInControllerPrefabRepository builtInControllerPrefabRepository)
		{
			_controllerGrid = controllerGrid;
			_builtInControllerPrefabRepository = builtInControllerPrefabRepository;
		}

		public override void InstallBindings()
		{
			Container.Bind<ControllerRepository>().ToSelf().AsSingle();
			Container.Bind<ControllerInputBridgeService>().ToSelf().AsSingle();
			
			Container.Bind<ControllerGrid>().ToSelf().FromInstance(_controllerGrid).AsSingle();
			
			_builtInControllerPrefabRepository.Bootup();
			Container.Bind<BuiltInControllerPrefabRepository>().ToSelf().FromInstance(_builtInControllerPrefabRepository).AsSingle();
		}
	}
}