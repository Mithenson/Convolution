using Convolution.Controllers;
using Zenject;

namespace Convolution
{
	public sealed class ControllersInstaller : Installer<ControllersInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<ControllerRepository>().ToSelf().AsSingle();
		}
	}
}