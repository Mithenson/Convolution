using Convolution.DevKit.Interaction;
using Convolution.Interaction;
using Cysharp.Threading.Tasks;
using Maxim.AssetManagement.Configurations;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class InteractionInstaller : Installer<InteractionInstaller>
	{
		public override async UniTask InstallBindings()
		{
			Container.Bind<Cursor>().ToSelf().AsSingle();
			await ConfigurationInstaller<CursorConfiguration>.Install(Container);
			
			Container.BindInterfacesAndSelfTo<InteractionService>().AsSingle();
		}
	}
}