using Maxim.AssetManagement.Configurations;
using UnityEngine;

namespace Convolution.Handling
{
    public sealed class Cursor
    {
        public Vector2 Position;

        private readonly ConfigurationMonitor<CursorConfiguration> _configurationMonitor;
        
        public Cursor(ConfigurationMonitor<CursorConfiguration> configurationMonitor) => _configurationMonitor = configurationMonitor;

        public float Radius => _configurationMonitor.Configuration.Radius;
    }
}
