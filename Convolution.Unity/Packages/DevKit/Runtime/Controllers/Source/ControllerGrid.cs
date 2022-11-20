using Convolution.DevKit.Placement;
using UnityEngine;

namespace Convolution.DevKit.Controllers
{
	public sealed class ControllerGrid : RectangularGrid
	{
		public void Place(Vector2Int position, Controller controller) => Place(position, controller.transform);
	}
}