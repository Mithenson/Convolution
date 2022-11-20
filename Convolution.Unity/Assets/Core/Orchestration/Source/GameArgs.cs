using Convolution.DevKit.MiniGames;

namespace Convolution.Orchestration
{
	public sealed class GameArgs
	{
		public MiniGameConfiguration GameConfiguration;

		public void Reset()
		{
			GameConfiguration = null;
		}
	}
}