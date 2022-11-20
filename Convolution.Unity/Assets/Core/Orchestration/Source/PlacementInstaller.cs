using Convolution.DevKit.Placement;
using Maxim.AssetManagement.Configurations;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class PlacementInstaller : Installer<PlacementInstaller>
	{
		public override void InstallBindings()
		{
			ConfigurationInstaller<GridConfiguration>.Install(Container);
		}
	}
}