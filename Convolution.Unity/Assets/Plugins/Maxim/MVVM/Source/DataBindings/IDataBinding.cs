using System;

namespace Maxim.MVVM.DataBindings
{
	public interface IDataBinding : IDisposable
	{
		bool IsActive { get; }
		void SetActive(bool value);
	}
}