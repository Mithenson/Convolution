using Convolution.DevKit.MiniGames;
using Convolution.Gameplay;
using Cysharp.Threading.Tasks;
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
		private readonly GameContext _gameContext;
		
		public GameplayEndMenuViewModel(GameplayModel model, GameContext gameContext)
		{
			_model = model;
			_gameContext = gameContext;

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

		public void Restart() => _gameContext.Restart().Forget();
		public void Leave() => _gameContext.Leave().Forget();
	}
}