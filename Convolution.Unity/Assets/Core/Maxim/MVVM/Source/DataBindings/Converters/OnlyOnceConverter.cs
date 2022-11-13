using System;

namespace Maxim.MVVM.DataBindings.Converters
{
	[Serializable]
	public sealed class OnlyOnceConverter : IDataBindingConverter
	{
		public Type OutputType => null;

		private bool _hasBeenExecutedOnce;
		
		public bool TryConvert(object input, out object output)
		{
			if (_hasBeenExecutedOnce)
			{
				output = default;
				return false;
			}

			_hasBeenExecutedOnce = true;
			output = input;

			return true;
		}
	}
}