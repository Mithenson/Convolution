using System;

namespace Maxim.MVVM.Observables
{
	public sealed class ListElementAddedEventArgs : EventArgs
	{
		public readonly object Item;
		
		public ListElementAddedEventArgs(object item) => 
			Item = item;
	}
}