using System;

namespace Maxim.AssetManagement.Configurations
{
	public abstract class IConfigurationMonitor
	{ 
		Configuration Configuration { get; }
	}
	
	public sealed class ConfigurationMonitor<TConfiguration> : IConfigurationMonitor
		where TConfiguration : Configuration
	{
		public event Action OnRefresh;
		
		public TConfiguration Configuration { get; private set; }

		public ConfigurationMonitor(TConfiguration configuration) => Configuration = configuration;

		public void Refresh(TConfiguration configuration)
		{
			Configuration = configuration;
			OnRefresh?.Invoke();
		}
	}
}