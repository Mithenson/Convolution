using Convolution.Handling;
using Convolution.Interaction;
using Maxim.AssetManagement.Configurations;
using Zenject;

namespace Convolution
{
	public sealed class InteractionInstaller : Installer<InteractionInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<Cursor>().ToSelf().AsSingle();
			ConfigurationInstaller<CursorConfiguration>.Install(Container);
			
			Container.BindInterfacesAndSelfTo<InteractionService>().AsSingle();
		}
	}
}