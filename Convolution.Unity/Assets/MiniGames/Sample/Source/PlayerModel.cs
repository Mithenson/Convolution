using Maxim.MVVM.Observables;

namespace Convolution.MiniGames.Sample
{
	public sealed class PlayerModel : Observable
	{
		private int _health;

		public int Health
		{
			get => _health;
			set => ChangeProperty(ref _health, value);
		}
	}
}