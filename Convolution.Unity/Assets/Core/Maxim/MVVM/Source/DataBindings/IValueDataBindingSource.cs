using System;

namespace Maxim.MVVM.DataBindings
{
	public interface IValueDataBindingSource
	{
		event Action<object> OnSourceChanged;
		object Value { get; }
	}
}