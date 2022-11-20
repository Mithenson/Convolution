using Maxim.AssetManagement.Configurations;
using Maxim.Inputs;
using UnityEngine.InputSystem;

namespace Convolution.Gameplay
{
	public sealed class GameplayInputsRepository 
	{
		private readonly InputsService _underlyingService;
		private readonly ConfigurationMonitor<GameplayInputsConfiguration> _configuration;

		public InputAction CursorPositionAction { get; private set; }
		public InputAction CursorClickAction { get; private set; }
        
		public GameplayInputsRepository(
			InputsService underlyingService, 
			ConfigurationMonitor<GameplayInputsConfiguration> configuration)
		{
			_underlyingService = underlyingService;
			_configuration = configuration;

			CursorPositionAction = _underlyingService.GetAction(new InputActionId(_configuration.Configuration.CursorPositionInputReference));
			CursorClickAction =  _underlyingService.GetAction(new InputActionId(_configuration.Configuration.CursorClickInputReference));
		}
	}
}