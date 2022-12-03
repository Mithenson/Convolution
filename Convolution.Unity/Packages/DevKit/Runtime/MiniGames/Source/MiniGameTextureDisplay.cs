using System;
using UnityEngine;

namespace Convolution.DevKit.MiniGames
{
	public sealed class MiniGameTextureDisplay : IMiniGameDisplay
	{
		private readonly MiniGameRenderer _renderer;
		
		private bool _isActive;
		private Texture2D _texture;
		private Color[] _clear;
	

		public MiniGameTextureDisplay(MiniGameRenderer renderer) => _renderer = renderer;

		public void Bootup(int width = 1920, int height = 1080, TextureFormat format = TextureFormat.ARGB32)
		{
			_texture = new Texture2D(width, height, format, false);
			_renderer.Set(_texture);

			_clear = new Color[_texture.width * _texture.height];
		}

		public void Show() => _isActive = true;

		public void Draw(int x, int y, Color color)
		{
			if (!_isActive)
				throw new InvalidOperationException();
			
			_texture.SetPixel(x, y, color);
		}
		public void Clear() => _texture.SetPixels(_clear);

		public void Hide()
		{
			Clear();
			_isActive = false;
		}
	}
}