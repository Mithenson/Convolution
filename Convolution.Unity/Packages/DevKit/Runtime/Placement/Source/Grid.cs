using System.Collections.Generic;
using Maxim.AssetManagement.Configurations;
using UnityEngine;
using Zenject;

namespace Convolution.DevKit.Placement
{
    public abstract class Grid : MonoBehaviour
    {
        public abstract IReadOnlyList<Vector2Int> Cells { get; }

        private ConfigurationMonitor<GridConfiguration> _configurationMonitor;
        private HashSet<Vector2Int> _set;

        [Inject]
        private void Inject(ConfigurationMonitor<GridConfiguration> configurationMonitor) => _configurationMonitor = configurationMonitor;
        
        private void Awake() => _set = new HashSet<Vector2Int>(Cells);

        protected GridConfiguration Configuration => _configurationMonitor.Configuration;

        public bool Has(Vector2Int cell) => _set.Contains(cell);
        public bool Fits(Vector2Int position, Grid grid)
        {
            foreach (var cell in grid.Cells)
            {
                var transformedCell = position + cell;
                if (!_set.Contains(transformedCell))
                    return false;
            }

            return true;
        }

        public void Place(Vector2Int position, Transform target)
        {
            var globalPosition = (Vector2)transform.position + (Vector2)position * (Configuration.CellSize + Configuration.Spacing);
            target.position = globalPosition;
        }
    }
}
