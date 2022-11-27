using Maxim.AssetManagement.Configurations;
using UnityEngine;

namespace Convolution.DevKit.Interaction
{
    public sealed class Cursor
    {
        private Vector2 _lastPosition;
        private Vector2 _position;

        private readonly ConfigurationMonitor<CursorConfiguration> _configurationMonitor;
        
        public Cursor(ConfigurationMonitor<CursorConfiguration> configurationMonitor) => _configurationMonitor = configurationMonitor;

        public Vector2 LastPosition => _lastPosition;
        public Vector2 Position
        {
            get => _position;
            set
            {
                _lastPosition = _position;
                _position = value;
            }
        }
        public Vector2 Delta => Position - LastPosition;

        public float Radius => _configurationMonitor.Configuration.Radius;
    }
}
