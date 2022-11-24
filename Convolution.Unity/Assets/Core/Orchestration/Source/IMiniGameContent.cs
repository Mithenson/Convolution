using Convolution.DevKit.MiniGames;
using Cysharp.Threading.Tasks;

namespace Convolution.Orchestration
{
	public interface IMiniGameContent
	{
		MiniGameDefinition Definition { get; }
		
		UniTask<IMiniGameConfiguration> Load();
		UniTask Unload();
	}
}