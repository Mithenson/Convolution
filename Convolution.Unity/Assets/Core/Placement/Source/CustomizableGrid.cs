using System.Collections.Generic;
using UnityEngine;

namespace Convolution.Placement
{
	public sealed class CustomizableGrid : Grid
	{
		[SerializeField]
		private Vector2Int[] _cells = new Vector2Int[] { Vector2Int.zero };

		public override IReadOnlyList<Vector2Int> Cells => _cells;
	}
}