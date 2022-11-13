using System;

namespace Maxim.MVVM.DataBindings.Converters
{
	[Serializable]
	public sealed class ToStringDataBindingConverter : DataBindingConverter<object, string>
	{
		public override bool TryConvertExplicitly(object input, out string output)
		{
			output = input.ToString();
			return true;
		}
		
	}
}