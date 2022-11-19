using Convolution.MiniGames.Source;
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

		private MiniGameState _miniGameState;
		public MiniGameState MiniGameState
		{
			get => _miniGameState;
			set => ChangeProperty(ref _miniGameState, value);
		}
	}
}