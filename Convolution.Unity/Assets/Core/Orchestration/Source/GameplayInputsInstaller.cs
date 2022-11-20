using Convolution.Gameplay;
using Maxim.AssetManagement.Configurations;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class GameplayInputsInstaller : Installer<GameplayInputsInstaller>
	{
		public override void InstallBindings()
		{
			ConfigurationInstaller<GameplayInputsConfiguration>.Install(Container);
			Container.BindInterfacesAndSelfTo<GameplayInputsRepository>().AsSingle();
		}
	}
}