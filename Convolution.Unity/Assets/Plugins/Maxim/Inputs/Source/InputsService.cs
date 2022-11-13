using UnityEngine.InputSystem;

namespace Maxim.Inputs
{
	public sealed class InputsService
	{
		private readonly InputActionAsset _asset;

		public InputsService(InputActionAsset asset) => _asset = asset;

		public void Enable() => _asset.Enable();

		public InputActionMap GetMap(InputActionMapId id) => _asset.FindActionMap(id.Value);
		public InputAction GetAction(InputActionId id) => _asset.FindAction(id.Value);

		public void Disable() => _asset.Disable();
	}
}