using Convolution.DevKit.MiniGames;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Convolution.Orchestration
{
	public sealed class MiniGameDisplayInstaller : Installer<MiniGameDisplaySceneRepository, MiniGameRenderer, MiniGameDisplayInstaller>
	{
		private readonly MiniGameDisplaySceneRepository _displaySceneRepository;
		private readonly MiniGameRenderer _renderer;
        
		public MiniGameDisplayInstaller(MiniGameDisplaySceneRepository displaySceneRepository, MiniGameRenderer renderer)
		{
			_displaySceneRepository = displaySceneRepository;
			_renderer = renderer;
		}

		public override UniTask InstallBindings()
		{
			var displays = _displaySceneRepository.Displays;
			foreach (var display in displays)
			{
				var displayType = display.GetType();
				Container.Bind(displayType).To(displayType).FromInstance(display).AsSingle();
			}

			Container.Bind<MiniGameTextureDisplay>().ToSelf().AsSingle();
			Container.Bind<MiniGameRenderTextureDisplay>().ToSelf().AsSingle();

			Container.Bind<MiniGameRenderer>().FromInstance(_renderer);
			return UniTask.CompletedTask;
		}
	}
}