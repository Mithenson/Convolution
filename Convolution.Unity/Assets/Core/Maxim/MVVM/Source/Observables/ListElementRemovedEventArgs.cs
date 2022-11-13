using System;

namespace Maxim.MVVM.Observables
{
	public sealed class ListElementRemovedEventArgs : EventArgs
	{
		public readonly object Item;
		public readonly int Index;
		
		public ListElementRemovedEventArgs(object item, int index)
		{
			Item = item;
			Index = index;
		}
	}
}