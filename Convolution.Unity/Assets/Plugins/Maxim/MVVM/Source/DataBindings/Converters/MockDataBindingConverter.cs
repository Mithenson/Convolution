using System;

namespace Maxim.MVVM.DataBindings.Converters
{
	public sealed class MockDataBindingConverter : IDataBindingConverter
	{
		public Type OutputType => null;

		public bool TryConvert(object input, out object output)
		{
			output = input;
			return true;
		}
	}
}