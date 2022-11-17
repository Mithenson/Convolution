using System;
using System.Collections.Generic;
using UnityEngine;

namespace Convolution.Placement
{
	public class RectangularGrid : Grid
	{
		[SerializeField]
		private Vector2Int _size;

		private Vector2Int _cachedSize;
		private Vector2Int[] _cells;

		public override IReadOnlyList<Vector2Int> Cells
		{
			get
			{
				_cells ??= new Vector2Int[0];
				if (_cachedSize == _size)
					return _cells;

				Array.Resize(ref _cells, _size.x * _size.y);
                
				var index = 0;
				for (var x = 0; x < _size.x; x++)
				{
					for (var y = 0; y < _size.y; y++)
					{
						_cells[index] = new Vector2Int(x, y);
						index++;
					}
				}

				_cachedSize = _size;
				return _cells;
			}
		}
	}
}