using Convolution.DevKit.Placement;
using Cysharp.Threading.Tasks;
using Maxim.AssetManagement.Configurations;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class PlacementInstaller : Installer<PlacementInstaller>
	{
		public override async UniTask InstallBindings()
		{
			await ConfigurationInstaller<GridConfiguration>.Install(Container);
		}
	}
}