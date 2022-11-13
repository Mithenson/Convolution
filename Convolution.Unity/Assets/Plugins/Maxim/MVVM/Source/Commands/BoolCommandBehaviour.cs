using System;

namespace Maxim.MVVM.Commands
{
	public sealed class BoolCommandBehaviour : CommandBehaviour
	{
		protected override Type SourceType => typeof(bool);

		public void Execute(bool value) => 
			_dataBindingTarget.Set(value);
	}
}