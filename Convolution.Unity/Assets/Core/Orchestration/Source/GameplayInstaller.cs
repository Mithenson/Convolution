using Convolution.Gameplay;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class GameplayInstaller : Installer<GameplayInstaller>
	{
		public override async UniTask InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<GameplayModel>().AsSingle();
			Container.Bind(typeof(GameplayLoop), typeof(ITickable)).To<GameplayLoop>().AsSingle();
			
			await GameplayInputsInstaller.Install(Container);

			Container.BindInterfacesAndSelfTo<GameplayEndMenuViewModel>().AsSingle();
		}
	}
}