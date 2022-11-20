using Convolution.DevKit.Controllers;
using Convolution.DevKit.MiniGames;

namespace Convolution.Interaction
{
	public sealed class ControllerInputBridgeService
	{
		private readonly ControllerRepository _controllerRepository;
        
		public ControllerInputBridgeService(ControllerRepository controllerRepository) => _controllerRepository = controllerRepository;
        
		public void Tick(MiniGame miniGame)
		{
			foreach (var controller in _controllerRepository.Controllers)
			{
				var input = controller.ComputeInput();
				miniGame.HandleInput(input, controller.InputChannel);
			}
		}
	}
}