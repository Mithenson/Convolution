using System.Threading.Tasks;
using UnityEngine;

namespace Convolution.DevKit.MiniGames
{
	public sealed class MiniGameRenderTextureDisplay : IMiniGameDisplay
	{
		private readonly MiniGameRenderer _renderer;

		private RenderTexture _renderTexture;
		
		public MiniGameRenderTextureDisplay(MiniGameRenderer renderer) => _renderer = renderer;

		public void Bootup(RenderTexture renderTexture) => _renderTexture = renderTexture;

		public void Show() => _renderer.Set(_renderTexture);
		public void Hide() => _renderer.Set(null);
	}
}