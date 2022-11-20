using UnityEngine;

namespace Convolution.DevKit.MiniGames
{
	public sealed class MiniGameTextureDisplay : IMiniGameDisplay
	{
		private readonly MiniGameRenderer _renderer;
		
		private Texture2D _texture;
		private Color[] _clear;

		public MiniGameTextureDisplay(MiniGameRenderer renderer) => _renderer = renderer;

		public void Bootup(int with, int height, TextureFormat format)
		{
			_texture = new Texture2D(1920, 1080, TextureFormat.ARGB32, false);
			_renderer.Set(_texture);

			_clear = new Color[_texture.width * _texture.height];
		}

		public void Draw(int x, int y, Color color) => _texture.SetPixel(x, y, color);
		public void Clear() => _texture.SetPixels(_clear);
	}
}