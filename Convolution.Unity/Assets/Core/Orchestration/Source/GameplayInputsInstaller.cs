using Convolution.Gameplay;
using Cysharp.Threading.Tasks;
using Maxim.AssetManagement.Configurations;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class GameplayInputsInstaller : Installer<GameplayInputsInstaller>
	{
		public override async UniTask InstallBindings()
		{
			await ConfigurationInstaller<GameplayInputsConfiguration>.Install(Container);
			Container.BindInterfacesAndSelfTo<GameplayInputsRepository>().AsSingle();
		}
	}
}