using Maxim.AssetManagement.Configurations;
using UnityEngine;

namespace Convolution.Placement
{
	[CreateAssetMenu(menuName = "Convolution/Configurations/Grid", fileName = nameof(GridConfiguration))]
	public sealed class GridConfiguration : Configuration
	{
		[SerializeField]
		private float _cellSize;

		[SerializeField]
		private float _spacing;
		
		public float CellSize => _cellSize;
		public float Spacing => _spacing;
	}
}