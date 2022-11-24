using Cysharp.Threading.Tasks;
using Maxim.MVVM;
using Maxim.MVVM.Observables;

namespace Convolution.Orchestration
{
	public sealed class MiniGameViewModel : ViewModel
	{
		private string _name;
		public string Name
		{
			get => _name;
			set => ChangeProperty(ref _name, value);
		}

		private readonly IMiniGameContent _content;
		private readonly GameContext _gameContext;
		
		public MiniGameViewModel (IMiniGameContent content, GameContext gameContext)
		{
			_content = content;
			_gameContext = gameContext;
			
			Name = content.Definition.Name;
		}

		public void Pick() => _gameContext.Start(_content).Forget();
	}
}