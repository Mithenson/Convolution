using Cysharp.Threading.Tasks;
using Zenject;

namespace Maxim.AssetManagement.Configurations
{
	public sealed class ConfigurationInstaller<TConfiguration> : Installer<ConfigurationInstaller<TConfiguration>>
		where TConfiguration : Configuration
	{
		public override async UniTask InstallBindings()
		{
			var configurationHandle = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<TConfiguration>(typeof(TConfiguration).Name);
			var configuration = await configurationHandle.ToUniTask();
			
			Container.Bind<ConfigurationMonitor<TConfiguration>>().ToSelf().FromInstance(new ConfigurationMonitor<TConfiguration>(configuration)).AsSingle();
			UnityEngine.AddressableAssets.Addressables.Release(configurationHandle);
		}
	}
}