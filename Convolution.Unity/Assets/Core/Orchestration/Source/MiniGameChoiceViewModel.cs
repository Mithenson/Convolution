using System.Collections.Generic;
using Maxim.MVVM;
using Maxim.MVVM.Observables;

namespace Convolution.Orchestration
{
	public sealed class MiniGameChoiceViewModel : ViewModel
	{
		public ObservableList<MiniGameViewModel> MiniGameViewModels { get; private set; }
		
		private readonly GameContext _gameContext;

		public MiniGameChoiceViewModel(IReadOnlyList<IMiniGameContent> contents, GameContext gameContext)
		{
			_gameContext = gameContext;
			
			MiniGameViewModels = new ObservableList<MiniGameViewModel>();
			foreach (var content in contents)
			{
				var miniGameViewModel = new MiniGameViewModel(content, _gameContext);
				MiniGameViewModels.Add(miniGameViewModel);
			}
		}
	}
}