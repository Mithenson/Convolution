using Maxim.MVVM.Observables;
using UnityEngine;

namespace Convolution.MiniGames.Sample
{
	public sealed class TimerModel : Observable
	{
		private float _gameStartTimestamp;

		public float GameStartTimestamp
		{
			get => _gameStartTimestamp;
			set => ChangeProperty(ref _gameStartTimestamp, value);
		}

		public float ElapsedTime => Time.time - _gameStartTimestamp;
	}
}