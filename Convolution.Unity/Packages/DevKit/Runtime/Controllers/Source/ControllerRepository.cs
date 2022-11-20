using System.Collections.Generic;

namespace Convolution.DevKit.Controllers
{
	public sealed class ControllerRepository
	{
		private List<Controller> _controllers;

		public ControllerRepository() => _controllers = new List<Controller>();

		public IReadOnlyList<Controller> Controllers => _controllers;

		public void Bootup(IEnumerable<Controller> controllers) => _controllers.AddRange(controllers);
	}
}