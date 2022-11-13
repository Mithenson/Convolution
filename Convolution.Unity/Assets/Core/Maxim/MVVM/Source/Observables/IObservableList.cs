using System.Collections;

namespace Maxim.MVVM.Observables
{
	public interface IObservableList : IList
	{
		event ListElementAddedEventHandler OnElementAdded;
		event ListElementInsertedEventHandler OnElementInserted;
		event ListElementRemovedEventHandler OnElementRemoved;
		event ListClearedEventHandler OnCleared;
	}
}