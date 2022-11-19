using Maxim.MVVM.Observables;

namespace Convolution.Gameplay
{
	public sealed class GameplayModel : Observable
	{
		private GameplayState _state;

		public GameplayState State
		{
			get => _state;
			set => ChangeProperty(ref _state, value);
		}
	}
}