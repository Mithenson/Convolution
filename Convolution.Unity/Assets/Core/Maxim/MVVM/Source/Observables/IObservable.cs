namespace Maxim.MVVM.Observables
{
	public interface IObservable
	{
		event PropertyChangedEventHandler OnPropertyChanged;
	}
}