using System;

namespace Maxim.Inputs
{
	public readonly struct InputActionMapId
	{
		public readonly Guid Value;

		public InputActionMapId(InputActionMapReference reference) => Value = reference.Id;
		public InputActionMapId(Guid value) => Value = value;
	}
}