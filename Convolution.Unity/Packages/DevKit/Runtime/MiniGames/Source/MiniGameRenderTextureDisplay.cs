using UnityEngine;

namespace Convolution.DevKit.MiniGames
{
	public sealed class MiniGameRenderTextureDisplay : IMiniGameDisplay
	{
		private readonly MiniGameRenderer _renderer;
		
		public MiniGameRenderTextureDisplay(MiniGameRenderer renderer) => _renderer = renderer;

		public void Bootup(RenderTexture renderTexture) => _renderer.Set(renderTexture);
	}
}