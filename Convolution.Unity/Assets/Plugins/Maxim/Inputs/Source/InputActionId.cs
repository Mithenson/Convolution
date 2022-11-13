using System;
using UnityEngine.InputSystem;

namespace Maxim.Inputs
{
	public readonly struct InputActionId
	{
		public readonly Guid Value;

		public InputActionId(InputActionReference reference) => Value = reference.action.id;
		public InputActionId(Guid value) => Value = value;
	}
}