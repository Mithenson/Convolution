using Convolution.Gameplay;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class GameplayInstaller : Installer<GameplayInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<GameplayModel>().AsSingle();
			Container.Bind(typeof(GameplayLoop), typeof(ITickable)).To<GameplayLoop>().AsSingle();
			
			GameplayInputsInstaller.Install(Container);
		}
	}
}