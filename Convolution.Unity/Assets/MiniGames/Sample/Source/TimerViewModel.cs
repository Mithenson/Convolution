using Maxim.MVVM;
using UnityEngine;
using Zenject;

namespace Convolution.MiniGames.Sample
{
	public sealed class TimerViewModel : ViewModel<TimerModel>, ITickable
	{
		private string _remainingTimeText;

		public string RemainingTimeText
		{
			get => _remainingTimeText;
			set => ChangeProperty(ref _remainingTimeText, value);
		}

		private readonly SampleMiniGameConfiguration _configuration;

		public TimerViewModel(TimerModel model, SampleMiniGameConfiguration configuration) : base(model)
		{
			_configuration = configuration;
		}

		void ITickable.Tick()
		{
			var remainingTime = _configuration.NeededSurvivalDuration - _model.ElapsedTime;
			if (remainingTime < 0.0f)
				remainingTime = 0.0f;
            
			RemainingTimeText = Mathf.CeilToInt(remainingTime).ToString();
		}
	}
}