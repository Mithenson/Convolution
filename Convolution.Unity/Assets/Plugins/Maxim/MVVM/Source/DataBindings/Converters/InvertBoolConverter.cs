using System;

namespace Maxim.MVVM.DataBindings.Converters
{
	[Serializable]
	public sealed class InvertBoolConverter : DataBindingConverter<bool, bool>
	{
		public override bool TryConvertExplicitly(bool input, out bool output)
		{
			output = !input;
			return true;
		}
	}
}