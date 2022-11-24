using Convolution.Core.EmbeddedMiniGames;
using Convolution.DevKit.MiniGames;
using Cysharp.Threading.Tasks;

namespace Convolution.Orchestration
{
	public sealed class EmbeddedMiniGameContent : IMiniGameContent
	{
		private readonly IEmbeddedMiniGameConfiguration _configuration;
		
		public EmbeddedMiniGameContent(IEmbeddedMiniGameConfiguration configuration) => _configuration = configuration;

		public MiniGameDefinition Definition => _configuration.Definition;

		public UniTask<IMiniGameConfiguration> Load() => new UniTask<IMiniGameConfiguration>(_configuration);
		public UniTask Unload() => UniTask.CompletedTask;
	}
}