using Zenject;

namespace Maxim.AssetManagement.Configurations
{
	public sealed class ConfigurationInstaller<TConfiguration> : Installer<ConfigurationInstaller<TConfiguration>>
		where TConfiguration : Configuration
	{
		public override void InstallBindings()
		{
			var handle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<TConfiguration>(typeof(TConfiguration).Name);
			handle.WaitForCompletion();

			Container.Bind<ConfigurationMonitor<TConfiguration>>().FromInstance(new ConfigurationMonitor<TConfiguration>(handle.Result)).AsSingle();
		}
	}
}