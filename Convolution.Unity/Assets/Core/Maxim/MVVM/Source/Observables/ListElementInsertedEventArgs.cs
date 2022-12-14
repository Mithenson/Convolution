using System;

namespace Maxim.MVVM.Observables
{
	public sealed class ListElementInsertedEventArgs : EventArgs
	{
		public readonly object Item;
		public readonly int Index;
		
		public ListElementInsertedEventArgs(object item, int index)
		{
			Item = item;
			Index = index;
		}
	}
}