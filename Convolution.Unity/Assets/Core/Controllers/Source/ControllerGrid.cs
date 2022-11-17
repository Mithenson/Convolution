using Convolution.Placement;
using UnityEngine;

namespace Convolution.Controllers
{
	public sealed class ControllerGrid : RectangularGrid
	{
		public void Place(Vector2Int position, Controller controller) => Place(position, controller.transform);
	}
}