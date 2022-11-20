using Convolution.DevKit.MiniGames;
using Convolution.Gameplay;
using Maxim.Common.Extensions;
using Maxim.MVVM;

namespace Convolution.Orchestration
{
	public sealed class GameplayEndMenuViewModel : ViewModel
	{
		private bool _isActive;
		public bool IsActive
		{
			get => _isActive;
			set => ChangeProperty(ref _isActive, value);
		}

		private string _endText;
		public string EndText
		{
			get => _endText;
			set => ChangeProperty(ref _endText, value);
		}

		private readonly GameplayModel _model;
		private readonly RestartService _restartService;
		
		public GameplayEndMenuViewModel(GameplayModel model, RestartService restartService)
		{
			_model = model;
			_restartService = restartService;

			AddMethodBinding(model, nameof(GameplayModel.State), nameof(OnGameplayStateChanged));
		}

		public void OnGameplayStateChanged(GameplayState state)
		{
			IsActive = state == GameplayState.Done;
			
			switch (_model.MiniGameState)
			{
				case MiniGameState.Won:
					EndText = "You won";
					break;
				
				case MiniGameState.Failed:
					EndText = "You lost";
					break;

				default:
					EndText = string.Empty;
					break;
			}
		}

		public void Restart() => _restartService.Restart().FireAndForget();
	}
}