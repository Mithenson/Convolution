using System;

namespace Maxim.MVVM.Commands
{
	public sealed class FloatCommandBehaviour : CommandBehaviour
	{
		protected override Type SourceType => typeof(float);

		public void Execute(float value) => 
			_dataBindingTarget.Set(value);
	}
}